using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_IOS || UNITY_ANDROID
using System.Runtime.Remoting;
#endif
using UnityEngine;

public class TransformToolPlane : TransformToolHandleBase {
    public int materialID;
    public Color originalColor;
    public float transparencyOverrideAmount;
    public Color highlightColor => controller.highlightColor;
    public MeshRenderer meshRenderer;
    public PlaneAxis axis;
    public Vector3 offset;

    void Start() {
        originalColor = meshRenderer.materials[materialID].color;
    }

    public override void SelectedUpdate() {
        controller.slave.transform.position = GetInteractPoint() + offset;
    }

    public Vector3 GetInteractPoint() {
#if UNITY_EDITOR || UNITY_STANDALONE
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
        Ray ray = camera.ScreenPointToRay(Input.touches[0].position);
#endif
        Vector3 deltaDirection = Vector3.Normalize(ray.direction);

        float difference;
        float magnitude;
        Vector3 intersection = Vector3.zero;
        switch (axis) {
            case PlaneAxis.XY:
                difference = ray.origin.z - transform.position.z;
                magnitude = difference / deltaDirection.z;
                intersection = ray.origin - (magnitude * deltaDirection);
                return intersection;
            case PlaneAxis.XZ:
                difference = ray.origin.y - transform.position.y;
                magnitude = difference / deltaDirection.y;
                intersection = ray.origin - (magnitude * deltaDirection);
                return intersection;
            case PlaneAxis.YZ:
                difference = ray.origin.x - transform.position.x;
                magnitude = difference / deltaDirection.x;
                intersection = ray.origin - (magnitude * deltaDirection);
                return intersection;
        }
        // todo: Account for local rotation as well.
        return transform.position;
    }

    public override void OnSelectOn() {
        meshRenderer.materials[materialID].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, transparencyOverrideAmount);
        offset = GetInteractPoint() - transform.position;
    }

    public override void OnSelectOff() {
        meshRenderer.materials[materialID].color = new Color(originalColor.r, originalColor.g, originalColor.b, transparencyOverrideAmount);
    }
}

public abstract class TransformToolHandleBase : ToolHandle {
    public new TransformTool controller => (TransformTool)base.controller;
}

public enum PlaneAxis {
    XY,
    XZ,
    YZ
}