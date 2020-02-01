using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameLogic : MonoBehaviour
{
    public int gameParts = 5;
    public float puzzleTime = 20f;
    float puzzleTimeLeft;
    public float slowdown = 2f;

    public GameLogic gameLogic;
    public GameObject outline;
    public GameObject[] parts = new GameObject[5];
    public Vector3[] startLocations = new Vector3[5];

    int currentCorrectParts = 0;
    private int parts_needed_for_repair = 5;

    Canvas canvas;

    private void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
        canvas.enabled = false;
        
    }

    

    // Start is called before the first frame update
    public void StartMinigame(int number)
    {
        Time.timeScale = 1 / slowdown;
        puzzleTimeLeft = puzzleTime;
        currentCorrectParts = 0;
        //reset part locations and enable dragHandler
        for (int i=0; i<5; i++)
        {
            parts[i].transform.localPosition = startLocations[i];
            parts[i].GetComponent<DragHandler>().enabled = true;
        }

        // TODO change Game
        canvas.enabled = true;
        outline.SetActive(true);

        
    }


    public void PartCorrectlyPlaced()
    {
        currentCorrectParts++;
        if (currentCorrectParts == parts_needed_for_repair)
        {
            Time.timeScale = 1f;
            //TODO Game is won!
            gameLogic.GameCompleted();
            canvas.enabled = false;
        }
    }

    public void Update()
    {
        if (canvas.enabled)
        {
            puzzleTimeLeft -= Time.deltaTime * slowdown;
            if (puzzleTimeLeft <= 0f)
            {
                Time.timeScale = 1f;
                Debug.Log("Timeout!");
                canvas.enabled = false;
            }

        }
    }

}
