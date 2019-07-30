using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// A reference call-back class that just sets the camera reference in ARSessionManager.
/// The actual camera is implemented in ARCameraBackground and TrackedPoseDriver
/// </summary>
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(ARCameraBackground))]
[RequireComponent(typeof(TrackedPoseDriver))]
public class ARCamera : MonoBehaviour {
    public new Camera camera {
        get {
            if (_camera == null) _camera = GetComponent<Camera>();
            return _camera;
        }
    }
    [SerializeField]
    Camera _camera;

    public ARCameraBackground background {
        get {
            if (_background == null) _background = GetComponent<ARCameraBackground>();
            return _background;
        }
    }
    [SerializeField]
    ARCameraBackground _background;

    public TrackedPoseDriver poseDriver {
        get {
            if (_poseDriver == null) _poseDriver = GetComponent<TrackedPoseDriver>();
            return _poseDriver;
        }
    }
    TrackedPoseDriver _poseDriver;

    void Start() {
        ARSessionManager.instance.cam = this;
        ARSessionManager.instance.OnCameraCreate.Invoke(this);
        int layer31BitValue = (int) Math.Pow(2, 31); // 2,147,483,648‬
        if (camera.cullingMask >= layer31BitValue) camera.cullingMask -= layer31BitValue;
#if UNITY_EDITOR
        if (ARSessionManager.instance.moveToMainCameraOnStart) transform.position = ARSessionManager.instance.mainCamera.transform.position;
#endif
    }

    void OnDestroy() {
        if (ARSessionManager.instance.cam == this) ARSessionManager.instance.cam = null;
    }
}
