using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField]
    float powerUsage = 10f;
    [SerializeField]
    Hourglass2 Gauge_UI;


    // Start is called before the first frame update
    void Start()
    {
        Gauge_UI = FindObjectOfType<Hourglass2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (depleted)
        { //charge a little before you can re-engage
            if ((sinceDepleted += Time.deltaTime) > depletedDelay) depleted = false;
            Time.timeScale = 1f;
            currentEnergy += Time.deltaTime * chargeRate;
        }
        else
        {
            if (Input.GetKeyDown("f"))
            {
                poweredOn = !poweredOn;
            }
            
            if (currentEnergy <= 0)
            {
                depleted = true;
                poweredOn = false;
                sinceDepleted = 0f;
            }


            if (!poweredOn)
            {
                currentEnergy += Time.deltaTime * chargeRate;
                Time.timeScale = 1f;
            }
            else
            {
                currentEnergy -= (Time.deltaTime/Time.timeScale) * powerUsage;
                if (Mathf.Abs(CrossPlatformInputManager.GetAxis("Mouse X")) > Mathf.Epsilon ||
                Mathf.Abs(CrossPlatformInputManager.GetAxis("Mouse Y")) > Mathf.Epsilon || Input.anyKey)
                {
                    Time.timeScale = 0.5f;
                }
                else
                {
                    Time.timeScale = 0.01f;
                }               
            }
        }
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        Gauge_UI.SetFill(currentEnergy);

    }
}
