using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DebugARSpawner : MonoBehaviour {
    public GameObject debugPlanePrefab;
    public List<Transform> spawnPoints;

    public ARSessionOrigin origin => ARSessionManager.instance.sessionObject;

    bool triggered = false;

    void Update() {
        CheckTrigger();
    }

    public void CheckTrigger() {
        if (origin == null) {
            triggered = false;
            return;
        }
        if (triggered) return;

        triggered = true;
        OnTrigger();
    }

    public void OnTrigger() {
        foreach (Transform spawnPoint in spawnPoints)
            Instantiate(debugPlanePrefab, spawnPoint.position, spawnPoint.rotation, origin.trackablesParent);
    }
}
