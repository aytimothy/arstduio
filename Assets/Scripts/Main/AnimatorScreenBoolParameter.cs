using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorScreenBoolParameter : AnimatorScreenParameterBase {
    public Toggle toggle;
    public TMP_Text text;

    public override void inputControl_OnValueChanged() {
        if (suppressUpdates) return;
        animator.SetBool(parameterName, toggle.isOn);
    }

    public override AnimatorControllerParameterType type => AnimatorControllerParameterType.Bool;
    public override string GetValue() {
        return animator.GetBool(parameterName).ToString();
    }

    public override void Init(Animator animator, ARObjectAnimationParameters parameter) {
        this.animator = animator;
        suppressUpdates = true;
        parameterName = parameter.name;
        text.text = parameterName;
        bool value;
        if (bool.TryParse(GetValue(), out value))
            toggle.isOn = value;
        suppressUpdates = false;
    }

    public override void Init(SkinnedMeshRenderer skinnedMeshRenderer, ARObjectBlendshapeDescriptor parameter) {
        throw new NotSupportedException();
    }
}
