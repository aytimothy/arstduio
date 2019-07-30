using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Instantly marks this Game Object for non-destruction; persistence between scenes.
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour {
    /// <summary>
    /// Interface property for _destroyOnNextLoad
    /// </summary>
    public bool destroyOnNextLoad {
        get { return _destroyOnNextLoad; }
        set {
            activeScene = SceneManager.GetActiveScene().name;
            _destroyOnNextLoad = value;
        }
    }
    /// <summary>
    /// Whether to unload the object the next time a new scene is loaded.
    /// </summary>
#if UNITY_EDITOR
	public
#endif
    bool _destroyOnNextLoad;
    /// <summary>
    /// The scene that was active when unload was triggered.
    /// </summary>
#if UNITY_EDITOR
	public
#endif
    string activeScene;

    /// <summary>
    /// MonoBehaviour.Awake () - Called when the script is loaded.
    /// </summary>
	void Awake () {
        // Marks the object as persistent.
		if (transform.parent == null)
			DontDestroyOnLoad(gameObject);
	}

    /// <summary>
    /// MonoBehaviour.Update () - Called every frame, before drawing.
    /// </summary>
    /// <remarks>
    /// If the object is set for destruction and the active scene has changed... DESTROY IT!
    /// </remarks>
    void Update () {
        if (_destroyOnNextLoad)
            if (SceneManager.GetActiveScene().name != name)
                Destroy(gameObject);
    }
}