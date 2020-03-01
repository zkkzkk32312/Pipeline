using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class PopupController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI text;
    public float popDuration = 2.5f;
    Coroutine routine;
    Tween fadeTween;

    private void Start()
    {
        canvasGroup.alpha = 0f;

        FlowController.FlowSpeedChanged += FlowSpeedChangedHandler;
        FlowController.FlowVolumeChanged += FlowVolumeChangedHandler;
    }

    private void OnDestroy()
    {
        FlowController.FlowSpeedChanged -= FlowSpeedChangedHandler;
        FlowController.FlowVolumeChanged -= FlowVolumeChangedHandler;
    }

    private void FlowSpeedChangedHandler(float speed)
    {
        SetText("Speed : " + speed.ToString("F2"));
        Pop();
    }

    private void FlowVolumeChangedHandler(float volume)
    {
        SetText("Volume : " + volume.ToString("F2"));
        Pop();
    }

    public void SetText (string t)
    {
        text.text = t;
    }

    public void Pop ()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
            fadeTween.Kill();
        }

        canvasGroup.alpha = 1f;
        routine = StartCoroutine(CountDownHide());
    }

    IEnumerator CountDownHide ()
    {
        yield return new WaitForSeconds(popDuration);
        fadeTween = canvasGroup.DOFade(0f, popDuration);
    }
}
