using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTool : Tool {
    public ScaleToolHandle xHandle;
    public ScaleToolHandle yHandle;
    public ScaleToolHandle zHandle;
    public ScaleToolRod xRod;
    public ScaleToolRod yRod;
    public ScaleToolRod zRod;
    public ScaleToolPlane xyPlane;
    public ScaleToolPlane xzPlane;
    public ScaleToolPlane yzPlane;
    public MeshRenderer handleRenderer;

    [Header("Visual Settings")]
    public Color highlightColor;

    public GlobalityState globalityState;

    public override void OnGlobalityChange(GlobalityState oldState, GlobalityState newState) {
        globalityState = newState;
    }

    public override void OnPivotChange(PivotState oldState, PivotState newState) {

    }

    public override void ShowVisuals() {
        xHandle.gameObject.SetActive(true);
        yHandle.gameObject.SetActive(true);
        zHandle.gameObject.SetActive(true);
        xRod.gameObject.SetActive(true);
        yRod.gameObject.SetActive(true);
        zRod.gameObject.SetActive(true);
        xyPlane.gameObject.SetActive(true);
        xzPlane.gameObject.SetActive(true);
        yzPlane.gameObject.SetActive(true);
        handleRenderer.gameObject.SetActive(true);
    }

    public override void HideVisuals() {
        xHandle.gameObject.SetActive(false);
        yHandle.gameObject.SetActive(false);
        zHandle.gameObject.SetActive(false);
        xRod.gameObject.SetActive(false);
        yRod.gameObject.SetActive(false);
        zRod.gameObject.SetActive(false);
        xyPlane.gameObject.SetActive(false);
        xzPlane.gameObject.SetActive(false);
        yzPlane.gameObject.SetActive(false);
        handleRenderer.gameObject.SetActive(false);
    }
}