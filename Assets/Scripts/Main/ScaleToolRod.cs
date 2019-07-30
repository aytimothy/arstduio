using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToolRod : ScaleToolHandleBase {

    public Color originalColor;
    public Color highlightColor => controller.highlightColor;
    public LineRenderer lineRenderer;
    public Axis axis;
    public Vector3 originalScale;
    public float pressPoint;
    public float rodLength = 1f;
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
        Vector3 cursorPoint = GetInteractPoint();
        float rawDistance = Vector3.Distance(cursorPoint, transform.position);
        float distanceScale = rawDistance / pressPoint;
        switch (axis) {
            case Axis.X:
                transform.localScale = new Vector3(distanceScale * originalScale.x, originalScale.y, originalScale.z);
                break;
            case Axis.Y:
                transform.localScale = new Vector3(originalScale.x, distanceScale * originalScale.y, originalScale.z);
                break;
            case Axis.Z:
                transform.localScale = new Vector3(originalScale.x, originalScale.y, distanceScale * originalScale.z);
                break;
        }
        // todo: Update handles to work for rotated axii.
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
        Vector3 point = transform.position;
        float distance;

        switch (axis) {
            case Axis.X:
                difference = ray.origin.y - transform.position.y;
                magnitude = difference / deltaDirection.y;
                intersection = ray.origin - (magnitude * deltaDirection);
                point = new Vector3(intersection.x, controller.slave.transform.position.y, controller.slave.transform.position.z);
                break;
            case Axis.Y:
                difference = ray.origin.x - transform.position.x;
                magnitude = difference / deltaDirection.x;
                intersection = ray.origin - (magnitude * deltaDirection);
                point = new Vector3(controller.slave.transform.position.x, intersection.y, controller.slave.transform.position.z);
                break;
            case Axis.Z:
                difference = ray.origin.y - transform.position.y;
                magnitude = difference / deltaDirection.y;
                intersection = ray.origin - (magnitude * deltaDirection);
                point = new Vector3(controller.slave.transform.position.x, controller.slave.transform.position.y, intersection.z);
                break;
        }

        return point;
    }

    public override void OnSelectOn() {
        lineRenderer.materials[0].color = highlightColor;
        originalScale = transform.localScale;
        pressPoint = Vector3.Distance(GetInteractPoint(), transform.position);
    }

    public override void OnSelectOff() {
        lineRenderer.materials[0].color = originalColor;
    }
}
