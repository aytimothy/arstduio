using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTool : Tool {
    public AnimatorScreen animatorScreen => controller.animatorScreen;

    protected override void Start() {
        base.Start();
        animatorScreen.Show(slave);
        controller.currentSelectedObject = null;
    }

    public override void ShowVisuals() {
        // there's nothing to show! lol
    }

    public override void HideVisuals() {
        // there's nothing to hide! lol
    }
}
