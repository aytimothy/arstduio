using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTool : Tool {

    public Color highlightColor;
    public RotateAxisHandle xAxisHandle;
    public RotateAxisHandle yAxisHandle;
    public RotateAxisHandle zAxisHandle;
    public RotateGimbalHandle gimbalHandle;
    public RotateGimbalHandle wAxisHandle => gimbalHandle;
    public float radius;
    public float gimbalRadius => radius * 1.1f;

    protected virtual void Start() {
        Debug.Log("Rotate Tool Spawned!");
    }

    public override void OnGlobalityChange(GlobalityState oldState, GlobalityState newState) {
        
    }

    public override void OnPivotChange(PivotState oldState, PivotState newState) {

    }

    public override void ShowVisuals() {
        xAxisHandle.gameObject.SetActive(true);
        yAxisHandle.gameObject.SetActive(true);
        zAxisHandle.gameObject.SetActive(true);
        wAxisHandle.gameObject.SetActive(true);
    }

    public override void HideVisuals() {
        xAxisHandle.gameObject.SetActive(false);
        yAxisHandle.gameObject.SetActive(false);
        zAxisHandle.gameObject.SetActive(false);
        wAxisHandle.gameObject.SetActive(false);
    }
}
