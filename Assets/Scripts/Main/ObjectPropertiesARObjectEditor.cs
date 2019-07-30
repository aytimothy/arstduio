using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPropertiesARObjectEditor : ObjectPropertiesEditor {
    [Header("UI Elements")]
    public TMP_InputField xPosInputField;
    public TMP_InputField yPosInputField;
    public TMP_InputField zPosInputField;
    public TMP_InputField xScaleInputField;
    public TMP_InputField yScaleInputField;
    public TMP_InputField zScaleInputField;
    public TMP_InputField xEulerRotInputField;
    public TMP_InputField yEulerRotInputField;
    public TMP_InputField zEulerRotInputField;
    public TMP_InputField xQuatRotInputField;
    public TMP_InputField yQuatRotInputField;
    public TMP_InputField zQuatRotInputField;
    public TMP_InputField wQuatRotInputField;
    public GameObject quaternionRotContainer;
    public GameObject eulerRotContainer;
    public TMP_InputField nameInputField;
    public TMP_InputField uuidInputField;
    public TMP_InputField baseUuidInputField;
    public Toggle objectActiveToggle;
    public Image uuidUnlockIcon;
    public TMP_Text positionGlobalityText;
    public TMP_Text rotationGlobalityText;
    public TMP_Text rotationCoordinateSystemText;

    [Header("UI References")]
    public Sprite unlockedSprite;
    public Sprite lockedSprite;

    [SerializeField]
    bool suppressUpdate = false;
    [SerializeField]
    bool positionLocal = false;
    [SerializeField]
    bool rotationLocal = false;

    public override void LoadMenu() {
        LoadObjectProperties();
        LoadEditorToggles();
    }

    public void LoadObjectProperties() {
        suppressUpdate = true;
        Vector3 position = Vector3.zero;
        if (positionLocal)
            position = arObject.transform.localPosition;
        if (!positionLocal)
            position = arObject.transform.position;

        xPosInputField.text = position.x.ToString();
        yPosInputField.text = position.y.ToString();
        zPosInputField.text = position.z.ToString();

        Vector3 euler = Vector3.zero;
        Quaternion quaternion = Quaternion.identity;
        if (rotationLocal) {
            euler = arObject.transform.localEulerAngles;
            quaternion = arObject.transform.localRotation;
        }
        if (!rotationLocal) {
            euler = arObject.transform.eulerAngles;
            quaternion = arObject.transform.rotation;
        }

        xEulerRotInputField.text = euler.x.ToString();
        yEulerRotInputField.text = euler.y.ToString();
        zEulerRotInputField.text = euler.z.ToString();
        xQuatRotInputField.text = quaternion.x.ToString();
        yQuatRotInputField.text = quaternion.y.ToString();
        zQuatRotInputField.text = quaternion.z.ToString();
        wQuatRotInputField.text = quaternion.w.ToString();

        Vector3 localScale = arObject.transform.localScale;
        xScaleInputField.text = localScale.x.ToString();
        yScaleInputField.text = localScale.y.ToString();
        zScaleInputField.text = localScale.z.ToString();
        objectActiveToggle.isOn = arObject.gameObject.activeSelf;

        nameInputField.text = arObject.name;
        uuidInputField.text = arObject.UUID;
        baseUuidInputField.text = arObject.baseUUID;

        suppressUpdate = false;
    }

    public void LoadEditorToggles() {
        positionGlobalityText.text = (positionLocal) ? "L" : "W";
        rotationGlobalityText.text = (rotationLocal) ? "L" : "W";
        rotationCoordinateSystemText.text = (useEuler) ? "E" : "Q";
        uuidInputField.interactable = uuidFieldUnlocked;
        uuidUnlockIcon.sprite = (uuidFieldUnlocked) ? unlockedSprite : lockedSprite;
    }

    public void xPosInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Vector3 original;
            Vector3 newPosition;
            if (positionLocal) {
                original = arObject.transform.localPosition;
                newPosition = new Vector3(value, original.y, original.z);
                arObject.transform.localPosition = newPosition;
            }
            if (!positionLocal) {
                original = arObject.transform.position;
                newPosition = new Vector3(value, original.y, original.z);
                arObject.transform.position = newPosition;
            }
        }
    }

    public void yPosInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Vector3 original;
            Vector3 newPosition;
            if (positionLocal) {
                original = arObject.transform.localPosition;
                newPosition = new Vector3(original.x, value, original.z);
                arObject.transform.localPosition = newPosition;
            }
            if (!positionLocal) {
                original = arObject.transform.position;
                newPosition = new Vector3(original.x, value, original.z);
                arObject.transform.position = newPosition;
            }
        }
    }

    public void zPosInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Vector3 original;
            Vector3 newPosition;
            if (positionLocal) {
                original = arObject.transform.localPosition;
                newPosition = new Vector3(original.x, original.y, value);
                arObject.transform.localPosition = newPosition;
            }
            if (!positionLocal) {
                original = arObject.transform.position;
                newPosition = new Vector3(original.x, original.y, value);
                arObject.transform.position = newPosition;
            }
        }
    }

    public void xScaleInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Vector3 original = arObject.transform.localScale;
            Vector3 newScale = new Vector3(value, original.y, original.z);
            arObject.transform.localScale = newScale;
        }
    }

    public void yScaleInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Vector3 original = arObject.transform.localScale;
            Vector3 newScale = new Vector3(original.x, value, original.z);
            arObject.transform.localScale = newScale;
        }
    }

    public void zScaleInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Vector3 original = arObject.transform.localScale;
            Vector3 newScale = new Vector3(original.x, original.y, value);
            arObject.transform.localScale = newScale;
        }
    }

    public void xEulerRotInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;
        if (!useEuler) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Vector3 original = Vector3.zero;
            Vector3 newAngle = Vector3.zero;
            Quaternion quaternion = Quaternion.identity;
            if (rotationLocal) {
                original = arObject.transform.localEulerAngles;
                newAngle = new Vector3(value, original.y, original.z);
                arObject.transform.localEulerAngles = newAngle;
                quaternion = arObject.transform.localRotation;
            }
            if (!rotationLocal) {
                original = arObject.transform.eulerAngles;
                newAngle = new Vector3(value, original.y, original.z);
                arObject.transform.eulerAngles = newAngle;
                quaternion = arObject.transform.rotation;
            }

            xQuatRotInputField.text = quaternion.x.ToString();
            yQuatRotInputField.text = quaternion.y.ToString();
            zQuatRotInputField.text = quaternion.z.ToString();
            wQuatRotInputField.text = quaternion.w.ToString();
        }
    }

    public void yEulerRotInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;
        if (!useEuler) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Vector3 original = Vector3.zero;
            Vector3 newAngle = Vector3.zero;
            Quaternion quaternion = Quaternion.identity;
            if (rotationLocal) {
                original = arObject.transform.localEulerAngles;
                newAngle = new Vector3(original.x, value, original.z);
                arObject.transform.localEulerAngles = newAngle;
                quaternion = arObject.transform.localRotation;
            }
            if (!rotationLocal) {
                original = arObject.transform.eulerAngles;
                newAngle = new Vector3(original.x, value, original.z);
                arObject.transform.eulerAngles = newAngle;
                quaternion = arObject.transform.rotation;
            }

            xQuatRotInputField.text = quaternion.x.ToString();
            yQuatRotInputField.text = quaternion.y.ToString();
            zQuatRotInputField.text = quaternion.z.ToString();
            wQuatRotInputField.text = quaternion.w.ToString();
        }
    }

    public void zEulerRotInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;
        if (!useEuler) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Vector3 original = Vector3.zero;
            Vector3 newAngle = Vector3.zero;
            Quaternion quaternion = Quaternion.identity;
            if (rotationLocal) {
                original = arObject.transform.localEulerAngles;
                newAngle = new Vector3(original.x, original.y, value);
                arObject.transform.localEulerAngles = newAngle;
                quaternion = arObject.transform.localRotation;
            }
            if (!rotationLocal) {
                original = arObject.transform.eulerAngles;
                newAngle = new Vector3(original.x, original.y, value);
                arObject.transform.eulerAngles = newAngle;
                quaternion = arObject.transform.rotation;
            }

            xQuatRotInputField.text = quaternion.x.ToString();
            yQuatRotInputField.text = quaternion.y.ToString();
            zQuatRotInputField.text = quaternion.z.ToString();
            wQuatRotInputField.text = quaternion.w.ToString();
        }
    }

    public void xQuatRotInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;
        if (useEuler) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Quaternion original = Quaternion.identity;
            Quaternion newAngle = Quaternion.identity;
            Vector3 euler = Vector3.zero;
            if (rotationLocal) {
                original = arObject.transform.localRotation;
                newAngle = new Quaternion(value, original.y, original.z, original.w);
                arObject.transform.localRotation = newAngle;
                euler = arObject.transform.localEulerAngles;
            }
            if (!rotationLocal) {
                original = arObject.transform.rotation;
                newAngle = new Quaternion(value, original.y, original.z, original.w);
                arObject.transform.rotation = newAngle;
                euler = arObject.transform.eulerAngles;
            }

            xEulerRotInputField.text = euler.x.ToString();
            yEulerRotInputField.text = euler.y.ToString();
            zEulerRotInputField.text = euler.z.ToString();
        }
    }

    public void yQuatRotInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;
        if (useEuler) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Quaternion original = Quaternion.identity;
            Quaternion newAngle = Quaternion.identity;
            Vector3 euler = Vector3.zero;
            if (rotationLocal) {
                original = arObject.transform.localRotation;
                newAngle = new Quaternion(original.x, value, original.z, original.w);
                arObject.transform.localRotation = newAngle;
                euler = arObject.transform.localEulerAngles;
            }
            if (!rotationLocal) {
                original = arObject.transform.rotation;
                newAngle = new Quaternion(original.x, value, original.z, original.w);
                arObject.transform.rotation = newAngle;
                euler = arObject.transform.eulerAngles;
            }

            xEulerRotInputField.text = euler.x.ToString();
            yEulerRotInputField.text = euler.y.ToString();
            zEulerRotInputField.text = euler.z.ToString();
        }
    }

    public void zQuatRotInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;
        if (useEuler) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Quaternion original = Quaternion.identity;
            Quaternion newAngle = Quaternion.identity;
            Vector3 euler = Vector3.zero;
            if (rotationLocal) {
                original = arObject.transform.localRotation;
                newAngle = new Quaternion(original.x, original.y, value, original.w);
                arObject.transform.localRotation = newAngle;
                euler = arObject.transform.localEulerAngles;
            }
            if (!rotationLocal) {
                original = arObject.transform.rotation;
                newAngle = new Quaternion(original.x, original.y, value, original.w);
                arObject.transform.rotation = newAngle;
                euler = arObject.transform.eulerAngles;
            }

            xEulerRotInputField.text = euler.x.ToString();
            yEulerRotInputField.text = euler.y.ToString();
            zEulerRotInputField.text = euler.z.ToString();
        }
    }

    public void wQuatRotInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;
        if (useEuler) return;

        float value;
        if (float.TryParse(newValue, out value)) {
            Quaternion original = Quaternion.identity;
            Quaternion newAngle = Quaternion.identity;
            Vector3 euler = Vector3.zero;
            if (rotationLocal) {
                original = arObject.transform.localRotation;
                newAngle = new Quaternion(original.x, original.y, original.z, value);
                arObject.transform.localRotation = newAngle;
                euler = arObject.transform.localEulerAngles;
            }
            if (!rotationLocal) {
                original = arObject.transform.rotation;
                newAngle = new Quaternion(original.x, original.y, original.z, value);
                arObject.transform.rotation = newAngle;
                euler = arObject.transform.eulerAngles;
            }

            xEulerRotInputField.text = euler.x.ToString();
            yEulerRotInputField.text = euler.y.ToString();
            zEulerRotInputField.text = euler.z.ToString();
        }
    }

    public void nameInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;

        arObject.name = newValue;
    }

    public void uuidInputField_OnValueChanged(string newValue) {
        if (suppressUpdate) return;
        if (!uuidFieldUnlocked) return;

        arObject.UUID = newValue;
    }

    public void positionGlobalityToggleButton_OnClick() {
        positionLocal = !positionLocal;
        suppressUpdate = true;
        Vector3 position = Vector3.zero;
        position = (positionLocal) ? arObject.transform.localPosition : arObject.transform.position;
        xPosInputField.text = position.x.ToString();
        yPosInputField.text = position.y.ToString();
        zPosInputField.text = position.z.ToString();
        suppressUpdate = false;
        positionGlobalityText.text = (positionLocal) ? "L" : "W";
        // for "Local" and "World"
    }

    public void rotationGlobalityToggleButton_OnClick() {
        rotationLocal = !rotationLocal;
        suppressUpdate = true;
        Vector3 eulerAngles = Vector3.zero;
        Quaternion quaternion = Quaternion.identity;
        if (rotationLocal) {
            eulerAngles = arObject.transform.localEulerAngles;
            quaternion = arObject.transform.localRotation;
        }
        if (!rotationLocal) {
            eulerAngles = arObject.transform.eulerAngles;
            quaternion = arObject.transform.rotation;
        }

        xEulerRotInputField.text = eulerAngles.x.ToString();
        yEulerRotInputField.text = eulerAngles.y.ToString();
        zEulerRotInputField.text = eulerAngles.z.ToString();
        xQuatRotInputField.text = quaternion.x.ToString();
        yQuatRotInputField.text = quaternion.y.ToString();
        zQuatRotInputField.text = quaternion.z.ToString();
        wQuatRotInputField.text = quaternion.w.ToString();

        suppressUpdate = false;
        rotationGlobalityText.text = (rotationLocal) ? "L" : "W";
        // for "Local" and "World"
    }

    [SerializeField]
    bool useEuler = true;

    public void rotationAngleSystemToggleButton_OnClick() {
        useEuler = !useEuler;

        quaternionRotContainer.gameObject.SetActive(!useEuler);
        eulerRotContainer.gameObject.SetActive(useEuler);
        rotationCoordinateSystemText.text = (useEuler) ? "E" : "Q";
        // for "Euler" and "Quaternion".
    }

    [SerializeField]
    bool uuidFieldUnlocked = false;

    public void uuidUnlockButton_OnClick() {
        uuidFieldUnlocked = !uuidFieldUnlocked;

        if (uuidFieldUnlocked) {
            uuidUnlockIcon.sprite = unlockedSprite;
        }
        if (!uuidFieldUnlocked) {
            uuidUnlockIcon.sprite = lockedSprite;
        }

        uuidInputField.interactable = uuidFieldUnlocked;
    }

    public void objectActiveToggle_OnToggle(bool newValue) {
        if (suppressUpdate) return;
        arObject.gameObject.SetActive(newValue);
    }
}
