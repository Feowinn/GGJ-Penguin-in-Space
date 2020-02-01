using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnScript : MonoBehaviour
{

    //locations for spawn
    public GameObject spawnPoint0;
    public GameObject spawnPoint1;
    public GameObject dir_helper;

    public GameObject meteor;
    private List<GameObject> deactivated_meteors = new List<GameObject>();

    private System.Random random = new System.Random();
   
    //timer info for meteorite spwan delays and logic
    private float timer = 0.0f;
    private float time_frame = 0.0f;
    private float spawn_delay = 2.0f;

    void Start()
    {
        for (int i = 0; i <= 10; i++)
        {
            GameObject meteor_ = Instantiate(meteor, spawnPoint0.transform.position, Quaternion.identity);
            meteor_.SetActive(false);
            deactivated_meteors.Add(meteor_);
        }
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawn_delay)
        {
            //spawn some meteorites
            if (deactivated_meteors.Count > 0)
            {
                GameObject meteor_ = deactivated_meteors[0];
                meteor_.SetActive(true);
                deactivated_meteors.RemoveAt(0);
                meteor_.transform.position = GetRandomPosition(spawnPoint0.transform.position, spawnPoint1.transform.position);
                Vector3 force_dir = dir_helper.transform.position - spawnPoint0.transform.position;
                meteor_.GetComponent<Rigidbody>().velocity = 1.0f*force_dir;
                //reset timer
                timer = 0.0f;
            }
        }
    }

    private Vector3 GetRandomPosition(Vector3 start, Vector3 end)
    {
        int randomNumber = random.Next(0, 100);
        return start + (end - start) * (float)randomNumber/100.0f;
    }

    public void AddDeactivatedMeteor(GameObject meteor_)
    {
        meteor_.SetActive(false);
        this.deactivated_meteors.Add(meteor_);
    }
}
