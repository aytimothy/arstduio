using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorScreenParameterBase : MonoBehaviour {
    public Animator animator;
    public new SkinnedMeshRenderer renderer;
    public string parameterName;
    protected bool suppressUpdates;

    public abstract void inputControl_OnValueChanged();
    public abstract AnimatorControllerParameterType type { get; }
    public abstract string GetValue();
    public abstract void Init(Animator animator, ARObjectAnimationParameters parameter);
    public abstract void Init(SkinnedMeshRenderer skinnedMeshRenderer, ARObjectBlendshapeDescriptor parameter);
}
