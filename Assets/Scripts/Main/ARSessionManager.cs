using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Manages the AR Foundation objects that are used by Unity.
/// This starts, stops and switches modes.
/// </summary>
public class ARSessionManager : Singleton<ARSessionManager> {
    public GameObject frontCameraSessionPrefab;
    public GameObject backCameraSessionPrefab;
    public GameObject offPrefab;

    public GameObject sessionGameObject;
    public ARSession sessionController = null;
    public ARInputManager inputManager = null;
    public ARSessionOrigin sessionObject = null;
    public ARCamera cam;

    public ARCameraCreateEvent OnCameraCreate = new ARCameraCreateEvent();

    public ARSessionType sessionType = ARSessionType.None;
    [SerializeField]
    [EditorReadOnly]
    ARSessionType _sessionType = ARSessionType.None;

#if UNITY_EDITOR
    [Header("Editor Settings")]
    public bool moveToMainCameraOnStart;
    public Camera mainCamera;
#endif

    public override void Awake() {
        base.Awake();
        sessionType = ARSessionType.Front;
    }

    void Update() {
        UpdateState();
    }

    public void ToggleSessionType() {
        switch (sessionType) {
            case ARSessionType.None:
                sessionType = ARSessionType.None;
                return;
            case ARSessionType.Front:
                sessionType = ARSessionType.Back;
                return;
            case ARSessionType.Back:
                sessionType = ARSessionType.Front;
                return;
        }
    }

    public void ToggleSessionOn() {
        switch (sessionType) {
            case ARSessionType.None:
                sessionType = ARSessionType.Front;
                return;
            case ARSessionType.Front:
                sessionType = ARSessionType.None;
                return;
            case ARSessionType.Back:
                sessionType = ARSessionType.None;
                return;
        }
    }

    public void UpdateState() {
        // Basically split the operation up into two frames.
        if (_sessionType == ARSessionType.None) {
            if (sessionType == ARSessionType.None) return;
            _sessionType = sessionType;
            CreateState(sessionType);
            return;
        }
        if (sessionType != _sessionType) {
            DisposeState(_sessionType);
            _sessionType = ARSessionType.None;
        }
    }

    private void DisposeState(ARSessionType state) {
        ARSessionType previousType = _sessionType;
        switch (state) {
            case ARSessionType.None:
                Destroy(sessionGameObject);
                break;
            case ARSessionType.Front:
                Destroy(sessionGameObject);
                Destroy(sessionController);
                Destroy(inputManager);
                break;
            case ARSessionType.Back:
                goto case ARSessionType.Front;
        }
        if (sessionType == ARSessionType.Reset) sessionType = previousType;
    }

    private void CreateState(ARSessionType state) {
        switch (state) {
            case ARSessionType.None:
                sessionGameObject = Instantiate(offPrefab);
                if (SceneManager.GetActiveScene() != gameObject.scene)
                    SceneManager.MoveGameObjectToScene(sessionGameObject, gameObject.scene);
                return;
            case ARSessionType.Front:
                sessionController = gameObject.AddComponent<ARSession>();
                inputManager = gameObject.AddComponent<ARInputManager>();
                sessionGameObject = Instantiate(frontCameraSessionPrefab);
                if (SceneManager.GetActiveScene() != gameObject.scene)
                    SceneManager.MoveGameObjectToScene(sessionGameObject, gameObject.scene);
                sessionObject = sessionGameObject.GetComponent<ARSessionOrigin>();
                return;
            case ARSessionType.Back:
                sessionController = gameObject.AddComponent<ARSession>();
                inputManager = gameObject.AddComponent<ARInputManager>();
                sessionGameObject = Instantiate(backCameraSessionPrefab);
                if (SceneManager.GetActiveScene() != gameObject.scene)
                    SceneManager.MoveGameObjectToScene(sessionGameObject, gameObject.scene);
                sessionObject = sessionGameObject.GetComponent<ARSessionOrigin>();
                return;
        }
    }

    public void Reset() {
        sessionType = ARSessionType.Reset;
    }
}

public enum ARSessionType {
    None,       // Basically it's off.
    Front,      // Uses the front-facing camera, typically used for environment tracking.
    Back,       // Uses the rear-facing camera, typically used for face tracking.
    Reset
}

public class ARCameraCreateEvent : UnityEvent<ARCamera> { }