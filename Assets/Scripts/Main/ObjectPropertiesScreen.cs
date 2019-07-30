using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;

public class ObjectPropertiesScreen : MonoBehaviour {
    #region Initialization
    public SideMenuAnimator menuAnimator;

    public string activelyEditedObjectUUID;
    public ARObject activelyEditedObject {
        get {
            if (_activelyEditedObject == null) _activelyEditedObject = ObjectManager.GetObjectByUUID(activelyEditedObjectUUID);
            // there'll probably be some cases or twats that will be able get a NullObjectReference to trigger here.
            if (_activelyEditedObject.UUID != activelyEditedObjectUUID) _activelyEditedObject = ObjectManager.GetObjectByUUID(activelyEditedObjectUUID);
            return _activelyEditedObject;
        }
        set {
            _activelyEditedObject = value;
            activelyEditedObjectUUID = value.UUID;
        }
    }
    ARObject _activelyEditedObject;
    public List<ObjectPropertiesEditor> editors;
    public Transform editorParent;

    [Header("Passed References")]
    public ObjectAnimationScreen objectAnimationScreen;
    public AnimatorScreen animatorScreen;
    public MenuScreen menuScreen;

    public void Show(string objectUUID) {
        menuAnimator.Show();
        menuScreen.Hide();
        PrepareMenu(objectUUID);
    }

    public void Hide(bool isReturnCall = true) {
        menuAnimator.Hide();
        if (isReturnCall) menuScreen.Show();
        ClearAllMenus();
    }

    public void Toggle() {
        menuAnimator.Toggle();
    }

    public void PrepareMenu(string objectUUID) {
        activelyEditedObjectUUID = objectUUID;
        if (activelyEditedObject == null) {
            Debug.LogError("Could not find any objects with the UUID: \"" + objectUUID + "\".");
            return;
        }
        PrepareMenu(objectUUID, activelyEditedObject.definition.type);
    }

    public void PrepareMenu(string objectUUID, ObjectType objectType) {
        activelyEditedObjectUUID = objectUUID;
        if (activelyEditedObject == null) {
            Debug.LogError("Could not find any objects with the UUID: \"" + objectUUID + "\".");
            return;
        }

        ClearAllMenus();

        GameObject newEditorObject;
        ObjectPropertiesEditor newEditorBaseComponent;
        Debug.Log(objectType);
        switch (objectType) {
            case ObjectType.None:
                if (_activelyEditedObject != null) PrepareBasicMenu(objectUUID);
                if (_activelyEditedObject == null) PrepareNullMenu();
                break;
            case ObjectType.Static:
                PrepareBasicMenu(objectUUID);
                PrepareStaticMenu(objectUUID);
                PrepareFeatureMenu(objectUUID);
                break;
            case ObjectType.Dynamic:
                PrepareBasicMenu(objectUUID);
                PrepareStaticMenu(objectUUID);
                PrepareDynamicMenu(objectUUID);
                PrepareFeatureMenu(objectUUID);
                break;
            case ObjectType.Character:
                PrepareBasicMenu(objectUUID);
                PrepareStaticMenu(objectUUID);
                PrepareDynamicMenu(objectUUID);
                PrepareCharacterMenu(objectUUID);
                PrepareFeatureMenu(objectUUID);
                break;
        }
    }

    public void ClearAllMenus() {
        foreach (ObjectPropertiesEditor editor in editors)
            Destroy(editor.gameObject);
        editors.Clear();
    }

    public void PrepareNullMenu() {
        GameObject newNullMenuObject = Instantiate(ObjectPropertiesNullContainer, editorParent);
        ObjectPropertiesNullEditor newNullEditor = newNullMenuObject.GetComponent<ObjectPropertiesNullEditor>();
        newNullEditor.Init("", this);
        editors.Add(newNullEditor);
    }

    public void PrepareBasicMenu(string UUID) {
        GameObject newBasicMenuObject = Instantiate(ObjectPropertiesBasicContainer, editorParent);
        ObjectPropertiesARObjectEditor newBasicEditor = newBasicMenuObject.GetComponent<ObjectPropertiesARObjectEditor>();
        newBasicEditor.Init(UUID, this);
        editors.Add(newBasicEditor);
    }

    public void PrepareStaticMenu(string UUID) {
        GameObject newStaticMenuObject = Instantiate(ObjectPropertiesStaticContainer, editorParent);
        ObjectPropertiesARStaticEditor newStaticEditor = newStaticMenuObject.GetComponent<ObjectPropertiesARStaticEditor>();
        newStaticEditor.Init(UUID, this);
        editors.Add(newStaticEditor);
    }

    public void PrepareDynamicMenu(string UUID) {
        GameObject newDynamicMenuObject = Instantiate(ObjectPropertiesDynamicContainer, editorParent);
        ObjectPropertiesARDynamicEditor newDynamicEditor = newDynamicMenuObject.GetComponent<ObjectPropertiesARDynamicEditor>();
        newDynamicEditor.animationScreen = objectAnimationScreen;
        newDynamicEditor.animatorScreen = animatorScreen;
        newDynamicEditor.Init(UUID, this);
        editors.Add(newDynamicEditor);
    }

    public void PrepareCharacterMenu(string UUID) {
        GameObject newCharacterMenuObject = Instantiate(ObjectPropertiesCharacterContainer, editorParent);
        ObjectPropertiesCharacterEditor newCharacterEditor = newCharacterMenuObject.GetComponent<ObjectPropertiesCharacterEditor>();
        newCharacterEditor.Init(UUID, this);
        editors.Add(newCharacterEditor);
    }

    public void PrepareFeatureMenu(string UUID) {
        GameObject newDynamicFeaturesMenuObject = Instantiate(ObjectPropertiesExtraFeatureContainer, editorParent);
        ObjectPropertiesDynamicFeaturesEditor newDynamicFeatureEditor = newDynamicFeaturesMenuObject.GetComponent<ObjectPropertiesDynamicFeaturesEditor>();
        newDynamicFeatureEditor.Init(UUID, this);
        editors.Add(newDynamicFeatureEditor);
    }
    #endregion
    #region Menu Controls
    [Header("Prefabs")]
    public GameObject ObjectPropertiesNullContainer;
    public GameObject ObjectPropertiesBasicContainer;
    public GameObject ObjectPropertiesStaticContainer;
    public GameObject ObjectPropertiesDynamicContainer;
    public GameObject ObjectPropertiesCharacterContainer;
    public GameObject ObjectPropertiesExtraFeatureContainer;
    #endregion
}
