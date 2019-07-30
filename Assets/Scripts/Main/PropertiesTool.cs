using System;
using System.Collections.Generic;
using UnityEngine;

public class PropertiesTool : Tool {
    public ObjectPropertiesScreen propertiesScreen => controller.propertiesScreen;

    protected override void Start() {
        base.Start();
        propertiesScreen.Show(slave.UUID);
        controller.currentSelectedObject = null;
    }

    public override void ShowVisuals() {
        // there's nothing to show! lol
    }

    public override void HideVisuals() {
        // there's nothing to hide! lol
    }
}
