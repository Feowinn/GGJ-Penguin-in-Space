using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScript : MonoBehaviour
{
    private float rotation_per_tile;
    public Vector3 position;
    public float rotation_speed = 0.01f;
    public Vector3 start_rotation;
    public Vector3 default_rot = new Vector3(0f, 0f, 20f);

    private int num_tiles = 1;
    public GameObject[] Tilelist = new GameObject[1];
    public List<GameObject> active_tiles = new List<GameObject>();

    private System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            //spawn enough tiles to 
            GameObject new_tile = Instantiate(Tilelist[random.Next(0, num_tiles - 1)]);
            active_tiles.Add(new_tile);
            new_tile.transform.position = position;
            new_tile.transform.Rotate(start_rotation+(float)i*default_rot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // rotate tiles and spawn new ones while deleting old

        // if (new_spawncondition)
        bool newspawncondition = active_tiles[0].transform.rotation.eulerAngles.z < 240.0f;
        //bool newspawncondition = false;
        
        if (newspawncondition)
        {
            GameObject old_tile = active_tiles[0];
            active_tiles.RemoveAt(0);
            Vector3 new_start_rot = old_tile.transform.rotation.eulerAngles;
            Destroy(old_tile);

            GameObject new_tile = Instantiate(Tilelist[random.Next(0, num_tiles-1)]);
            active_tiles.Add(new_tile);
            new_tile.transform.position = position;
            new_tile.transform.eulerAngles = old_tile.transform.eulerAngles;
            new_tile.transform.Rotate( 5 * default_rot);
        }

        rotateAllTiles();
    }

    void rotateAllTiles()
    {
        float rot = rotation_speed * Time.deltaTime;
        foreach(GameObject g in active_tiles)
        {
            g.transform.Rotate(0f, 0f, -rot);
        }
    }
}
