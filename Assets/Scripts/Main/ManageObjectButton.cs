using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ManageObjectButton : MonoBehaviour {
    public string objectUUID;
    public ManageObjectsList controller;
    public ObjectManager manager {
        get { return controller.objectManager; }
    }
    public ARObject arObject;

    public TMP_Text UUIDTextBox;
    public TMP_Text nameTextBox;

    public void ManageObjectButton_OnClick() {
        controller.ShowPropertiesPanel(objectUUID);
    }

    public void DeleteObjectButton_OnClick() {
        Destroy(ObjectManager.GetObjectByUUID(objectUUID).gameObject);
        Destroy(this.gameObject);
    }

    public void RenameObjectButton_OnClick() {
        controller.ShowRenamePanel(objectUUID);
    }

    public void Refresh() {
        if (arObject == null) arObject = ObjectManager.GetObjectByUUID(objectUUID);
        if (arObject != null)
            if (arObject.UUID != objectUUID) arObject = ObjectManager.GetObjectByUUID(objectUUID);
        nameTextBox.text = (arObject == null) ? "<OBJECT NOT FOUND>" : arObject.name;
        UUIDTextBox.text = objectUUID;
    }

    public void Refresh(string UUID) {
        objectUUID = UUID;
        Refresh();
    }
}
