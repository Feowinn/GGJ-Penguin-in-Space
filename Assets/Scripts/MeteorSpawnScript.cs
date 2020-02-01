using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnScript : MonoBehaviour
{

    //locations for spawn
    public GameObject spawnPoint0;
    public GameObject spawnPoint1;
    public GameObject dir_helper;

    public int max_number_meteors = 10;
    public float meteor_rotation_speed = 10.0f;
    public GameObject meteor;
    public GameObject[] meteors = new GameObject[6];
    private List<GameObject> deactivated_meteors = new List<GameObject>();

    public int max_number_collectibles = 5;
    public float collectible_rotation_speed = 2.0f;
    public GameObject collectible;
    private List<GameObject> deactivated_collectibles = new List<GameObject>();

    private System.Random random = new System.Random();
   
    //timer info for meteorite spwan delays and logic
    private float meteor_timer = 0.0f;
    private float collectible_timer = 0.0f;

    private float meteor_next_random_add_time = 0.0f;
    public float meteor_time_frame = 1.0f;
    public float meteor_spawn_delay = 0.3f;

    private float collectible_next_random_add_time = 0.0f;
    public float collectible_time_frame = 1.0f;
    public float collectible_spawn_delay = 0.3f;

    void Start()
    {
        for (int i = 0; i <= max_number_meteors; i++)
        {
            GameObject meteor_ = Instantiate(meteors[random.Next(0,5)], spawnPoint0.transform.position, Quaternion.identity);
            meteor_.SetActive(false);
            deactivated_meteors.Add(meteor_);
        }
        for (int i = 0; i <= max_number_collectibles; i++)
        {
            GameObject collectible_ = Instantiate(collectible, spawnPoint0.transform.position, Quaternion.identity);
            collectible_.SetActive(false);
            deactivated_collectibles.Add(collectible_);
        }
    }


    // Update is called once per frame
    void Update()
    {
        spawnMeteors();
        spawnCollectibles();
    }

    private Vector3 GetRandomPosition(Vector3 start, Vector3 end)
    {
        return start + (end - start) * (float)random.NextDouble();
    }

    public void AddDeactivatedMeteor(GameObject meteor_)
    {
        meteor_.SetActive(false);
        this.deactivated_meteors.Add(meteor_);
    }

    public void AddDeactivatedCollectible(GameObject collectible_)
    {
        collectible_.SetActive(false);
        this.deactivated_collectibles.Add(collectible_);
    }

    private void spawnMeteors()
    {
        meteor_timer += Time.deltaTime;
        if (meteor_timer >= meteor_spawn_delay + meteor_next_random_add_time)
        {
            //spawn some meteorites
            if (deactivated_meteors.Count > 0)
            {
                //take first deactivated meteor activate it and remove it from the deactivated-list
                GameObject meteor_ = deactivated_meteors[0];
                //meteor_.Mesh = GameObject.FindGameObjectsWithTag("MeteorMesh");
                meteor_.SetActive(true);
                deactivated_meteors.RemoveAt(0);

                //create random position on spawn line and apply orthogonal speed in plane
                meteor_.transform.position = GetRandomPosition(spawnPoint0.transform.position, spawnPoint1.transform.position);
                meteor_.transform.rotation = Random.rotation;
                Vector3 force_dir = dir_helper.transform.position - spawnPoint0.transform.position;
                meteor_.GetComponent<Rigidbody>().velocity = 1.0f * force_dir;
                Vector3 rot_dir = getRandomRotation();
                meteor_.GetComponent<Rigidbody>().AddTorque(meteor_rotation_speed*rot_dir);

                //reset timer
                meteor_next_random_add_time = meteor_time_frame * (float)random.NextDouble();
                meteor_timer = 0.0f;
            }
        }
    }

    private void spawnCollectibles()
    {
        collectible_timer += Time.deltaTime;
        if (collectible_timer >= collectible_spawn_delay + collectible_next_random_add_time)
        {
            //spawn some meteorites
            if (deactivated_collectibles.Count > 0)
            {
                //take first deactivated meteor activate it and remove it from the deactivated-list
                GameObject collectible_ = deactivated_collectibles[0];
                collectible_.SetActive(true);
                deactivated_collectibles.RemoveAt(0);

                //create random position on spawn line and apply orthogonal speed in plane
                collectible_.transform.position = GetRandomPosition(spawnPoint0.transform.position, spawnPoint1.transform.position);
                collectible_.transform.rotation = Random.rotation;
                Vector3 force_dir = dir_helper.transform.position - spawnPoint0.transform.position;
                collectible_.GetComponent<Rigidbody>().velocity = 1.0f * force_dir; //TODO add random factor to speed
                Vector3 rot_dir = getRandomRotation();
                collectible_.GetComponent<Rigidbody>().AddTorque(collectible_rotation_speed * rot_dir);

                //reset timer
                collectible_next_random_add_time = collectible_time_frame * (float)random.NextDouble();
                collectible_timer = 0.0f;
            }
        }
    }

    private Vector3 getRandomRotation()
    {
        float x = (float)random.NextDouble() * 2 - 1;
        float y = (float)random.NextDouble() * 2 - 1;
        float z = (float)random.NextDouble() * 2 - 1;
        return new Vector3(x,y,z);
    }
}
