using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPropertiesCharacterEditor : ObjectPropertiesEditor {
    [Header("UI Elements")]
    public TMP_Text ownershipOwnerLabel;
    public Toggle useAIToggle;

	public override void LoadMenu() {
		// Do nothing, for now.
	}

    public void useAIToggle_OnToggle(bool newValue) {

    }

    public void TakeOwnershipButton_OnClick() {

    }

    public void ControlButton_OnClick() {

    }
}
