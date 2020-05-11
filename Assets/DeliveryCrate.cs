using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class DeliveryCrate : MonoBehaviour
{

    public GameObject ExplosionPrefab;
    public GameObject cratePiecesPrefab;
    public GameObject botPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ExplosionPrefab, transform.position + Vector3.up, Quaternion.identity);
        Instantiate(cratePiecesPrefab, transform.position + Vector3.up, Quaternion.identity);

        GameObject bot =  Instantiate(botPrefab, transform.position, Quaternion.identity);
        Cloak.aiCharacterControl.Add(bot.GetComponent<AICharacterControl>());
        Destroy(transform.parent.gameObject);
    }

}
