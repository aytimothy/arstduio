using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScreen : MonoBehaviour {
    public SideMenuAnimator menuAnimator;
    public ToolInputManager toolInputManager;
    public Transform animatorGroupRootTransform;
    public string objectUUID;
    public ARObject arObject;
    public GameObject incompatibleGameObject;
    public GameObject panelGameObject;

    [Header("Prefabs")]
    public GameObject intParameterPrefab;
    public GameObject floatParameterPrefab;
    public GameObject boolParameterPrefab;
    public GameObject triggerParameterPrefab;
    public GameObject animatorGroupPrefab;
    public GameObject skinnedMeshContainerPrefab;
    public GameObject blendshapePrefab;

    public List<AnimatorScreenAnimatorContainer> containers;
    public List<AnimatorScreenSkinnedMeshRendererContainer> skinnedMeshContainers;

    public void Show(string objectUUID) {
        menuAnimator.Show();
        toolInputManager.screenVisible = true;
        this.objectUUID = objectUUID;
        arObject = ObjectManager.GetObjectByUUID(objectUUID);
        LoadMenus();
    }

    public void Show(ARObject arObject) {
        menuAnimator.Show();
        toolInputManager.screenVisible = true;
        this.arObject = arObject;
        objectUUID = arObject.UUID;
        LoadMenus();
    }

    public void LoadMenus() {
        Debug.LogWarning("Clean this code up.");
        if (!CheckCompatibility()) return;
        if (arObject == null) {
            Debug.LogError("There is no ARObject to be found!");
            return;
        }
        ARDynamicDescriptor dynamicDescriptor = arObject.GetComponent<ARDynamicDescriptor>();
        if (dynamicDescriptor == null) {
            Debug.LogError("There is no descriptor to be found!");
            return;
        }

        foreach (ARObjectAnimatorDescriptor animatorDescriptor in dynamicDescriptor.animatorDescriptors) {
            GameObject newContainerObject = Instantiate(animatorGroupPrefab, animatorGroupRootTransform);
            AnimatorScreenAnimatorContainer container = newContainerObject.GetComponent<AnimatorScreenAnimatorContainer>();
            container.Init(animatorDescriptor, this);
            containers.Add(container);
        }
        foreach (AROBjectSkinnedMeshRendererDescriptor skinnedMeshDescriptor in dynamicDescriptor.skinnedMeshRendererDescriptors) {
            GameObject newContainerObject = Instantiate(skinnedMeshContainerPrefab, animatorGroupRootTransform);
            AnimatorScreenSkinnedMeshRendererContainer container = newContainerObject.GetComponent<AnimatorScreenSkinnedMeshRendererContainer>();
            container.Init(skinnedMeshDescriptor, this);
            skinnedMeshContainers.Add(container);
        }
    }

    public bool CheckCompatibility() {
        bool compatible = arObject.definition.type >= ObjectType.Dynamic;
        incompatibleGameObject.SetActive(!compatible);
        panelGameObject.SetActive(compatible);
        return compatible;
    }

    public void Hide() {
        menuAnimator.Hide();
        DestroyMenus();
        toolInputManager.screenVisible = false;
        arObject = null;
    }

    public void DestroyMenus() {
        foreach (AnimatorScreenAnimatorContainer containerElement in containers)
            Destroy(containerElement.gameObject);
        containers.Clear();
        foreach(AnimatorScreenSkinnedMeshRendererContainer skinnedMeshRendererContainer in skinnedMeshContainers)
            Destroy(skinnedMeshRendererContainer.gameObject);
        skinnedMeshContainers.Clear();
    }

    public void Toggle() {
        menuAnimator.Toggle();
    }
}
