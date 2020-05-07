using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

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

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ThirdPersonUserControl>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        Vector3 dist = (player.position + Vector3.up) - transform.position;
        transform.rotation = Quaternion.LookRotation(dist, Vector3.up);
        if (fireDelay < 0 && !Global.PlayerCloaked)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, (player.position - transform.position), out hit, range);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    //if (dist.sqrMagnitude < rangeSquared)
                    {
                        Invoke("FireWeapon", 0);
                        Invoke("FireWeapon", repeatDelay);
                        Invoke("FireWeapon", repeatDelay * 2f);
                        if (EyeballGun)
                        {
                            Invoke("FireWeapon", repeatDelay * 3f);
                            Invoke("FireWeapon", repeatDelay * 4f);
                            Invoke("FireWeapon", repeatDelay * 5f);
                        }
                        fireDelay = 2f;
                    }
                }
            }
        }
        else
        {
            fireDelay -= Time.deltaTime;
        }
    }

    Vector3 offset = new Vector3(.1f, 0, 0); //line up with the barrel of gun
    void FireWeapon()
    {
        offset = -offset;
        Instantiate(projectilePrefab, transform.position + transform.rotation * offset, transform.rotation);
    }
}
