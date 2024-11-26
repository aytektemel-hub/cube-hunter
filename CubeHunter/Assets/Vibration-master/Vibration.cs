using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Vibration
{
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern bool _HasVibrator();

    [DllImport("__Internal")]
    private static extern void _Vibrate();

    [DllImport("__Internal")]
    private static extern void _VibratePop();

    [DllImport("__Internal")]
    private static extern void _VibratePeek();

    [DllImport("__Internal")]
    private static extern void _VibrateNope();

    public static void VibratePop()
    {
        _VibratePop();
    }

    public static void VibratePeek()
    {
        _VibratePeek();
    }

    public static void VibrateNope()
    {
        _VibrateNope();
    }
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
	public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	public static AndroidJavaObject vibrator =currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
	public static AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

	public static void Vibrate(long milliseconds)
	{
		vibrator.Call("vibrate", milliseconds);
	}

	public static void Vibrate(long[] pattern, int repeat)
	{
		vibrator.Call("vibrate", pattern, repeat);
	}

	public static void Cancel()
	{
		vibrator.Call("cancel");
	}
#endif

    public static bool HasVibrator()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context");
		string Context_VIBRATOR_SERVICE = contextClass.GetStatic<string>("VIBRATOR_SERVICE");
		AndroidJavaObject systemService = context.Call<AndroidJavaObject>("getSystemService", Context_VIBRATOR_SERVICE);
		if (systemService.Call<bool>("hasVibrator"))
		{
			return true;
		}
		else
		{
			return false;
		}
#elif UNITY_IOS
        return true;//_HasVibrator ();
#else
        return false;
#endif
    }

    public static void Vibrate()
    {
#if UNITY_EDITOR
        Debug.Log("Bzzzt! Cool vibration!");
#endif
        Handheld.Vibrate();
    }

    public static void VibrateBoth()
    {
        if (PlayerPrefs.GetInt("vibration", 1) == 1)
        {
#if UNITY_IOS && !UNITY_EDITOR
                Vibration.VibratePop ();
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
                Vibration.Vibrate(25);
#endif
        }
    }
}
