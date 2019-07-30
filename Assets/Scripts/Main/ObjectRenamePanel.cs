using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectRenamePanel : MonoBehaviour {
    public string activeObjectUUID;
    public TMP_InputField inputField;
    public ARObject activeObject {
        get {
            if (_activeObject == null) _activeObject = ObjectManager.GetObjectByUUID(activeObjectUUID);
            if (_activeObject.UUID != activeObjectUUID) _activeObject = ObjectManager.GetObjectByUUID(activeObjectUUID);
            return _activeObject;
        }
        set {
            _activeObject = value;
            activeObjectUUID = value.UUID;
        }
    }
    ARObject _activeObject;

    public void Show(string UUID) {
        activeObjectUUID = UUID;
        gameObject.SetActive(true);
        inputField.text = activeObject.name;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void ConfirmButton_OnClick() {
        activeObject.name = inputField.text;
        Hide();
    }

    public void CancelButton_OnClick() {
        Hide();
    }
}
