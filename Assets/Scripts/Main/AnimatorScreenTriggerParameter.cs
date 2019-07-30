using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimatorScreenTriggerParameter : AnimatorScreenParameterBase {
    public TMP_Text text;

    public override void inputControl_OnValueChanged() {
        if (suppressUpdates) return;
        animator.SetTrigger(parameterName);
    }

    public override AnimatorControllerParameterType type => AnimatorControllerParameterType.Trigger;
    public override string GetValue() {
        return animator.GetBool(parameterName).ToString();
    }

    public override void Init(Animator animator, ARObjectAnimationParameters parameter) {
        this.animator = animator;
        parameterName = parameter.name;
        text.text = parameterName;
    }

    public override void Init(SkinnedMeshRenderer skinnedMeshRenderer, ARObjectBlendshapeDescriptor parameter) {
        throw new NotSupportedException();
    }
}
