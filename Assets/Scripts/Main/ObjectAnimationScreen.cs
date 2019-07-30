using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimationScreen : MonoBehaviour {
    public SideMenuAnimator menuAnimator;
    public void Show(string objectUUID) {
        menuAnimator.Show();
    }

    public void Hide() {
        menuAnimator.Hide();
    }

    public void Toggle() {
        menuAnimator.Toggle();
    }
}
