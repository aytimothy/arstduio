using System.Collections.Generic;
using UnityEngine;

public class BoneTool : Tool {
    public BoneToolbar toolbar;
    public List<Bone> bones;

    void Start() {
        toolbar.gameObject.SetActive(true);
        SetupAvatar();
    }

    void OnDestroy() {
        toolbar.gameObject.SetActive(false);
    }

    public override void HideVisuals() {
        foreach (Bone bone in bones) 
            bone.HideVisuals();
    }

    public override void ShowVisuals() {
        foreach (Bone bone in bones)
            bone.ShowVisuals();
    }

    public void SetupAvatar() {
        SetupAvatar(slave.GetComponentInChildren<SkinnedMeshRenderer>());
    }

    public void SetupAvatar(SkinnedMeshRenderer renderer) {
        if (renderer == null) return;
        foreach (Transform boneTransform in renderer.bones) {
            Bone boneController = boneTransform.gameObject.AddComponent<Bone>();
            bones.Add(boneController);
            boneController.controller = this;
        }
    }

    public void UnsetupAvatar() {
        foreach (Bone bone in bones)
            Destroy(bone);
    }
}
