using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageObjectsList : MonoBehaviour {
    public GameObject buttonPrefab;
    public List<ManageObjectButton> buttons;
    public ObjectManager objectManager;
    public GameObject noObjectDisplay;
    public ObjectRenamePanel objectRenamePanel;
    public ObjectPropertiesScreen objectPropertiesScreen;
    public ManageObjectsScreen parent;

    void Start() {
        CheckForNoObjects();
    }

    bool CheckForNoObjects() {
        bool noObjects = objectManager.objects == null;
        if (!noObjects) noObjects = objectManager.objects.Count <= 0;
        noObjectDisplay.SetActive(noObjects);
        return noObjects;
    }

    public void Refresh() {
        foreach (ManageObjectButton button in buttons) Destroy(button.gameObject);

        if (CheckForNoObjects()) return;

        foreach (ARObject arObject in objectManager.objects) {
            GameObject newButtonObject = Instantiate(buttonPrefab, transform);
            ManageObjectButton newButtonController = newButtonObject.GetComponent<ManageObjectButton>();
            newButtonController.controller = this;
            newButtonController.arObject = arObject;
            newButtonController.Refresh(arObject.UUID);
            buttons.Add(newButtonController);
        }
    }

    public bool UpdateAvaliable() {
        return true;
    }

    public void ShowRenamePanel(string objectUUID) {
        objectRenamePanel.Show(objectUUID);
    }

    public void ShowPropertiesPanel(string objectUUID) {
        objectPropertiesScreen.Show(objectUUID);
        parent.Hide();
    }
}
