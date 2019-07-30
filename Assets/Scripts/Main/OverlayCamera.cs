using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This camera renders a overlay-only layer to be drawn on top of everything.
/// </summary>
/// <remarks>
/// RenderTextures are slow, USE A FUCKING SHADER WITH Z-DEPTH.
/// (And probably add in a copy of the standard shader)
/// </remarks>
[RequireComponent(typeof(Camera))]
public class OverlayCamera : MonoBehaviour {
    public Camera self {
        get {
            if (_self == null) _self = GetComponent<Camera>();
            return _self;
        }
    }
    Camera _self;
    public Camera fallbackCamera;
    public Camera targetCamera {
        get {
            if (_targetCamera == null) if (ARSessionManager.instance.cam != null) _targetCamera = ARSessionManager.instance.cam.camera;
            if (_targetCamera == null) return fallbackCamera;
            return _targetCamera;
        }
    }
    Camera _targetCamera;
    public RenderTexture targetTexture;

    void Start() {
        SetupRenderTexture();
    }

    void SetupRenderTexture() {
        targetTexture.height = Screen.height;
        targetTexture.width = Screen.width;
    }

    void Update() {
        transform.position = targetCamera.transform.position;
        transform.rotation = targetCamera.transform.rotation;
    }
}
