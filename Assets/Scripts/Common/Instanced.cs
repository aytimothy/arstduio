using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Class - For anything that needs its instances keep tracked of.
/// </summary>
/// <typeparam name="T">The MonoBehaviour to be tracked.</typeparam>
public class Instanced<T> : MonoBehaviour where T : Instanced<T> {
    public static List<T> instances;

    void Awake() {
        if (instances == null)
            instances = new List<T>();
        instances.Add((T)this);
    }

    void OnDestroy() {
        instances.Remove((T) this);
    }
}
