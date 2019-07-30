using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToolInputManager : Singleton<ToolInputManager> {
    [Header("Current Selection")] public bool inFocus; // If something selected is it EVEN selected at all? Or is it just in memory for shows (if inFocus is false and selectedObjects has stuff in it)?

    public ARObject activeSelectedObject {
        get { return toolController.currentSelectedObject;}
        set { toolController.currentSelectedObject = value; }
    }
    public Tool activeSelectedTool {
        get { return toolController.currentToolObject; }
        set { toolController.currentToolObject = value; }
    }
    public ToolHandle activeHandle;

    // You should probably move this into the ObjectManager. It just makes more sense.
    public ActiveObjectChangedEvent onActiveObjectChanged = new ActiveObjectChangedEvent();
    public ToolController toolController;

#if UNITY_EDITOR
    [Header("Debug Settings")]
    public bool showDebugRay = true;
    public bool showDebugMessages = true;
#endif

    /// <summary>
    /// Are any screens visible?
    /// </summary>
    /// <remarks>
    /// If yes, you shouldn't be running. Ignore any presses.
    /// </remarks>
    public bool screenVisible;
    bool stillHolding;

    [Header("Performance Settings")] public float updateRate = 0.1f;
    float nextUpdateTime;

    void Update() {
        UpdateHold();
        UpdatePress();
    }


    public void UpdateHold() {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && showDebugRay) {
            Ray cameraRay = ARSessionManager.instance.cam.camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(cameraRay.origin, cameraRay.origin + (cameraRay.direction * 100f), Color.yellow);
            Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.red);
        }
#endif

#if UNITY_EDITOR || UNITY_STANDALONE
        if (!Input.GetMouseButton(0) || screenVisible) {
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount <= 0 || screenVisible) {
#endif
            stillHolding = false;
            if (activeHandle == null) return;
            activeHandle.isSelected = false;
            activeHandle = null;
        }
    }

    public void UpdatePress() {
        if (stillHolding) return;
        if (screenVisible) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0)) {
            if (Time.time < nextUpdateTime) return;
            Vector3 touchPosition = Input.mousePosition;
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0) {
            if (Time.time < nextUpdateTime) return;
            Vector2 _touchPosition = Input.GetTouch(0).position;
            Vector3 touchPosition = new Vector3(_touchPosition.x, _touchPosition.y, 0);
#endif
            nextUpdateTime = Time.time + updateRate;
            stillHolding = true;
            Camera camera = ARSessionManager.instance.cam.camera;
            Ray cameraRay = camera.ScreenPointToRay(touchPosition);
            RaycastHit[] physicsResults = Physics.RaycastAll(cameraRay, camera.farClipPlane);
            ProcessPress(physicsResults);
        }
    }

    public void ProcessPress(RaycastHit[] hits) {
        if (hits == null) {
            Debug.LogError("This should not be happening. Needs to be handled as if no hits.");
        }
        if (hits.Length == 0) {
            activeSelectedObject = null;
            activeHandle = null;
            return;
        }

        bool overrideIfSamePriority = false; // Do we also sort by order, where being lower down the list is also a sort parameter.
        int currentMaxPriority = int.MinValue;
        ToolHandle currentHandle = null;
        ARObject currentObject = null;
        foreach (RaycastHit hit in hits) {
            ToolHandle handle = hit.collider.gameObject.GetComponent<ToolHandle>();
            ARObject arObject = hit.collider.gameObject.GetComponent<ARObject>();

#if UNITY_EDITOR
            if (showDebugMessages) {
                if (handle != null) Debug.Log("Found Handle!", handle.gameObject);
                if (arObject != null) Debug.Log("Found ARObject!", arObject.gameObject);
            }
#endif

            if (handle == null && arObject == null) continue;
            if (currentObject == null && arObject != null) currentObject = arObject;
            if (handle != null) {
                if ((handle.priority == currentMaxPriority && overrideIfSamePriority) || (handle.priority > currentMaxPriority)) {
                    currentHandle = handle;
                    currentMaxPriority = handle.priority;
                }
            }
        }

        if (currentHandle != null) {
            activeHandle = currentHandle;
            activeHandle.isSelected = true;
            return;
        }
        if (currentHandle == null) {
            if (currentObject != null) {
                activeSelectedObject = currentObject;
                return;
            }
            if (currentObject == null && activeSelectedObject != null) {
                activeSelectedObject = null;
            }
        }
    }
}

// ARObject oldActive, ARObject newActive
public class ActiveObjectChangedEvent : UnityEvent<ARObject, ARObject> {

}