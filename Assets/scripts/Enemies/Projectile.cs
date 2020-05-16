using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Standard_Assets.Utility;

public class Projectile : MonoBehaviour
{
    public static int count = 0;
    public float speed = 10f;
    float lifeTime = 2f;
    public GameObject explosionPrefab;
    [SerializeField]
    AudioClip[] audioClips;
    [SerializeField]
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        count++;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        audioSource.pitch = 0.3f + 0.7f * Time.timeScale; //hack because mixer doesn't work in WebGL
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

    private void OnDestroy()
    {
        count--;
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
