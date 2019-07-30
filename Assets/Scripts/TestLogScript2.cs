using System;
using System.Collections.Generic;
using UnityEngine;

public class TestLogScript2 : MonoBehaviour {
    public float triggerTime = 10f;
    public float currentTime = 0f;
    public bool destroyOnTrigger = true;

    public List<LogMessage> messages;
    void Update() {
        if (currentTime >= triggerTime) Trigger();
        currentTime += Time.deltaTime;
    }

    void Trigger() {
        foreach (LogMessage message in messages) {
            switch (message.type) {
                case LogType.Error:
                    Debug.LogError(message.message);
                    break;
                case LogType.Assert:
                    Debug.LogError("[ASSERT] " + message.message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message.message);
                    break;
                case LogType.Log:
                    Debug.Log(message.message);
                    break;
                case LogType.Exception:
                    Debug.LogError("[EXCEPTION] " + message.message);
                    break;
            }
        }
        if (destroyOnTrigger) Destroy(this);
    }
}
