using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectScreen : MonoBehaviour  {
    public GameObject testCubePrefab;
    public SideMenuAnimator menuAnimator;
    public MenuScreen menuScreen;
    public CreateObjectMenuList objectList;

    public void Show() {
        menuAnimator.Show();
        menuScreen.Hide();

        if (objectList.UpdateAvaliable()) objectList.Refresh();
    }

    public void Hide() {
        menuAnimator.Hide();
        menuScreen.Show();
    }

    public void Toggle() {
        menuAnimator.Toggle();
    }

    #region Test Code
    public void CreateTestCubeButton_OnClick() {

    }
    #endregion
}
