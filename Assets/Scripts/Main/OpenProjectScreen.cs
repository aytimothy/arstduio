using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenProjectScreen : MonoBehaviour {
    public SideMenuAnimator menuAnimator;
    public SettingsScreen settingsScreen;

    public void Show() {
        menuAnimator.Show();
        settingsScreen.Hide();
    }

    public void Hide() {
        menuAnimator.Hide();
        settingsScreen.Show();
    }

    public void Toggle() {
        menuAnimator.Toggle();
    }
}
