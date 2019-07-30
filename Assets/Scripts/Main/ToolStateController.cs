using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ToolController))]
public class ToolStateController : Singleton<ToolStateController> {
    public ToolState currentToolType { get; private set; }
    public GlobalityState globalityType { get; private set; }
    public PivotState pivotType { get; private set; }

    public ToolChangeEvent OnToolChange = new ToolChangeEvent();
    public GlobalityChangeEvent OnGlobalityChange = new GlobalityChangeEvent();
    public PivotChangeEvent OnPivotChange = new PivotChangeEvent();
    public Tool activeTool;

    public const string initialToolPrefName = "pref_tool_initTool";
    public const string initialGlobalityPrefName = "pref_tool_initGlobality";
    public const string initialPivotPrefName = "pref_tool_initPivotState";
    public const ToolState initialToolPrefDefaultValue = ToolState.Move;
    public const GlobalityState initialGlobalityPrefDefaultValue = GlobalityState.Global;
    public const PivotState initialPivotPrefDefaultValue = PivotState.Pivot;

    void Awake() {
        alreadySaved = false;
        SetupState();
    }

    bool alreadySaved = false;
    void OnApplicationQuit() {
        if (!alreadySaved) {
            alreadySaved = true;
            SaveState();
        }
    }

    void OnDestroy() {
        if (!alreadySaved) {
            alreadySaved = true;
            SaveState();
        }
    }

    public void SetupState() {
        currentToolType = (ToolState) PlayerPrefs.GetInt(initialToolPrefName, (int) initialToolPrefDefaultValue);
        globalityType = (GlobalityState) PlayerPrefs.GetInt(initialGlobalityPrefName, (int) initialGlobalityPrefDefaultValue);
        pivotType = (PivotState) PlayerPrefs.GetInt(initialPivotPrefName, (int) initialPivotPrefDefaultValue);
    }

    public void SaveState() {
        PlayerPrefs.SetInt(initialToolPrefName, (int) currentToolType);
        PlayerPrefs.SetInt(initialGlobalityPrefName, (int) globalityType);
        PlayerPrefs.SetInt(initialPivotPrefName, (int) pivotType);
        PlayerPrefs.Save();
    }

    public void SwitchTools(ToolState newTool) {
        ToolState oldTool = currentToolType;
        if (newTool == oldTool) return;
        currentToolType = newTool;
        OnToolChange.Invoke(oldTool, newTool);
    }

    public ToolState CycleToolState() {
        ToolState oldState = currentToolType;
        ToolState newState = ToolState.None;
        switch (currentToolType) {
            case ToolState.None:
                newState = ToolState.Move;
                break;
            case ToolState.Move:
                newState = ToolState.Rotate;
                break;
            case ToolState.Rotate:
                newState = ToolState.Scale;
                break;
            case ToolState.Scale:
                newState = ToolState.Pose;
                break;
            case ToolState.Pose:
                newState = ToolState.Gimbal;
                break;
            case ToolState.Gimbal:
                newState = ToolState.Properties;
                break;
            case ToolState.Properties:
                newState = ToolState.Animation;
                break;
            case ToolState.Animation:
                newState = ToolState.Create;
                break;
            case ToolState.Create:
                newState = ToolState.Duplicate;
                break;
            case ToolState.Duplicate:
                newState = ToolState.Move;
                break;
        }

        currentToolType = newState;
        OnToolChange.Invoke(oldState, newState);
        return newState;
    }

    public GlobalityState ToggleGlobalityState() {
        GlobalityState oldState = globalityType;
        GlobalityState newState = GlobalityState.Global;
        switch (globalityType) {
            case GlobalityState.Global:
                newState = GlobalityState.Local;
                break;
            case GlobalityState.Local:
                newState = GlobalityState.Global;
                break;
        }

        globalityType = newState;
        OnGlobalityChange.Invoke(oldState, newState);
        return newState;
    }

    public PivotState TogglePivotState() {
        PivotState oldState = pivotType;
        PivotState newState = PivotState.Pivot;
        switch (pivotType) {
            case PivotState.Pivot:
                newState = PivotState.Center;
                break;
            case PivotState.Center:
                newState = PivotState.Pivot;
                break;
        }

        pivotType = newState;
        OnPivotChange.Invoke(oldState, pivotType);
        return newState;
    }
}

[Serializable]
public class ToolChangeEvent : UnityEvent<ToolState, ToolState> { }
[Serializable]
public class GlobalityChangeEvent : UnityEvent<GlobalityState, GlobalityState> { }
[Serializable]
public class PivotChangeEvent : UnityEvent<PivotState, PivotState> { }

public enum ToolState {
    None,
    Move,
    Rotate,
    Scale,
    Pose,
    Gimbal,
    Properties,
    Animation,
    Create,
    Duplicate
}

public enum GlobalityState {
    Global,
    Local
}

public enum PivotState {
    Pivot,
    Center
}
