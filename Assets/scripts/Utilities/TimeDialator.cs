using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityStandardAssets.CrossPlatformInput;

public class TimeDialator : MonoBehaviour
{
    bool depleted = false;
    bool poweredOn = false;
    bool PoweredOn
    {
        get { return poweredOn; }
        set
        {
            if (poweredOn == value) return;
            poweredOn = value;
            if (poweredOn)
                AudioSource.PlayOneShot(timeWarpClips[0]);
            else
                AudioSource.PlayOneShot(timeWarpClips[1]);

        }
    }

    [SerializeField]
    float depletedDelay = 2f;
    float sinceDepleted = 0f;
    float maxEnergy = 100;
    float currentEnergy = 50;
    [SerializeField]
    float chargeRate = 20f;
    public static float powerUsage = 10f;
    [SerializeField]
    Hourglass2 Gauge_UI;
    public AudioMixer masterMixer;
    public GameObject WarpedVisual;
    public AudioSource AudioSource;
    public AudioClip[] timeWarpClips;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        Gauge_UI = FindObjectOfType<Hourglass2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= Mathf.Epsilon) return;

        if (depleted)
        { //charge a little before you can re-engage
            if ((sinceDepleted += Time.deltaTime) > depletedDelay) depleted = false;
            currentEnergy += Time.deltaTime * chargeRate;
            PoweredOn = false;
        }
        else
        {
            if (Input.GetKeyDown("f"))
            {
                PoweredOn = !poweredOn;
            }
        }

        if (currentEnergy <= 0)
        {
            depleted = true;
            PoweredOn = false;
            sinceDepleted = 0f;
        }


        if (!poweredOn)
        {
            //
            Time.timeScale = Mathf.Clamp(Time.timeScale + Time.unscaledDeltaTime/2f, 0.01f, 1f);
            currentEnergy += Time.deltaTime * chargeRate;
        }
        else
        {
            currentEnergy -= (Time.deltaTime / Time.timeScale) * powerUsage;
            Time.timeScale = Mathf.Clamp(Time.timeScale - Time.unscaledDeltaTime, 0.01f, 1f);

            if (Mathf.Abs(CrossPlatformInputManager.GetAxis("Mouse X")) > Mathf.Epsilon ||
            Mathf.Abs(CrossPlatformInputManager.GetAxis("Mouse Y")) > Mathf.Epsilon || Input.anyKey)
            {
                if (Time.timeScale < 0.1f)
                {
                    Time.timeScale = 0.1f;

                }
            }
        }

        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        Gauge_UI.SetFill(currentEnergy);

        // had to hack each audiosource independently since mixer doesnt work on webgl
        // masterMixer.SetFloat("SfxPitch", Time.timeScale);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        WarpedVisual.SetActive(Time.timeScale < 0.9f);

    }
}
