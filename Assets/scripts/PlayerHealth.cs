using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public GameObject ragdollPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //healthSlider.value = 100f;
        healthSlider = FindObjectOfType<Slider>();
    }


    public void DamageHealth(int damage)
    {
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
