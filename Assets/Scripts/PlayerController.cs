using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MeteorSpawnScript meteorSpawnScript;
    public GameObject left_boundary_point;
    public GameObject right_boundary_point;
    public float maxSpeed = 5f;

    public ShipPart[] shipParts = new ShipPart[5];
    //public ShipPart dome, leftFront, leftBack, rightFront, right
    bool[] broken = new bool[5] {false,false,false,false,false };
    public GameObject[] brokenObj = new GameObject[5];

    public GameObject logic;

    private Quaternion start_rot;
    public float tilt_factor = 3f;

    public AudioSource audioData;
    public AudioClip meteor_collision_audio;
    public AudioClip collectible_collision_audio;  


    // Start is called before the first frame update
    void Start()
    {
        start_rot = this.transform.rotation;
        //audioData = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody>().velocity = new Vector3(move * maxSpeed, 0f, 0f);

        if (this.transform.position.x < left_boundary_point.transform.position.x)
        {
            this.transform.position = new Vector3(left_boundary_point.transform.position.x,
                                                    this.transform.position.y,
                                                    this.transform.position.z);
        }
        else if (this.transform.position.x > right_boundary_point.transform.position.x)
        {
            this.transform.position = new Vector3(right_boundary_point.transform.position.x,
                                                  this.transform.position.y,
                                                  this.transform.position.z);
        }

        if (true)
        {
            Quaternion localRotation = Quaternion.Euler(0, (-move * maxSpeed)*tilt_factor, 0f);
            transform.rotation = start_rot * localRotation;
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            //play some collision audio
            audioData.PlayOneShot(meteor_collision_audio);
            GameObject part = collision.contacts[0].thisCollider.gameObject;
            // despawn Meteor
            meteorSpawnScript.AddDeactivatedMeteor(collision.gameObject);

            //only damage ship parts
            if (!part.Equals(this.gameObject))
            {
                broken[part.GetComponent<ShipPart>().partNumber] = true;
                // shattering logic - TODO frage dein Designer -> pivot point
                GameObject g = Instantiate(brokenObj[part.GetComponent<ShipPart>().partNumber],
                                      part.transform.position + new Vector3(0.0f,0.5f,0.0f),
                                      transform.rotation);
                //g.transform.SetParent(gameObject.transform);
                g.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                //g.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f,0.0f,-5.0f));
                Destroy(g, 5.0f);
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
                        Debug.Log("You lose!");
                    }
                }
                
            }
        }

        if (collision.gameObject.CompareTag("Collectible"))
        {
            audioData.PlayOneShot(collectible_collision_audio);
            meteorSpawnScript.AddDeactivatedCollectible(collision.gameObject);
            //TODO add method 
            logic.GetComponent<GameLogic>().AddCollectible();
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
