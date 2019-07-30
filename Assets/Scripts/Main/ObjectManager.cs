using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager> {
    public List<ARObject> objects = new List<ARObject>();
    public static bool loaded { get; private set; }

    #region Management
    public static void Register(ARObject self) {
        if (!loaded) {
            Debug.LogError("Cannot Register() an object without initializing an ObjectManager first! There's no tracker!");
            return;
        }

        if (!instance.objects.Contains(self))
            instance.objects.Add(self);
    }

    public static void Unregister(ARObject self) {
        if (!loaded) {
            Debug.LogError("Cannot Unregister() an object without initializing an ObjectManager first! There's nowhere to untrack from.");
            return;
        }

        instance.objects.Remove(self);
    }

    public static ARObject GetObjectByUUID(string UUID) {
        if (instance == null) {
            Debug.LogError("Please instantiate an ObjectManager first!");
            return null;
        }
        foreach (ARObject arObject in instance.objects)
            if (arObject.UUID == UUID)
                return arObject;
        Debug.LogError("Could not find an object with the UUID \"" + UUID + "\".");
        return null;
    }

    public static ARObject[] GetObjectsByBaseUUID(string baseUUID) {
        if (instance == null) {
            Debug.LogError("Please instantiate an ObjectManager first!");
            return null;
        }
        List<ARObject> results = new List<ARObject>();
        foreach (ARObject arObject in instance.objects)
            if (arObject.baseUUID == baseUUID)
                results.Add(arObject);
        return results.ToArray();
    }

    public static ARObject[] GetARObjectByType(ObjectType objectType) {
        if (instance == null) {
            Debug.LogError("Please instantiate an ObjectManager first!");
            return null;
        }
        List<ARObject> results = new List<ARObject>();
        foreach (ARObject arObject in instance.objects) {
            switch (objectType) {
                case ObjectType.None:
                    return new ARObject[0];
                case ObjectType.Static:
                    if (arObject.GetType() == typeof(ARStatic))
                        results.Add(arObject);
                    break;
                case ObjectType.Dynamic:
                    if (arObject.GetType() == typeof(ARDynamic))
                        results.Add(arObject);
                    break;
                case ObjectType.Character:
                    if (arObject.GetType() == typeof(ARCharacter))
                        results.Add(arObject);
                    break;
            }
        }
        return results.ToArray();
    }
    #endregion

    public override void Awake() {
        base.Awake();
        loaded = true;
    }
}
