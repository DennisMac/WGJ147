using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityStandardAssets.CrossPlatformInput;

public class TimeDialator : MonoBehaviour
{
    bool depleted = false;
    bool poweredOn = false;
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


    // Start is called before the first frame update
    void Start()
    {
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
            poweredOn = false;
        }
        else
        {
            if (Input.GetKeyDown("f"))
            {
                poweredOn = !poweredOn;
            }
        }

            if (currentEnergy <= 0)
            {
                depleted = true;
                poweredOn = false;
                sinceDepleted = 0f;
            }

        
        if (!poweredOn)
        {
            //
            Time.timeScale = Mathf.Clamp(Time.timeScale + Time.unscaledDeltaTime, 0.01f, 1f);
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

        masterMixer.SetFloat("SfxPitch", Time.timeScale);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
