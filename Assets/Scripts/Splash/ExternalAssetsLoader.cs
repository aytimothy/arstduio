using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(FlowEventNode))]
public class ExternalAssetsLoader : MonoBehaviour {
    public FlowEventNode eventCallback;
    public InitializerSceneFlow initializer;
    public ObjectDatabase database;
    public AsyncOperation asyncOperation => initializer.mainSceneLoadOperation;

    public string[] searchPaths = new string[0];
    public bool verbose;

    #region Event Check

    void Awake() {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (!Directory.Exists(Application.persistentDataPath + "/data"))
            Directory.CreateDirectory(Application.persistentDataPath + "/data");
        if (!Directory.Exists(Application.dataPath + "/data"))
            Directory.CreateDirectory(Application.dataPath + "/data");

        searchPaths = new string[] { Application.persistentDataPath + "/data", Application.dataPath + "/data" };
#elif UNITY_IOS
        if (!Directory.Exists(Application.persistentDataPath + "/data"))
            Directory.CreateDirectory(Application.persistentDataPath + "/data");

        searchPaths = new string[] { Application.persistentDataPath + "/data" };
#elif UNITY_ANDROID
        if (!Directory.Exists(Application.persistentDataPath + "/data"))
            Directory.CreateDirectory(Application.persistentDataPath + "/data");

        searchPaths = new string[] { Application.persistentDataPath + "/data" };
#endif
    }

    void Start() {
        if (asyncOperation.isDone) OnSceneLoaded();
        if (!asyncOperation.isDone) asyncOperation.completed += AsyncOperationOnCompleted;
    }

    private void AsyncOperationOnCompleted(AsyncOperation operation) {
        OnSceneLoaded();
    }

    void Update() {
        if (armed && assetBundlesLoaded == assetBundlesFound) {
            OnAssetBundleLoadingComplete();
        }
    }

    #endregion

    #region Do Stuff

    public void OnSceneLoaded() {
        if (database == null) database = ObjectDatabase.instance;
        StartCoroutine("LoadAll");
    }

    public void LoadAll() {
        string[] assetBundlePaths = FindAllAssetBundles(searchPaths);
        if (verbose)
            Debug.Log("Found " + assetBundlePaths.Length + " AssetBundles.");
        LoadBundles(assetBundlePaths);
    }


    public bool armed = false;
    public int assetBundlesFound = 0;
    public int assetBundlesLoaded = 0;

    // Note: Don't forget to make sure your compiled AssetBundle(s) end with .assetbundle!!!
    public string[] FindAllAssetBundles(string[] searchPaths) {
        List<string> results = new List<string>();
        foreach (string searchPath in searchPaths) {
            string[] filePaths = Directory.GetFiles(searchPath);
            foreach (string filePath in filePaths) {
                string[] splitString = filePath.Split('.');
                if (splitString[splitString.Length - 1].ToLower() == "assetbundle") {
                    if (verbose)
                        Debug.Log("Found AssetBundle at: " + filePath);
                    results.Add(filePath);
                }
            }
        }
        return results.ToArray();
    }

    public void LoadBundles(string[] bundlePaths) {
        foreach (string bundlePath in bundlePaths) {
            AssetBundleCreateRequest loadOperation = AssetBundle.LoadFromFileAsync(bundlePath);
            assetBundlesFound++;
            loadOperation.completed += OnAssetBundleLoaded;
        }

        armed = true;
    }

    private void OnAssetBundleLoaded(AsyncOperation operation) {
        AssetBundleCreateRequest _operation = (AssetBundleCreateRequest) operation;
        GameObject descriptorObject = _operation.assetBundle.LoadAsset<GameObject>("AssetBundleDescriptor");
        GameObject descriptorInstance = Instantiate(descriptorObject);
        AssetBundleDescriptor bundleDescriptor = descriptorInstance.GetComponent<AssetBundleDescriptor>();
        foreach (GameObject arObject in bundleDescriptor.arObjects) {
            GameObject arObjectInstance = Instantiate(arObject);
            ObjectDescriptor descriptor = arObjectInstance.GetComponent<ObjectDescriptor>();
            if (verbose)
                Debug.Log("Found Object \"" + arObjectInstance.name + "\" from \"" + bundleDescriptor.name + "\"!");
            if (descriptor == null) {
                DestroyImmediate(arObjectInstance);
                Debug.Log("Loaded Object \"" + arObjectInstance.name + "\" from \"" + bundleDescriptor.name + "\" is not setup properly!");
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
                prefab = arObject,
                type = descriptor.objectType
            };

            database.objects.Add(definition);

            DestroyImmediate(arObjectInstance);
        }
        DestroyImmediate(descriptorInstance);
        assetBundlesLoaded++;
    }

    public void OnAssetBundleLoadingComplete() {
        eventCallback.Activate();
    }

    #endregion
}
