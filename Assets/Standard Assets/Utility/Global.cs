using UnityEngine;

namespace Assets.Standard_Assets.Utility
{
    public class Global
    {
        static bool isPaused = false;
        static float playerspeed = 0.1f;
        public static float PlayerSpeed { get { return playerspeed; } }
        public static bool PlayerCloaked = false;
        public static bool PlayerFiring = false;
        public static Transform PlayerTransform;

        public static AudioSource musicAudioSource;
        public static float MouseSensitivity = 0.5f;


        public static bool IsPaused
        {
            get { return isPaused; }

            set
            {
                isPaused = value;
                if (isPaused) //pause stuff
                {
                    musicAudioSource.Pause();
                }
                else //continue stuff
                {
                    musicAudioSource.Play();
                }
            }

        }

    }
}
