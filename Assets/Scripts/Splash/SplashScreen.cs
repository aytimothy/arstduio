using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is a script that powers that splash screen.
/// </summary>
public class SplashScreen : MonoBehaviour {
    public bool allowAutoTransition;
    public bool allowUserTransition;
    public bool loop;
    public Vector2 transitionTime;
    public bool invokeOnceOnly;
    private bool eventInvoked;

    public List<SplashScreenSlide> slides;
    private int currentSlideIndex;
    private float nextAutoTransition;
    private float nextUserTransition;
    public UnityEvent onSlideshowFinished;

    void Start() {
        currentSlideIndex = -1;
        Transition();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
            if (Time.time > nextUserTransition && allowUserTransition)
                Transition();
        if (Time.time > nextAutoTransition && allowAutoTransition)
            Transition();
    }

    public void Transition() {
        nextUserTransition = Time.time + transitionTime.x;
        nextAutoTransition = Time.time + transitionTime.y;

        if (currentSlideIndex >= slides.Count - 1) {
            if (!invokeOnceOnly || !eventInvoked) {
                onSlideshowFinished.Invoke();
                eventInvoked = true;
            }

            HideAll();
            if (loop)
                ShowSlide(0, true);
            return;
        }

        ShowSlide(currentSlideIndex + 1);
    }

    public void HideAll() {
        foreach (SplashScreenSlide slide in slides)
            slide.gameObject.SetActive(false);
    }

    public void ShowSlide(int index, bool force = false) {
        if (index >= slides.Count)
            throw new IndexOutOfRangeException();

        if (force)
            HideAll();
        if (currentSlideIndex >= 0)
            slides[currentSlideIndex].gameObject.SetActive(false);
        currentSlideIndex = index;
        slides[currentSlideIndex].gameObject.SetActive(true);
    }
}
