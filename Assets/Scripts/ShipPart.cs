using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour
{
    public int partNumber = 0;
    public PlayerController spaceShip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            Debug.Log("A meteor hit " + name + "!");

            // play animation

            // let the ship handle the destruction
            spaceShip.ShipPartIsDamaged(partNumber);
        }
    }
}
