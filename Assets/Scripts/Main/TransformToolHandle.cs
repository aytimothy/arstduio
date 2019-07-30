using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TransformToolHandle : TransformToolHandleBase {
    public int materialID;
    public Color originalColor;
    public Color highlightColor => controller.highlightColor;
    public MeshRenderer meshRenderer;
    public Axis axis;
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
        Vector3 point = Vector3.zero;
        switch (axis) {
            case Axis.X:
                difference = ray.origin.y - transform.position.y;
                magnitude = difference / deltaDirection.y;
                intersection = ray.origin - (magnitude * deltaDirection);
                point = new Vector3(intersection.x, controller.slave.transform.position.y, controller.slave.transform.position.z);
                return point;
            case Axis.Y:
                difference = ray.origin.x - transform.position.x;
                magnitude = difference / deltaDirection.x;
                intersection = ray.origin - (magnitude * deltaDirection);
                point = new Vector3(controller.slave.transform.position.x, intersection.y, controller.slave.transform.position.z);
                return point;
            case Axis.Z:
                difference = ray.origin.y - transform.position.y;
                magnitude = difference / deltaDirection.y;
                intersection = ray.origin - (magnitude * deltaDirection);
                point = new Vector3(controller.slave.transform.position.x, controller.slave.transform.position.y, intersection.z);
                return point;
        }

        return transform.position;
    }

    public override void OnSelectOn() {
        meshRenderer.materials[materialID].color = highlightColor;
        offset = GetInteractPoint() - transform.position;
    }

    public override void OnSelectOff() {
        meshRenderer.materials[materialID].color = originalColor;
    }
}

public enum Axis {
    None,
    X,
    Y,
    Z,
    W
}

//See UnityEngine.IMGUI.Controls.PrimitiveBoundsHandle.Axes
public enum Axes {
    None = 0,
    X = 1,
    Y = 2,
    Z = 4,
    XY = 3,
    XZ = 5,
    YX = 6,
    All = 7,
    XYZ = All
}