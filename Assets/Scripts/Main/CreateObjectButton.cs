using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class CreateObjectButton : MonoBehaviour {
    [Header("Data")]
    public string objectUUID;

    public ObjectDefinition objectDefinition {
        get {
            if (_objectDefinition == null) _objectDefinition = ObjectDatabase.instance.GetObjectByUUID(objectUUID);
            if (_objectDefinition.UUID != objectUUID) _objectDefinition = ObjectDatabase.instance.GetObjectByUUID(objectUUID);
            return _objectDefinition;
        }
    }

    ObjectDefinition _objectDefinition;

    [Header("UI Elements")] public TMP_Text nameText;
    public TMP_Text typeText;
    public Image image;

    public void UpdateValues() {
        nameText.text = objectDefinition.name;
        switch (objectDefinition.type) {
            case ObjectType.None:
                // typeText.text = "None (You should not be seeing or creating assets with this type)";
                typeText.text = "Timothy's a Twat";
                break;
            case ObjectType.Static:
                typeText.text = "Static Prop";
                break;
            case ObjectType.Dynamic:
                typeText.text = "Dynamic Prop";
                break;
            case ObjectType.Character:
                typeText.text = "Usable Character";
                break;
        }
        image.sprite = objectDefinition.image;
    }

    public void UpdateValues(string UUID) {
        objectUUID = UUID;
        UpdateValues();
    }

    public void UpdateValues(ObjectDefinition definition) {
        _objectDefinition = definition;
        objectUUID = definition.UUID;
        UpdateValues();
    }

    public void CreateObjectButton_OnClick() {
        Camera viewportCamera = ARSessionManager.instance.cam.GetComponent<Camera>();
        Ray ray = viewportCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit[] hits = Physics.RaycastAll(ray, 20f);
        if (hits == null) {
            NativeToolkit.ShowAlert("Error", "Cannot place a character when there is no ground.");
            return;
        }
        if (hits.Length == 0) {
            NativeToolkit.ShowAlert("Error", "Cannot place a character when there is no ground.");
            return;
        }

        foreach (RaycastHit raycastHit in hits) {
            ARPlane plane = raycastHit.collider.gameObject.GetComponent<ARPlane>();
            if (plane == null) continue;        // Not a plane, move on.
            GameObject newObject = Instantiate(objectDefinition.prefab, raycastHit.point, Quaternion.identity, ARSessionManager.instance.sessionController.transform);
            ObjectDescriptor objectDescriptor = newObject.GetComponent<ObjectDescriptor>();
            if (objectDescriptor != null)
                if (objectDescriptor.baseUUID != objectUUID) Debug.LogWarning("There is a mismatch in the ID of the prefab in this object definition!\nThe prefab has the ID \"" + objectDescriptor.baseUUID + "\" but we were expecting \"" + objectUUID + "\".");
            ARObject controller = null;
            switch (objectDefinition.type) {
                case ObjectType.None:
                    controller = newObject.AddComponent<ARObject>();
                    break;
                case ObjectType.Static:
                    controller = newObject.AddComponent<ARStatic>();
                    break;
                case ObjectType.Dynamic:
                    controller = newObject.AddComponent<ARDynamic>();
                    break;
                case ObjectType.Character:
                    controller = newObject.AddComponent<ARCharacter>();
                    break;
            }

            controller.baseUUID = objectUUID;
            controller.UUID = GenerateUUID();
            return;
        }
    }

    public string GenerateUUID() {
        SHA256 hasher = SHA256.Create();
        byte[] hashBytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(DateTime.Now.ToString()));
        return TextUtilities.ByteArrayToString(hashBytes);
    }
}

