using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToolPlane : ScaleToolHandleBase {
    public int materialID;
    public Color originalColor;
    public float transparencyOverrideAmount;
    public Color highlightColor => controller.highlightColor;
    public MeshRenderer meshRenderer;
    public PlaneAxis axis;

    public float axisSize = 0.75f;
    public Vector3 originalScale;
    public float pressPoint;

    void Start() {
        originalColor = meshRenderer.materials[materialID].color;
    }

    public override void SelectedUpdate() {
        Vector3 cursorPoint = GetInteractPoint();
        float rawDistance = Vector3.Distance(cursorPoint, transform.position);
        float distanceScale = rawDistance / pressPoint;
        switch (axis) {
            case PlaneAxis.XY:
                transform.localScale = new Vector3(distanceScale * originalScale.x, distanceScale * originalScale.y, originalScale.z);
                break;
            case PlaneAxis.XZ:
                transform.localScale = new Vector3(distanceScale * originalScale.x, originalScale.y, distanceScale * originalScale.z);
                break;
            case PlaneAxis.YZ:
                transform.localScale = new Vector3(originalScale.x, distanceScale * originalScale.y, distanceScale * originalScale.z);
                break;
        }
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
                break;
            case PlaneAxis.XZ:
                difference = ray.origin.y - transform.position.y;
                magnitude = difference / deltaDirection.y;
                intersection = ray.origin - (magnitude * deltaDirection);
                break;
            case PlaneAxis.YZ:
                difference = ray.origin.x - transform.position.x;
                magnitude = difference / deltaDirection.x;
                intersection = ray.origin - (magnitude * deltaDirection);
                break;
        }

        return intersection;
    }

    public override void OnSelectOn() {
        meshRenderer.materials[materialID].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, transparencyOverrideAmount);
        originalScale = transform.localScale;
        pressPoint = Vector3.Distance(GetInteractPoint(), transform.position);
    }

    public override void OnSelectOff() {
        meshRenderer.materials[materialID].color = new Color(originalColor.r, originalColor.g, originalColor.b, transparencyOverrideAmount);
    }
}
