using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformToolRod : TransformToolHandleBase {
    public Color originalColor;
    public Color highlightColor => controller.highlightColor;
    public LineRenderer lineRenderer;
    public Axis axis;
    public float rodLength = 0.75f;
    public Vector3 offset;
    public GlobalityState globalityState => controller.globalityState;

    void Start() {
        originalColor = lineRenderer.materials[0].color;
    }

    protected override void Update() {
        base.Update();
        switch (globalityState) {
            case GlobalityState.Global:
                switch (axis) {
                    case Axis.X:
                        lineRenderer.SetPositions(new Vector3[] {
                            transform.position, transform.position + (Vector3.right * rodLength)
                        });
                        break;
                    case Axis.Y:
                        lineRenderer.SetPositions(new Vector3[] {
                            transform.position, transform.position + (Vector3.up * rodLength)
                        });
                        break;
                    case Axis.Z:
                        lineRenderer.SetPositions(new Vector3[] {
                            transform.position, transform.position + (Vector3.forward * rodLength)
                        });
                        break;
                }
                break;
            case GlobalityState.Local:
                switch (axis) {
                    case Axis.X:
                        lineRenderer.SetPositions(new Vector3[] {
                            transform.position, transform.position + (controller.transform.right * rodLength)
                        });
                        break;
                    case Axis.Y:
                        lineRenderer.SetPositions(new Vector3[] {
                            transform.position, transform.position + (controller.transform.up * rodLength)
                        });
                        break;
                    case Axis.Z:
                        lineRenderer.SetPositions(new Vector3[] {
                            transform.position, transform.position + (controller.transform.forward * rodLength)
                        });
                        break;
                }
                break;
            default:
                goto case GlobalityState.Global;
        }
        // todo: Change rods to work for different globality states.
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
        Vector3 intersection;
        Vector3 point;
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
        // todo: Update handles to work for rotated axii.
        return transform.position;
    }

    public override void OnSelectOn() {
        lineRenderer.materials[0].color = highlightColor;
        offset = GetInteractPoint() - transform.position;
    }

    public override void OnSelectOff() {
        lineRenderer.materials[0].color = originalColor;
    }
}