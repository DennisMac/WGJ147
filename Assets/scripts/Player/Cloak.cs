using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Assets.Standard_Assets.Utility;

public class Cloak : MonoBehaviour
{
    //bool cloaked = false;
    bool depleted = false;
    float depletedDelay = 2f;
    float sinceDepleted = 0f;
    float maxEnergy = 100;
    float currentEnergy = 50;
    [SerializeField]
    float chargeRate = 10f;

    public static float powerUsage = 10f; // static to make it easy to cheat

    [SerializeField]
    Hourglass Gauge_UI;
    [SerializeField]
    Material[] cloakedMaterial;
    [SerializeField]
    Material[] decloakedMaterial;
    SkinnedMeshRenderer skinnedMeshRenderer;

    MeshRenderer[] meshRenderers;
    Material[][] decloakedExtraMaterials;
    Material[] decloackedExtraSingleMaterial;
    AudioSource audioSource;
    [SerializeField]
    AudioClip cloakClip;
    [SerializeField]
    GameObject sparklesPrefab;

    public static List<AICharacterControl> aiCharacterControl;
    private bool wasCloaked;

    bool Cloaked
    {
        get { return Global.PlayerCloaked; }
        set
        {
            if (Global.PlayerCloaked == value) return;
  
            Global.PlayerCloaked = value;
            if (Global.PlayerCloaked)
            {
                foreach (AICharacterControl aicc in aiCharacterControl)
                {
                    aicc.SetTarget(null);
                }
                audioSource.pitch = 0.3f + 0.7f * Time.timeScale; //hack because mixer doesn't work in WebGL
                audioSource.PlayOneShot(cloakClip);
                GameObject sparkles = Instantiate(sparklesPrefab, transform.position + Vector3.up + Camera.main.transform.forward * 0.25f, Quaternion.identity);
                sparkles.transform.parent = this.transform;
                skinnedMeshRenderer.materials = cloakedMaterial;
                foreach (MeshRenderer mr in meshRenderers)
                {
                    if (mr.materials.Length == 0)
                    {
                        mr.material = cloakedMaterial[0];
                    }
                    else
                    {
                        mr.materials = cloakedMaterial;
                    }
                }
            }
            else
            {
                foreach (AICharacterControl aicc in aiCharacterControl)
                {
                    RaycastHit hit;
                    Physics.Raycast(aicc.transform.position, (transform.position - aicc.transform.position), out hit, 100);
                    if (hit.collider.gameObject.layer == 8)
                    {
                        aicc.SetTarget(this.transform);
                    }
                    else
                    {
                        aicc.SetTarget(null);
                    }
                }


                skinnedMeshRenderer.materials = decloakedMaterial;
                int j = 0;
                foreach (MeshRenderer mr in meshRenderers)
                {
                    if (mr.materials.Length == 0)
                    {
                        mr.material = decloackedExtraSingleMaterial[j];
                    }
                    else
                    {
                        mr.materials = decloakedExtraMaterials[j];
                    }
                    j++;
                }
            }
        }
    }



    public bool IsCloaked { get { return Global.PlayerCloaked; } }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aiCharacterControl = new List<AICharacterControl>(FindObjectsOfType<AICharacterControl>());
        foreach (AICharacterControl aicc in aiCharacterControl)
        {
            aicc.SetTarget(this.transform);
        }
        Gauge_UI = FindObjectOfType<Hourglass>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        decloakedExtraMaterials = new Material[meshRenderers.Length][];
        decloackedExtraSingleMaterial = new Material[meshRenderers.Length];
        int k = 0;
        foreach (MeshRenderer mr in meshRenderers)
        {
            if (mr.materials.Length == 0)
            {
                decloackedExtraSingleMaterial[k] = mr.material;
            }
            else
            {
                decloakedExtraMaterials[k] = new Material[mr.materials.Length];
                decloakedExtraMaterials[k] = mr.materials;
            }
            k++;
        }

        Cloaked = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (depleted)
        { //charge a little before you can re-engage
            if ((sinceDepleted += Time.deltaTime) > depletedDelay) depleted = false;
        }
        else
        {
            if (Input.GetKeyDown("e"))
            {
                Cloaked = !Cloaked;
            }                
        }       


        if (Cloaked)
        {            
            currentEnergy -= Time.deltaTime * powerUsage;
            if (currentEnergy <= 0)
            {
                depleted = true;
                Cloaked = false;
                sinceDepleted = 0f;
            }
            if (Global.PlayerFiring)
            {
                wasCloaked = Cloaked || wasCloaked;
                Cloaked = false;
            }
        }
        else
        {
            if (wasCloaked && !Global.PlayerFiring)
            {
                Cloaked = true;
                wasCloaked = false;
            }
            currentEnergy += Time.deltaTime * chargeRate;                 
        }

        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        Gauge_UI.SetFill(currentEnergy);


        //check line of sight on the enemies
        if (!Cloaked && Random.Range(0,60) < 1) //do this every second or so
        {
            foreach (AICharacterControl aicc in aiCharacterControl)
            {
                RaycastHit hit;
                Physics.Raycast(aicc.transform.position, (transform.position - aicc.transform.position), out hit, 100);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.layer == 8)
                    {
                        aicc.SetTarget(this.transform);
                    }
                }
            }
        }
    }
}