using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimatorScreenBlendshapeEditor : AnimatorScreenParameterBase {
    [Header("UI Elements")]
    public TMP_Text label;
    public TMP_InputField input;

    [Header("Data")]
    public int index;

    public override void inputControl_OnValueChanged() {
        if (suppressUpdates) return;
        float value;
        if (float.TryParse(input.text, out value))
            renderer.SetBlendShapeWeight(index, value);
    }

    public override AnimatorControllerParameterType type => AnimatorControllerParameterType.Float;
    public override string GetValue() {
        return input.text.ToString();
    }

    public override void Init(Animator animator, ARObjectAnimationParameters parameter) {
        throw new NotSupportedException();
    }

    public override void Init(SkinnedMeshRenderer skinnedMeshRenderer, ARObjectBlendshapeDescriptor parameter) {
        renderer = skinnedMeshRenderer;
        label.text = parameter.name;
        index = parameter.index;
        suppressUpdates = true;
        input.text = renderer.GetBlendShapeWeight(index).ToString();
        suppressUpdates = false;
    }
}
