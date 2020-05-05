using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    float lifeTime = 2f;
    public GameObject explosionPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.parent.position += transform.parent.forward * Time.deltaTime * speed;
        lifeTime -= Time.deltaTime;        
    }
    private void Update()
    {
        if (lifeTime <= 0)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().DamageHealth(5);
        }
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.transform.parent.gameObject);
    }
}
