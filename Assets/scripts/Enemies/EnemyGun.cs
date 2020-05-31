using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Assets.Standard_Assets.Utility;

public class EnemyGun : MonoBehaviour
{
    public bool EyeballGun = false;
    Transform player;
    bool inRange = false;
    float fireDelay = 2f;
    float repeatDelay = .2f;
    int repeats = 0;
    public GameObject projectilePrefab;
    public float range = 15;
    public float aimSpeed = 20f;
    private Vector3 offset = new Vector3(0.1f, 0, 0);
    public float barrelLength = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ThirdPersonUserControl thirdPersonUserControl = FindObjectOfType<ThirdPersonUserControl>();
        if (thirdPersonUserControl != null)
        {
            player = thirdPersonUserControl.gameObject.transform;
        }

        if (!EyeballGun) offset = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || Global.PlayerCloaked || !CanLookAt(player)) return;
        Vector3 dist = (player.position + Vector3.up) - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dist, Vector3.up), aimSpeed * Time.deltaTime);
        if (fireDelay < 0)
        {
            Fire();
        }
        else
        {
            fireDelay -= Time.deltaTime;
        }
    }

    public bool CanLookAt(Transform obj)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, (obj.position - transform.position), out hit, range);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 8)
            {
                //if (dist.sqrMagnitude < rangeSquared)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    public void Fire()
    {
        
        FireWeapon();
        Invoke("FireWeapon", repeatDelay);
        Invoke("FireWeapon", repeatDelay * 2f); //dmc todo: this is lazy, do it proper so that slow motion will work
        if (EyeballGun)
        {
            Invoke("FireWeapon", repeatDelay * 3f);
            Invoke("FireWeapon", repeatDelay * 4f);
            Invoke("FireWeapon", repeatDelay * 5f);
        }
        fireDelay = 2f;
    }

    void FireWeapon()
    {
        if (Projectile.count > 7) return;//limit bullets alive for now. You should pool them
        offset = -offset;
        Instantiate(projectilePrefab, transform.position + transform.rotation * (offset + Vector3.forward * barrelLength), transform.rotation);
    }
}
