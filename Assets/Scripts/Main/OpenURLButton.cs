using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLButton : MonoBehaviour {
    public string destination;

    public void openURLButton_OnClick() {
        Application.OpenURL(destination);
    }
}
