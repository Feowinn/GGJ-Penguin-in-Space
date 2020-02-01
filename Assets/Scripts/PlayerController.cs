using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    //public ShipPart[] parts = new ShipPart[5];
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
            //only damage ship parts
            if (!part.Equals(this.gameObject))
            {
            broken[part.GetComponent<ShipPart>().partNumber] = true;
            part.SetActive(false);
            //part.GetComponent<ShipPart>()

            }

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
        //parts[shipNumber].enabled = true;
        shipParts[shipNumber].gameObject.SetActive(true);
    }
}
