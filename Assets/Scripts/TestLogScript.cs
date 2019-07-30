using System;
using System.Collections.Generic;
using UnityEngine;

public class TestLogScript : MonoBehaviour {
    public int triggerFrame = 10;
    public int currentFrame = 0;
    public bool destroyOnTrigger = true;

    public List<LogMessage> messages;
    void Update() {
        if (currentFrame == triggerFrame) Trigger();
        currentFrame++;
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
