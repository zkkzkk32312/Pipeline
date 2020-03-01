using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;


public class FlowController : MonoBehaviour
{
    public ParticleSystem particle;
    [Space(8)]
    public float minSpeed = 0.1f;
    public float maxSpeed = 10f;
    public float speedChangeIntervals = 1f;
    [Space(8)]
    public float minRate = 10f;
    public float maxRate = 100f;
    public float rateChangeIntervals = 5f;
    [Space(8)]
    public float transitionDuration;

    [HideInInspector]
    public static Action<float> FlowSpeedChanged;
    [HideInInspector]
    public static Action<float> FlowVolumeChanged;

    public void SetSpeed (float speed)
    {
        if (speed > maxSpeed)
            speed = maxSpeed;

        if (speed < minSpeed)
            speed = minSpeed;

        var main = particle.main;
        main.startSpeed = speed;

        if (FlowSpeedChanged != null)
            FlowSpeedChanged(speed);
    }

    [Button]
    public void SpeedUp ()
    {
        var main = particle.main;
        float speed = main.startSpeed.constant;
        SetSpeed(speed + speedChangeIntervals);
    }

    [Button]
    public void SpeedDown ()
    {
        var main = particle.main;
        float speed = main.startSpeed.constant;
        SetSpeed(speed - speedChangeIntervals);
    }

    public void SetVolume (float volume)
    {
        if (volume > maxRate)
            volume = maxRate;

        if (volume < minRate)
            volume = minRate;

        var emission = particle.emission;
        emission.rateOverTime = volume;

        if (FlowVolumeChanged != null)
            FlowVolumeChanged(volume);
    }

    [Button]
    public void VolumeUp ()
    {
        var emission = particle.emission;
        SetVolume(emission.rateOverTime.constant + rateChangeIntervals);
    }

    [Button]
    public void VolumeDown ()
    {
        var emission = particle.emission;
        SetVolume(emission.rateOverTime.constant - rateChangeIntervals);
    }
}
