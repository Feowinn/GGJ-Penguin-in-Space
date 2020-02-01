using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnScript : MonoBehaviour
{
  
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            GameObject meteorPlane = this.transform.parent.gameObject;
            meteorPlane.GetComponent<MeteorSpawnScript>().AddDeactivatedMeteor(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Collectible"))
        {
            GameObject meteorPlane = this.transform.parent.gameObject;
            meteorPlane.GetComponent<MeteorSpawnScript>().AddDeactivatedCollectible(collision.gameObject);
        }
    }
}
