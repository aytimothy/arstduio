using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTool : Tool {
    [Header("Interactables")]
    public TransformToolHandle xHandle;
    public TransformToolHandle yHandle;
    public TransformToolHandle zHandle;
    public TransformToolRod xRod;
    public TransformToolRod yRod;
    public TransformToolRod zRod;
    public TransformToolPlane xyPlane;
    public TransformToolPlane xzPlane;
    public TransformToolPlane yzPlane;
    public MeshRenderer handleRenderer;
    public GlobalityState globalityState;

    [Header("Visual Settings")]
    public Color highlightColor;

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