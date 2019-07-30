using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneToolbar : MonoBehaviour {
    public BoneTool boneController;

    public void Show(BoneTool controller) {
        boneController = controller;
        gameObject.SetActive(true);
    }

    public void Hide() {
        boneController = null;
        gameObject.SetActive(false);
    }

    public void TransformButton_OnClick() {
        
    }

    public void RotateButton_OnClick() {

    }
}
