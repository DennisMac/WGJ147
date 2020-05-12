using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyDamage : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    [SerializeField]
    private float health = 100;
    public GameObject[] bodyParts;
    List<GameObject> bodyPartsList;
    public GameObject delayedExplosionPrefab;
    public GameObject deliveryCrate;

    public void Damage( float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 100; //health resets each time he loses a body part
            if (bodyPartsList.Count > 0) //drop a body part off
            {
                GameObject go = bodyPartsList[0];
                bodyPartsList.RemoveAt(0);
                go.layer = 11;
                go.transform.parent = null;
                go.AddComponent<Rigidbody>();
                go.GetComponent<BoxCollider>().enabled = true;
                go.AddComponent<ObjectDestroyer>();
            }
            if (bodyPartsList.Count <= 0)
            {
                Cloak.aiCharacterControl.Remove(this.GetComponent<AICharacterControl>());
                Destroy(this.gameObject);
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                if (deliveryCrate != null)//necessary for roller bots that dont respawn yet
                {
                    Instantiate(deliveryCrate, transform.position + 8 * Vector3.up, Quaternion.identity);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bodyPartsList = new List<GameObject>(bodyParts);
        foreach (GameObject go in bodyPartsList)
        {
            go.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
