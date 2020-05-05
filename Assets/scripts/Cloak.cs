using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Cloak : MonoBehaviour
{
    //bool cloaked = false;
    bool depleted = false;
    [SerializeField]
    float depletedDelay = 2f;
    float sinceDepleted = 0f;
    float maxEnergy = 100;
    float currentEnergy = 50;
    [SerializeField]
    float chargeRate = 10f;
    [SerializeField]
    float powerUsage = 2f;
    Hourglass Gauge_UI;
    [SerializeField]
    Material[] cloakedMaterial;
    [SerializeField]
    Material[] decloakedMaterial;
    SkinnedMeshRenderer skinnedMeshRenderer;

    MeshRenderer[] meshRenderers;
    Material[][] decloakedExtraMaterials;
    Material[] decloackedExtraSingleMaterial;


    public static List<AICharacterControl> aiCharacterControl;

    bool Cloaked
    {
        get { return Global.PlayerCloaked; }
        set
        {
            Global.PlayerCloaked = value;
            if (Global.PlayerCloaked)
            {
                foreach (AICharacterControl aicc in aiCharacterControl)
                {
                    aicc.SetTarget( null);
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
            }
        }
    }



    public bool IsCloaked { get { return Global.PlayerCloaked; } }

    private void Start()
    {

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

        Cloaked = true;
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
                if (Global.PlayerFiring) Cloaked = false;
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
            currentEnergy += Time.deltaTime * chargeRate;
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

        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        Gauge_UI.SetFill(currentEnergy);


        //check line of sight on the enemies
        if (!Cloaked)
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