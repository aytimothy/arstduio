using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class ARDynamic : ARStatic {
    private ARDynamicDescriptor descriptor;
    public Rigidbody rigidbody;
    protected virtual void Start() {
        if (descriptor == null) descriptor = GetComponent<ARDynamicDescriptor>();
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
    }

    public float updateRate;
    public float updateRealSpeed {
        get { return 1f / updateRate; }
        set { updateRate = 1f / value; }
    }
    public float nextUpdateTime;

    protected virtual void Update() {
        UpdateDynamicValues();
        if (Time.time > nextUpdateTime) {
            nextUpdateTime = Time.time + updateRealSpeed;
            UpdateDynamicVariables();
        }
    }

    #region Dynamic Animator Values
    public Transform distanceTarget;
    public float life;
    public float timeSinceLastSave;
    public float lastSavedPersistentTime;
    public float persistentLife {
        get { return lastSavedPersistentTime + timeSinceLastSave; }
    }

    public void UpdateDynamicValues() {
        float deltaTime = Time.deltaTime;
        life += deltaTime;
        timeSinceLastSave += deltaTime;
    }

    public void UpdateDynamicVariables() {
        foreach (ARObjectAnimatorDescriptor animatorDescriptor in descriptor.animatorDescriptors) {
            foreach (ARObjectAnimationParameters animationParameter in animatorDescriptor.animatorParameters) {
                if (animationParameter.isManagedParameter) {
                    switch (animationParameter.managedParameterType) {
                        case ARObjectAnimationParameters.ManagedParameterType.Dummy:
                            // Does nothing.
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.ObjectTransX:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.position.x));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.position.x);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.ObjectTransY:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.position.y));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.position.y);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.ObjectTransZ:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.position.z));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.position.z);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.SelfTransX:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.localPosition.x));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.localPosition.x);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.SelfTransY:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.localPosition.y));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.localPosition.y);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.SelfTransZ:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.localPosition.z));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.localPosition.z);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.ObjectRotX:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.eulerAngles.x));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.eulerAngles.x);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.ObjectRotY:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.eulerAngles.y));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.eulerAngles.y);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.ObjectRotZ:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.eulerAngles.z));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.eulerAngles.z);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.SelfRotX:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.localEulerAngles.x));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.localEulerAngles.x);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.SelfRotY:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.localEulerAngles.y));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.localEulerAngles.y);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.SelfRotZ:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.localEulerAngles.z));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.localEulerAngles.z);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.ObjectScaleX:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.localScale.x));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.localScale.x);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.ObjectScaleY:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.localScale.y));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.localScale.y);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.ObjectScaleZ:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(transform.localScale.z));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, transform.localScale.z);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.ObjectDistance:
                            if (distanceTarget == null) goto case ARObjectAnimationParameters.ManagedParameterType.OriginDistance;
                            float objectDistance = Vector3.Distance(transform.position, distanceTarget.position);
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(objectDistance));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, objectDistance);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.OriginDistance:
                            float originDistance = transform.position.magnitude;
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(originDistance));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, originDistance);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.DeltaX:
                            if (rigidbody == null) break;
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(rigidbody.velocity.x));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, rigidbody.velocity.x);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.DeltaY:
                            if (rigidbody == null) break;
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(rigidbody.velocity.y));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, rigidbody.velocity.y);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.DeltaZ:
                            if (rigidbody == null) break;
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(rigidbody.velocity.z));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, rigidbody.velocity.z);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.DeltaMag:
                            if (rigidbody == null) break;
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(rigidbody.velocity.magnitude));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, rigidbody.velocity.magnitude);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.Life:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(life));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, life);
                            break;
                        case ARObjectAnimationParameters.ManagedParameterType.PersistLife:
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Integer)
                                animatorDescriptor.animationController.SetInteger(animationParameter.name, Mathf.RoundToInt(persistentLife));
                            if (animationParameter.type == ARObjectAnimationParameters.AnimationParameterType.Float)
                                animatorDescriptor.animationController.SetFloat(animationParameter.name, persistentLife);
                            break;
                    }
                }
            }
        }
    }
    #endregion
}