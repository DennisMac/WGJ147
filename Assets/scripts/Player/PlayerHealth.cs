using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Standard_Assets.Utility;


public class PlayerHealth : MonoBehaviour
{
    public static bool godMode = false;
    Slider healthSlider;
    public GameObject ragdollPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //healthSlider.value = 100f;
        healthSlider = FindObjectOfType<Slider>();
        Global.PlayerTransform = this.transform;
    }


    public void DamageHealth(int damage)
    {
        if (godMode) return;
        healthSlider.value -= damage;
        if (healthSlider.value <= 0)
        {
            GameObject ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
            UnityStandardAssets.Cameras.FreeLookCam.StaticRestartScene(2f);
            
            Destroy(gameObject); //temporary to show end
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
