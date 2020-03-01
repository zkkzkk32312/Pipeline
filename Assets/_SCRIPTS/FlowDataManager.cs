using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowDataManager : MonoBehaviour
{
    public FlowController flowController;
    ExecuteOnMainThread executeOnMainTHread;

    private static FlowDataManager singleton = null;
    public static FlowDataManager Singleton
    {
        get
        {
            return singleton;
        }
    }

    private void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(this.gameObject);
        }

        singleton = this;
        DontDestroyOnLoad(this.gameObject);
        executeOnMainTHread = GetComponent<ExecuteOnMainThread>();
    }

    /***
     * TO DO
     ***/

    public void FlowDataReceived (string s)
    {
        s = s.ToLower();
        if (s.Contains("speed"))
        {
            if (s.Contains("up"))
            {
                executeOnMainTHread.RunOnMainThread.Enqueue(() =>
                {
                    flowController.SpeedUp();
                });
            }
            else if (s.Contains("down"))
            {
                executeOnMainTHread.RunOnMainThread.Enqueue(() =>
                {
                    flowController.SpeedDown();
                });
            }
        }
        else if (s.Contains("volume"))
        {
            if (s.Contains("up"))
            {
                executeOnMainTHread.RunOnMainThread.Enqueue(() =>
                {
                    flowController.VolumeUp();
                });
            }
            else if (s.Contains("down"))
            {
                executeOnMainTHread.RunOnMainThread.Enqueue(() =>
                {
                    flowController.VolumeDown();
                });
            }
        }
    }

    private static string GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }
}
