using System;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectMenuList : MonoBehaviour {
    public ObjectDatabase objectDatabase;
    public List<CreateObjectButton> buttons;
    public GameObject noObjectsLabel;

    public GameObject buttonPrefab;

    int seenDBVersion = 0;
    public bool UpdateAvaliable() {
        return seenDBVersion != objectDatabase.currentVersionID;
    }

    public void Refresh() {
        seenDBVersion = objectDatabase.currentVersionID;
        foreach (CreateObjectButton button in buttons) Destroy(button.gameObject);
        noObjectsLabel.SetActive(objectDatabase.objects.Count == 0);
        foreach (ObjectDefinition objectDefinition in objectDatabase.objects) {
            GameObject newObjectButtonObject = Instantiate(buttonPrefab, transform);
            CreateObjectButton objectButtonComponent = newObjectButtonObject.GetComponent<CreateObjectButton>();
            if (objectButtonComponent == null) {
                Debug.LogError("Incompatible prefab detected! The button requires a CreateObjectButton component to callback.");
                return;
            }
            buttons.Add(objectButtonComponent);
            objectButtonComponent.UpdateValues(objectDefinition);
        }
    }
}
