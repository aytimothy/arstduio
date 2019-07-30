using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// FlowEvent for the initializer scene. See FlowEvent. This just contains the additional callback implementations.
/// </summary>
public class InitializerSceneFlow : FlowEvent {
    public string initSceneName = "Main";
    public AsyncOperation mainSceneLoadOperation;

    void Awake () {
        mainSceneLoadOperation = SceneManager.LoadSceneAsync(initSceneName, LoadSceneMode.Additive);
    }

    public void OnFinish () {
        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++) {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == initSceneName) SceneManager.SetActiveScene(scene);
        }
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
