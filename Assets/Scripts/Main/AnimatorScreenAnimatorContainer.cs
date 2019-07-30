using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScreenAnimatorContainer : MonoBehaviour {
    public AnimatorScreen parent;

    public GameObject intParameterPrefab => parent.intParameterPrefab;
    public GameObject floatParameterPrefab => parent.floatParameterPrefab;
    public GameObject boolParameterPrefab => parent.boolParameterPrefab;
    public GameObject triggerParameterPrefab => parent.triggerParameterPrefab;

    public List<AnimatorScreenParameterBase> parameterEditors;

    public void Init(ARObjectAnimatorDescriptor descriptor) {
        if (parent == null) {
            Debug.LogError("Cannot initialize! A prefab reference from the screen is required!");
            return;
        }

        foreach (ARObjectAnimationParameters parameter in descriptor.animatorParameters) {
            if (parameter.type == ARObjectAnimationParameters.AnimationParameterType.None) continue;

            GameObject newGameObject = null;
            AnimatorScreenParameterBase controller = null;
            switch (parameter.type) {
                case ARObjectAnimationParameters.AnimationParameterType.Integer:
                    newGameObject = Instantiate(intParameterPrefab, transform);
                    controller = newGameObject.GetComponent<AnimatorScreenIntParameter>();
                    break;
                case ARObjectAnimationParameters.AnimationParameterType.Float:
                    newGameObject = Instantiate(floatParameterPrefab, transform);
                    controller = newGameObject.GetComponent<AnimatorScreenFloatParameter>();
                    break;
                case ARObjectAnimationParameters.AnimationParameterType.Boolean:
                    newGameObject = Instantiate(boolParameterPrefab, transform);
                    controller = newGameObject.GetComponent<AnimatorScreenBoolParameter>();
                    break;
                case ARObjectAnimationParameters.AnimationParameterType.Trigger:
                    newGameObject = Instantiate(triggerParameterPrefab, transform);
                    controller = newGameObject.GetComponent<AnimatorScreenTriggerParameter>();
                    break;
            }

            if (controller == null) {
                Debug.LogError("Incompatible prefab found!");
                return;
            }

            controller.Init(descriptor.animationController, parameter);
            parameterEditors.Add(controller);
        }
    }

    public void Init(ARObjectAnimatorDescriptor descriptor, AnimatorScreen screen) {
        parent = screen;
        Init(descriptor);
    }
}
