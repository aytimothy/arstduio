using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ToolInputManager))]
// [RequireComponent(typeof(ObjectManager))]
public class ActiveObjectOutliner : Singleton<ActiveObjectOutliner> {
    public ToolInputManager objectManager;
    public ARObject activeObject => objectManager.activeSelectedObject;
    public ARSessionManager sessionManager;
    public new ARCamera camera => sessionManager.cam;

    void Start() {
        objectManager.onActiveObjectChanged.AddListener(OnActiveObjectChanged);
        sessionManager.OnCameraCreate.AddListener(OnCameraCreate);
    }

    public void OnCameraCreate(ARCamera newCam) {
        OnActiveObjectChanged(activeObject, activeObject);
    }

    Material originalMaterial;

    public static string outlineShaderName {
        get { return instance._outlineShaderName; }
        set { instance._outlineShaderName = value; }
    }
    public static Color outlineColor {
        get { return instance._outlineColor; }
        set { instance._outlineColor = value; }
    }
    public static float outlineWidth {
        get { return instance._outlineWidth; }
        set { instance._outlineWidth = value; }
    }
    [SerializeField]
    string _outlineShaderName = "Outline";
    [SerializeField]
    Color _outlineColor = Color.yellow;
    [SerializeField]
    float _outlineWidth = 0.5f;
    public void OnActiveObjectChanged(ARObject oldObject, ARObject newObject) {
        if (oldObject != null) oldObject.Unhighlight();
        if (newObject != null) newObject.Highlight();
    }
}
