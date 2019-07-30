using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoggerSettingsMenu : MonoBehaviour {
    public LoggerScreen logScreen;

    public Toggle showIconToggle;
    public Toggle liveUpdateToggle;
    public Toggle showStackTraceToggle;
    public Toggle useColorToggle;
    public Toggle showTimestampToggle;

    public const string showStackTracePlayerPrefName = "pref_debug_show_stack_trace";
    public const string showIconPlayerPrefName = "pref_debug_show_icon";
    public const string debugLiveUpdatePlayerPrefName = "pref_debug_live_update";
    public const string useColorPlayerPrefName = "pref_debug_use_color";
    public const string showTimestampPlayerPrefName = "pref_debug_show_timestamp";
    public const int showStackTracePlayerPrefDefaultValue = 0;
    public const int showIconPlayerPlayerPrefDefaultValue = 1;
    public const int debugLiveUpdatePlayerPrefDefaultValue = 1;
    public const int useColorPlayerPrefDefaultValue = 0;
    public const int showTimestampPlayerPrefDefaultValue = 1;

    bool suppressEvents;
    public SideMenuAnimator menuAnimator;

    void Start() {
        Refresh();
    }

    void Refresh() {
        suppressEvents = true;
        showIconToggle.isOn = PlayerPrefs.GetInt(showIconPlayerPrefName, showIconPlayerPlayerPrefDefaultValue) == 1;
        liveUpdateToggle.isOn = PlayerPrefs.GetInt(debugLiveUpdatePlayerPrefName, debugLiveUpdatePlayerPrefDefaultValue) == 1;
        showStackTraceToggle.isOn = PlayerPrefs.GetInt(showStackTracePlayerPrefName, showStackTracePlayerPrefDefaultValue) == 1;
        useColorToggle.isOn = PlayerPrefs.GetInt(useColorPlayerPrefName, useColorPlayerPrefDefaultValue) == 1;
        showTimestampToggle.isOn = PlayerPrefs.GetInt(showTimestampPlayerPrefName, showTimestampPlayerPrefDefaultValue) == 1;
        suppressEvents = false;
        ShowIconToggle_OnValueChanged(showIconToggle.isOn);
        LiveUpdateToggle_OnValueChanged(liveUpdateToggle.isOn);
        ShowStackTraceToggle_OnValueChanged(showStackTraceToggle.isOn);
        UseColorToggle_OnValueChanged(useColorToggle.isOn);
        ShowTimestampToggle_OnValueChanged(showTimestampToggle.isOn);
    }

    public void ShowIconToggle_OnValueChanged(bool newValue) {
        if (suppressEvents) return;
        if (newValue) logScreen.EnableIcon();
        if (!newValue) logScreen.DisableIcon();
    }

    public void ShowStackTraceToggle_OnValueChanged(bool newValue) {
        if (suppressEvents) return;
        if (newValue) logScreen.EnableStackTrace();
        if (!newValue) logScreen.DisableStackTrace();
    }

    public void LiveUpdateToggle_OnValueChanged(bool newValue) {
        if (suppressEvents) return;
        if (newValue) logScreen.EnableLiveLog();
        if (!newValue) logScreen.DisableLiveLog();
    }

    public void UseColorToggle_OnValueChanged(bool newValue) {
        if (suppressEvents) return;
        if (newValue) logScreen.EnableColor();
        if (!newValue) logScreen.DisableColor();
    }

    public void ShowTimestampToggle_OnValueChanged(bool newValue) {
        if (suppressEvents) return;
        if (newValue) logScreen.EnableTimestamp();
        if (!newValue) logScreen.DisableTimestamp();
    }

    public void Show() {
        menuAnimator.Show();
    }

    public void Hide() {
        menuAnimator.Hide();
    }
}
