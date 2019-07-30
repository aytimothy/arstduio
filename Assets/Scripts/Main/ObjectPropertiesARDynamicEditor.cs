using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPropertiesARDynamicEditor  : ObjectPropertiesEditor {
    public ObjectPropertiesScreen thisScreen => parent;
    public MenuScreen behindScreen => parent.menuScreen;
    public AnimatorScreen animatorScreen;
    public ObjectAnimationScreen animationScreen;

    [Header("UI Elements")]
    public Toggle useAnimatorToggle;

    [SerializeField]
    bool suppressUpdate;

	public override void LoadMenu() {
        LoadState();
    }

    public void LoadState() {
        suppressUpdate = true;
        Animator animator = arObject.GetComponent<Animator>();
        if (animator != null) useAnimatorToggle.isOn = animator.enabled;
        suppressUpdate = false;
    }

    public void useAnimatorToggle_OnToggle(bool newValue) {
        if (suppressUpdate) return;
        Animator animator = arObject.GetComponent<Animator>();
        if (animator != null) animator.enabled = newValue;
    }

    public void openAnimationPanelButton_OnClick() {
        animationScreen.Show(objectUUID);

    }

    public void openAnimatorPanelButton_OnClick() {
        thisScreen.Hide(false);
        behindScreen.Hide();
        animatorScreen.Show(objectUUID);
    }
}

