using System;
using System.Collections;
using System.Collections.Generic;
using MugHeadStudios;
using UnityEngine;
using UnityEngine.UI;

public class MHSFader : MonoBehaviour
{
    public MHSFader instance {get; private set;}

    public Color fadeColor = Color.black;

	public bool fadeInOnStart = true;

	public float defaultFadeTime = 1;

	RawImage rawImage;

    void Awake()
    {
        if(instance == null)
            instance = this;

        rawImage = gameObject.AddComponent<RawImage>();
        rawImage.color = fadeColor;
    }

	void Start()
	{
        if(fadeInOnStart)
            FadeIn();
	}

	public void SetFade(float alpha)
	{
        rawImage.color = Global.ModifyNonVariable(rawImage.color, c => { c.a = alpha; return c; });
	}

	public void InstantOut()
	{
		SetFade(1);
	}

    public void InstantIn()
    {
        SetFade(0);
    }

    public void FadeIn()
    {
        FadeIn(defaultFadeTime);
    }

    public void FadeOut()
    {
        FadeOut(defaultFadeTime);
    }

    public void FadeIn(float? time = null, Action callback = null)
    {
        InstantOut();

		float finalTime = (time != null) ? (float)time : defaultFadeTime;

        Global.RunFor(finalTime, p => {
            SetFade(1 - p);
        }, () => {
            if (callback != null) callback();
        });
    }

    public void FadeOut(float? time = null, Action callback = null)
    {
        InstantIn();

        float finalTime = (time != null) ? (float)time : defaultFadeTime;

        Global.RunFor(finalTime, p => {
            SetFade(p);
        }, () => {
            if (callback != null) callback();
        });
    }
}
