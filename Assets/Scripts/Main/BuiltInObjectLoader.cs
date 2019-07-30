using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectDatabase))]
public class BuiltInObjectLoader : MonoBehaviour {
    public List<GameObject> builtInObjects;
    public ObjectDatabase database;

    void Start() {
        AddBuiltInObjects();
    }

    void AddBuiltInObjects() {
        foreach (GameObject builtInObject in builtInObjects) {
            GameObject tempInstance = Instantiate(builtInObject);
            ObjectDescriptor descriptor = tempInstance.GetComponent<ObjectDescriptor>();
            if (descriptor == null) {
                Destroy(tempInstance);
                Debug.Log("Built-in Object \"" + builtInObject.name + "\" in index " + builtInObjects.IndexOf(builtInObject).ToString() + " is not setup properly!");
                continue;
            }

            ObjectDefinition definition = new ObjectDefinition() {
                UUID = descriptor.baseUUID,
                author = descriptor.author,
                bundle = null,
                dateCreated = descriptor.dateCreated,
                dateUpdated = descriptor.dateUpdated,
                description = descriptor.description,
                filePath = "builtin",
                image = descriptor.image,
                name = descriptor.name,
                prefab = builtInObject,
                type = descriptor.objectType
            };

            database.objects.Add(definition);
            DestroyImmediate(tempInstance);
        }

        database.currentVersionID++;
    }
}
