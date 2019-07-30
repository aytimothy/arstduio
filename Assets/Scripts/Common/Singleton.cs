using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Class - Describes a MonoBehaviour that should only have one instance alive at any one time.
/// </summary>
/// <typeparam name="T">The MonoBehaviour that should only have one instance of.</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    public static T instance;

    public virtual void Awake() {
	    instance = (T)this;
    }
}