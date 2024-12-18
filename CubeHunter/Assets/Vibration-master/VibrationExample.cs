﻿////////////////////////////////////////////////////////////////////////////////
//  
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
//
////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VibrationExample : MonoBehaviour
{
    public Text inputTime;
    public Text inputPattern;
    public Text inputRepeat;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TapVibrate()
    {
        Vibration.Vibrate();
    }

    public void TapVibrateCustom()
    {
        Debug.Log(inputTime);
        //Vibration.Vibrate (int.Parse ( inputTime.ToString()));
    }

    public void TapVibratePattern()
    {
        long[] longs = inputPattern.text.Select(item => (long)item).ToArray();
        Debug.Log(longs + " " + int.Parse(inputRepeat.text));
        //  Vibration.Vibrate ( longs, int.Parse ( inputRepeat.text ) );
    }

    public void TapCancelVibrate()
    {

        // Vibration.Cancel ();
    }

    public void TapPopVibrate()
    {
        //acilmasi lazim

        // #if UNITY_IOS
        // Vibration.VibratePop ();
        // #endif
    }

    public void TapPeekVibrate()
    {
        //Vibration.VibratePeek ();
    }

    public void TapNopeVibrate()
    {
        //ibration.VibrateNope ();
    }
}
