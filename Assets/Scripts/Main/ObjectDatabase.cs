using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is the database that contains all the "static" data regarding the items you can spawn into the AR world.
/// </summary>
public class ObjectDatabase : Singleton<ObjectDatabase> {
    public List<ObjectDefinition> objects;
    [SerializeField]
    List<string> fileList = new List<string>();
    [SerializeField]
    List<string> loadedFileList = new List<string>();
    public static string dataPath { get; private set; }
    public int currentVersionID = 0;
#if UNITY_EDITOR
    [SerializeField]
    string _dataPath;
#endif
    public UnityEvent OnLoadComplete;

    public override void Awake() {
        base.Awake();
#if UNITY_EDITOR || UNITY_STANDALONE
        dataPath = Application.persistentDataPath + "/objects/";
#elif UNITY_ANDROID
        dataPath = "/sdcard/vrstudio/objects/";
#elif UNITY_IOS
        dataPath = Application.persistentDataPath + "/objects/";
#endif
#if UNITY_EDITOR
        _dataPath = dataPath;
        LoadObjects();
#endif
    }

    public ObjectDefinition GetObjectByName(string name) {
        foreach (ObjectDefinition objectDefinition in objects)
            if (objectDefinition.name.ToLower() == name.ToLower())
                return objectDefinition;
        Debug.LogError("Could not find an object with the name: \"" + name + "\".");
        return null;
    }

    public ObjectDefinition GetObjectByUUID(string UUID) {
        foreach (ObjectDefinition objectDefinition in objects)
            if (objectDefinition.UUID == UUID)
                return objectDefinition;
        Debug.LogError("Could not find an object with the UUID: \"" + UUID + "\".");
        return null;
    }

    public void LoadObjects() {
        if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);
        string[] filePaths = Directory.GetFiles(dataPath);
        fileList = new List<string>(filePaths);
        foreach (string filePath in filePaths)
            LoadObject(filePath);
        OnLoadComplete.Invoke();
    }

    public void LoadMissingObjects() {
        foreach (string filePath in fileList) {
            if (!loadedFileList.Contains(filePath))
                LoadObject(filePath);
        }
    }

    public void LoadObject(string path) {
        AssetBundleCreateRequest asyncLoadOperation = AssetBundle.LoadFromFileAsync(path);
        loadedFileList.Add(path);
        asyncLoadOperation.completed += OnObjectLoad;
    }

    private void OnObjectLoad(AsyncOperation obj) {
        AssetBundleCreateRequest request = (AssetBundleCreateRequest) obj;
        currentVersionID++;
        Debug.LogError("// todo: Not Implemented. Add it to the database.");
    }

    public void UnloadObject(string path) {
        throw new NotImplementedException();
    }

    public bool UpdateAvaliable(int currentID = 0) {
        return !(currentID == currentVersionID);
    }

    public bool FileUpdateAvaliable() {
        string[] filePaths = Directory.GetFiles(dataPath);
        if (filePaths.Length != fileList.Count) return true;
        for (int i = 0; i < filePaths.Length; i++)
            if (filePaths[i] != fileList[i])
                return true;
        return false;
    }
}