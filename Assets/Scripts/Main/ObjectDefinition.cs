using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component holds information about an object's original. (It's an entry in ObjectDatabase)
/// </summary>
[Serializable]
public class ObjectDefinition {
    /// <summary>
    /// The UUID for the object. This is either a hash or a named pair. Up to you, really.
    /// </summary>
    public string UUID;
    /// <summary>
    /// This is the name for the object. It's the one to be displayed (repeats are allowed).
    /// </summary>
    public string name;
    /// <summary>
    /// This is the image that is the thumbnail of the object.
    /// </summary>
    public Sprite image;
    public string author;
    public string source;
    public string version;
    public DateTime dateUpdated;
    public DateTime dateCreated;
    public string description;
    /// <summary>
    /// This is the type of object it is.
    /// </summary>
    public ObjectType type;
    /// <summary>
    /// This is a reference to the object's prefab.
    /// </summary>
    public GameObject prefab;
    /// <summary>
    /// This is a reference to the AssetBundle it was loaded in.
    /// This is null if it is a built-in asset.
    /// </summary>
    public AssetBundle bundle;
    /// <summary>
    /// This is the string to the AssetBundle's location.
    /// This is empty if it is a built-in asset.
    /// </summary>
    public string filePath;
}

public enum ObjectType {
    None,
    Static,     // Can be scaled/rotated/moved, affected by physics, but that's it.
    Dynamic,    // Same as Static, but has animations.
    Character   // Same as Dynamic, but allows for character control mode (planned feature).
}
