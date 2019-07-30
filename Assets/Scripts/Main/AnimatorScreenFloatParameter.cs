using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimatorScreenFloatParameter : AnimatorScreenParameterBase {
    public TMP_InputField inputField;
    public TMP_Text text;

    public override void inputControl_OnValueChanged() {
        if (suppressUpdates) return;
        float newValue;
        if (float.TryParse(inputField.text, out newValue))
            animator.SetFloat(parameterName, newValue);
    }

    public override AnimatorControllerParameterType type => AnimatorControllerParameterType.Float;
    public override string GetValue() {
        return animator.GetFloat(parameterName).ToString();
    }

    public override void Init(Animator animator, ARObjectAnimationParameters parameter) {
        this.animator = animator;
        suppressUpdates = true;
        parameterName = parameter.name;
        text.text = parameterName;
        inputField.text = GetValue();
        suppressUpdates = false;
    }

    public override void Init(SkinnedMeshRenderer skinnedMeshRenderer, ARObjectBlendshapeDescriptor parameter) {
        throw new NotSupportedException();
    }
}
