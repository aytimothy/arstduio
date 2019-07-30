using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

/// <summary>
/// Holds information about an Object instance. This is the controller class that adds brain to the brainless ObjectDescriptor.
/// </summary>
public abstract class ARObject : MonoBehaviour {
    public Collider collider;
    public ObjectDefinition definition {
        get {
            if (_definition == null) _definition = ObjectDatabase.instance.GetObjectByUUID(baseUUID);
            if (_definition.UUID != baseUUID) _definition = ObjectDatabase.instance.GetObjectByUUID(baseUUID);
            return _definition;
        }
    }
    ObjectDefinition _definition;
    public string baseUUID;             // This is the object-specific UUID - It is specific to all objects of the same type.
    public string UUID;                 // This is a project-specific UUID - The one given to this particular instance. This is unique between instances.
    public ObjectType type => _definition.type;

    protected virtual void Awake() {
        ObjectManager.Register(this);

        CheckForCollider();
    }

    protected virtual void CheckForCollider() {
        if (collider == null) collider = GetComponent<Collider>();
        if (collider == null) {
            collider = gameObject.AddComponent<BoxCollider>();
        }
    }

    #region Highlight Code

    public Color outlineColor => ActiveObjectOutliner.outlineColor;
    public float outlineWidth => ActiveObjectOutliner.outlineWidth;

    public virtual void Highlight() {
        Shader outlineShader = Shader.Find("Outline");
        if (outlineShader == null) {
            Debug.LogError("Cannot find outline shader.");
            return;
        }

        Renderer[] baseRenderers = GetComponents<Renderer>();
        Renderer[] otherRenderers = GetComponentsInChildren<Renderer>();
        List<Renderer> renderers = new List<Renderer>();
        foreach (Renderer baseRenderer in baseRenderers)
            renderers.Add(baseRenderer);
        foreach (Renderer otherRenderer in otherRenderers)
            renderers.Add(otherRenderer);
        if (renderers.Count == 0) {
            Debug.LogError("Cannot highlight nothing!");
            return;
        }
        foreach (Renderer renderer in renderers) {
            OriginalMaterialData originalRendererData = new OriginalMaterialData();
            originalRendererData.renderer = renderer;
            originalRendererData.materials = new List<Material>();
            for (int i = 0; i < renderer.materials.Length; i++) {
                originalRendererData.materials.Add(renderer.materials[i]);
                Material newMaterial = new Material(outlineShader);
                newMaterial.SetColor("_Color", renderer.materials[i].GetColor("_Color"));
                newMaterial.SetColor("_ASEOutlineColor", outlineColor);
                newMaterial.SetFloat("_ASEOutlineWidth", outlineWidth);
                newMaterial.SetTexture("_Albedo", renderer.materials[i].GetTexture("_MainTex"));
                renderer.materials[i] = newMaterial;
            }
            originalMaterials.Add(originalRendererData);
        }
    }

    protected List<OriginalMaterialData> originalMaterials = new List<OriginalMaterialData>();
    public virtual void Unhighlight() {
        foreach (OriginalMaterialData meshRendererOriginalMaterialData in originalMaterials) if (meshRendererOriginalMaterialData.renderer != null) meshRendererOriginalMaterialData.renderer.materials = meshRendererOriginalMaterialData.materials.ToArray();
        originalMaterials.Clear();
    }
    
    [Serializable]
    protected class OriginalMaterialData {
        public Renderer renderer;
        public List<Material> materials;
    }
    #endregion
}
