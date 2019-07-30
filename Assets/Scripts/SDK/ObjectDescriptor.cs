using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Holds information about an object's prefab.
/// </summary>
public abstract class ObjectDescriptor : MonoBehaviour {
    [Header("Base Metadata")]
    new public string name;
    public string description;
    public Sprite image;
    public string version;
    public string author;
    public string source;
    public DateTime dateCreated;
    public DateTime dateUpdated;

    [Header("Base Internals")]
    public string baseUUID;         // UUID for reference in the database.
    public abstract ObjectType objectType { get; }

    public ObjectDefinition baseDefinition {
        get {
            if (_baseDefinition == null) _baseDefinition = ObjectDatabase.instance.GetObjectByUUID(baseUUID);
            return _baseDefinition;
        }
    }
    ObjectDefinition _baseDefinition;
}
