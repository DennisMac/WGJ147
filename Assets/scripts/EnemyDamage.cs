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

    public void Damage( float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 100;
            if (bodyPartsList.Count > 0)
            {
                GameObject go = bodyPartsList[0];
                bodyPartsList.RemoveAt(0);

                //go.GetComponentInChildren<BoxCollider>().gameObject.layer = 11;
                go.layer = 11;
                go.transform.parent = null;
                go.AddComponent<Rigidbody>();
                go.GetComponent<BoxCollider>().enabled = true;
            }
            if (bodyPartsList.Count <= 0)
            {
                Cloak.aiCharacterControl.Remove(this.GetComponent<AICharacterControl>());
                Destroy(this.gameObject);
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
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
