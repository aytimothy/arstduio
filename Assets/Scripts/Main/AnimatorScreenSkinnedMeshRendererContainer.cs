using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScreenSkinnedMeshRendererContainer : MonoBehaviour {
    public AnimatorScreen parent;
    public List<AnimatorScreenBlendshapeEditor> editors = new List<AnimatorScreenBlendshapeEditor>();

    public GameObject parameterPrefab => parent.blendshapePrefab;

    public void Init(AROBjectSkinnedMeshRendererDescriptor descriptor, AnimatorScreen screen) {
        parent = screen;
        Init(descriptor);
    }

    public void Init(AROBjectSkinnedMeshRendererDescriptor descriptor) {
        foreach (ARObjectBlendshapeDescriptor blendshape in descriptor.blendShapes) {
            GameObject newParameterObject = Instantiate(parameterPrefab, transform);
            AnimatorScreenBlendshapeEditor component = newParameterObject.GetComponent<AnimatorScreenBlendshapeEditor>();
            component.Init(descriptor.renderer, blendshape);
            editors.Add(component);
        }
    }
}
