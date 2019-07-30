using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPropertiesNullEditor : ObjectPropertiesEditor {
    public override void LoadMenu() {
        // do literally nothing.
    }

    public override void Init(string objectUUID) {
        loaded = true;
        LoadMenu();
    }
}
