using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// FlowEvent for the initializer scene. See FlowEvent. This just contains the additional callback implementations.
/// </summary>
public class InitializerSceneFlow : FlowEvent {
    public string initSceneName = "Main";

    void Awake () {
        
    }

    public void OnFinish () {
        SceneManager.LoadSceneAsync(initSceneName);
    }
}
