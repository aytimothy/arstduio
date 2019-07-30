using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageObjectsScreen : MonoBehaviour {
    public SideMenuAnimator menuAnimator;
    public MenuScreen menuScreen;
    public ManageObjectsList list;
    public CreateObjectScreen createScreen;

    public void Show() {
        menuAnimator.Show();
        menuScreen.Hide();

        if (list.UpdateAvaliable()) list.Refresh();
    }

    public void Hide() {
        menuAnimator.Hide();
        menuScreen.Show();
    }

    public void Toggle() {
        menuAnimator.Toggle();
        Debug.LogWarning("Try not to Toggle() - There is no on/off-specific events here. Stuff may (will) break.");
    }

    public void SpawnSomethingButton_OnClick() {
        Hide();
        createScreen.Show();
    }
}
