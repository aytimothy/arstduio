using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempDirtyLayoutButton : MonoBehaviour {
	public RectTransform imageTransform;
    private bool toggle;
	
	public void dirtyLayoutButton_OnClick() {
        if (toggle) {
            imageTransform.sizeDelta = new Vector2(0, 2);
            toggle = false;
            return;
        }

        if (!toggle) {
            imageTransform.sizeDelta = new Vector2(0, 1);
            toggle = true;
            return;
        }
	}
}
