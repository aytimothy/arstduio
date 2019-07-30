using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SideMenuAnimator))]
public class LoggerScreen : MonoBehaviour {
    [Header("References")]
    public GameObject prefab;
    public Transform container;
    public Logger logger;
    public SideMenuAnimator animator;
    public SettingsScreen lastScreen;
    public LoggerSettingsMenu settingsScreen;
    public GameObject noMessagesObject;

    public bool showStackTrace {
        get { return _showStackTrace; }
        private set { _showStackTrace = value; }
    }
    [Header("Active Settings")]
    [SerializeField]
    [EditorReadOnly]
    bool _showStackTrace;
    public bool allowColor {
        get { return _allowColor; }
        private set { _allowColor = value; }
    }
    [SerializeField]
    [EditorReadOnly]
    bool _allowColor;
    public bool showIcon {
        get { return _showIcon; }
        private set { _showIcon = value; }
    }
    [SerializeField]
    [EditorReadOnly]
    bool _showIcon;
    public bool liveUpdate {
        get { return _liveUpdate; }
        private set { _liveUpdate = value; }
    }
    [SerializeField]
    [EditorReadOnly]
    bool _liveUpdate;
    public bool showTimestamp {
        get { return _showTimestamp; }
        private set { _showTimestamp = value; }
    }
    [SerializeField]
    [EditorReadOnly]
    bool _showTimestamp;

    [Header("Messages")]
    public List<LoggerMessageObject> messages;

    [Header("Visual Settings")]
    public Sprite normalSprite;
    public Sprite infoSprite;
    public Sprite warningSprite;
    public Sprite errorSprite;

    public void Show() {
        animator.Show();
        lastScreen.Hide();
        
        // Check for an update.
        if (!liveUpdate && seenLogVersion != logger.version) Refresh();
        
    }

    public void Hide() {
        animator.Hide();
        lastScreen.Show();
    }

    public void EnableLiveLog() {
        Application.logMessageReceived += Application_OnLogMessageReceived;
        liveUpdate = true;
    }

    public void DisableLiveLog() {
        Application.logMessageReceived -= Application_OnLogMessageReceived;
        liveUpdate = false;
    }

    public void EnableStackTrace() {
        showStackTrace = true;
        foreach (LoggerMessageObject messageObject in messages) messageObject.Refresh();
    }

    public void DisableStackTrace() {
        showStackTrace = false;
        foreach (LoggerMessageObject messageObject in messages) messageObject.Refresh();
    }

    public void EnableColor() {
        allowColor = true;
        foreach (LoggerMessageObject messageObject in messages) messageObject.Refresh();
    }

    public void DisableColor() {
        allowColor = false;
        foreach (LoggerMessageObject messageObject in messages) messageObject.Refresh();
    }

    public void EnableIcon() {
        showIcon = true;
        foreach (LoggerMessageObject messageObject in messages) messageObject.Refresh();
    }

    public void DisableIcon() {
        showIcon = false;
        foreach (LoggerMessageObject messageObject in messages) messageObject.Refresh();
    }

    public void EnableTimestamp() {
        showTimestamp = true;
        foreach (LoggerMessageObject messageObject in messages) messageObject.Refresh();
    }

    public void DisableTimestamp() {
        showTimestamp = false;
        foreach (LoggerMessageObject messageObject in messages) messageObject.Refresh();
    }

    private void Application_OnLogMessageReceived(string condition, string stacktrace, LogType type) {
        CreateLogObject(DateTime.Now, condition, stacktrace, type);
    }

    public void CreateLogObject(LogMessage logMessage) {
        CreateLogObject(logMessage.timestamp, logMessage.message, logMessage.stackTrace, logMessage.type);
    }

    public void CreateLogObject(DateTime timestamp, string message, string stackTrace, LogType type) {
        GameObject newlogObject = Instantiate(prefab, container);
        LoggerMessageObject objectComponent = newlogObject.GetComponent<LoggerMessageObject>();
        if (objectComponent == null) {
            Debug.LogError("Incompatible log message prefab discovered!");
            return;
        }
        objectComponent.controller = this;
        objectComponent.timestamp = timestamp;
        objectComponent.message = message;
        objectComponent.stackTrace = stackTrace;
        objectComponent.type = type;
        messages.Add(objectComponent);
        noMessagesObject.SetActive(false);
        objectComponent.Refresh();
    }

    [EditorReadOnly]
    [SerializeField]
    uint seenLogVersion;

    public void Refresh() {
        seenLogVersion = logger.version;
        ClearAll();
        noMessagesObject.SetActive(logger.messages.Count <= 0);
        foreach (LogMessage logMessage in logger.messages)
            CreateLogObject(logMessage);
    }

    public void ClearAll() {
        foreach (LoggerMessageObject messageObject in messages)
            Destroy(messageObject.gameObject);
        messages.Clear();
        noMessagesObject.SetActive(true);
    }

    public void SettingsButton_OnClick() {
        settingsScreen.Show();
    }

    public void RefreshButton_OnClick() {
        Refresh();
    }

    public void ClearButton_OnClick() {
        logger.Clear();
        Refresh();
    }
}
