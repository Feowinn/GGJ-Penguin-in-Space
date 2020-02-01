﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MeteorSpawnScript meteorSpawnScript;
    public float maxSpeed = 5f;

    public ShipPart[] shipParts = new ShipPart[5];
    //public ShipPart dome, leftFront, leftBack, rightFront, right
    bool[] broken = new bool[5] {false,false,false,false,false };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody>().velocity = new Vector3(move * maxSpeed, 0f, 0f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
        if (collision.gameObject.CompareTag("Meteor"))
        {
            GameObject part = collision.contacts[0].thisCollider.gameObject;
            // despawn Meteor
            meteorSpawnScript.AddDeactivatedMeteor(collision.gameObject);

            //only damage ship parts
            if (!part.Equals(this.gameObject))
            {
                broken[part.GetComponent<ShipPart>().partNumber] = true;
                part.SetActive(false);

            }

            // do something when all parts are broken
            if (part.Equals(this.gameObject))
            {
                for(int i = 4; i >=0; i--)
                {
                    if (!broken[i])
                    {
                        broken[i] = true;
                        shipParts[i].gameObject.SetActive(false);
                        break;
                    }
                    if (i == 0)
                    {
                        // TODO lose the game!
                    }
                }
                
            }
        }

        if (collision.gameObject.CompareTag("Collectible"))
        {
            meteorSpawnScript.AddDeactivatedCollectible(collision.gameObject);
            //TODO add method 
        }

    }
    public void ShipPartIsDamaged(int shipNumber)
    {
        broken[shipNumber] = true;
        //parts[shipNumber].enabled = false;
        shipParts[shipNumber].gameObject.SetActive(false);
    }

    public void ShipPartIsRepaired(int shipNumber)
    {
        broken[shipNumber] = false;
        shipParts[shipNumber].gameObject.SetActive(true);
    }

    public void RepairAnyPart()
    {
        for( int i = 0; i < shipParts.Length; i++)
        {
            if (broken[i])
            {
                shipParts[i].gameObject.SetActive(true);
                broken[i] = false;
                break;
            }
        }
    }
}
