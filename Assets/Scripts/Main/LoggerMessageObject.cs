using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoggerMessageObject : MonoBehaviour {
    [Header("References")]
    public LoggerScreen controller;
    public DateTime timestamp;
    public string message;
    public string stackTrace;
    public LogType type;
    public TMP_Text text;
    public Image icon;
    public Sprite infoSprite => controller.infoSprite;
    public Sprite warningSprite => controller.warningSprite;
    public Sprite errorSprite => controller.errorSprite;

    [Header("Layout Numbers")]
    public float textOffset = 20f;

    public bool showStackTrace {
        get {
            if (controller == null) return PlayerPrefs.GetInt(LoggerSettingsMenu.showStackTracePlayerPrefName, LoggerSettingsMenu.showStackTracePlayerPrefDefaultValue) == 1;
            return controller.showStackTrace;
        }
    }
    public bool showIcon {
        get {
            if (controller == null) return PlayerPrefs.GetInt(LoggerSettingsMenu.showIconPlayerPrefName, LoggerSettingsMenu.showIconPlayerPlayerPrefDefaultValue) == 1;
            return controller.showIcon;
        }
    }
    public bool useColor {
        get {
            if (controller == null) return PlayerPrefs.GetInt(LoggerSettingsMenu.useColorPlayerPrefName, LoggerSettingsMenu.useColorPlayerPrefDefaultValue) == 1;
            return controller.allowColor;
        }
    }

    public bool showTimestamp {
        get {
            if (controller == null) return PlayerPrefs.GetInt(LoggerSettingsMenu.showTimestampPlayerPrefName, LoggerSettingsMenu.showTimestampPlayerPrefDefaultValue) == 1;
            return controller.showTimestamp;
        }
    }

    public void Refresh() {
        UpdateText();
        UpdateLayout();
    }

    public void UpdateText() {
        icon.gameObject.SetActive(showIcon);
        string timestampText = (showTimestamp) ? timestamp.ToString("O") + "\n" : "";
        string messageText = message + "\n";
        string stackTraceText = (showStackTrace) ? stackTrace + "\n" : "";
        string finalText = timestampText + messageText + stackTraceText;
        text.text = finalText;
        switch (type) {
            case LogType.Error:
                icon.sprite = errorSprite;
                break;
            case LogType.Assert:
                icon.sprite = errorSprite;
                break;
            case LogType.Warning:
                icon.sprite = warningSprite;
                break;
            case LogType.Log:
                icon.sprite = infoSprite;
                break;
            case LogType.Exception:
                icon.sprite = errorSprite;
                break;
        }
        text.richText = useColor;
    }

    public void UpdateLayout() {
        RectTransform textTransform = text.rectTransform;
        RectTransform imageTransform = icon.rectTransform;
        float position = (text.preferredHeight / 2) + textOffset;
        textTransform.anchoredPosition = new Vector2(textTransform.anchoredPosition.x, -position);
        float boxSize = Mathf.Max(text.preferredHeight, imageTransform.sizeDelta.y) + (2 * textOffset);

        RectTransform thisTransform = GetComponent<RectTransform>();
        thisTransform.sizeDelta = new Vector2(thisTransform.sizeDelta.x, boxSize);
    }
}
