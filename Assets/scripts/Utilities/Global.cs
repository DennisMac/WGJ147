using UnityEngine;
using System.Collections;

public class Global
{
    static bool isPaused = false;
    static float playerspeed = 0.1f; 
    public static bool IsPaused { get { return isPaused; } }
    public static float PlayerSpeed { get { return playerspeed; }}
    public static bool PlayerCloaked = false;
    public static bool PlayerFiring = false;
    public static Transform PlayerTransform;

    public static AudioSource musicAudioSource;
    

}
