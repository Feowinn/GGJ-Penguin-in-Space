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

    public Countdown countdown;

    int currentCorrectParts = 0;
    private int parts_needed_for_repair = 5;

    Canvas canvas;

    private void Start()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            //save locations
            startLocations[i] = parts[i].transform.localPosition;
        }

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
            //parts[i].transform.localPosition = startLocations[i];
            parts[i].GetComponent<DragHandler>().enabled = true;
        }

        // TODO change Game
        canvas.enabled = true;
        outline.SetActive(true);

        //set random start Locations
        bool[] chosen = new bool[5] { false, false, false, false, false };
        for (int i = 0; i < parts.Length; i++)
        {
            int random = 0;
            random = Random.Range(0, 4 - i);
            while (chosen[random])
            {
                    random++;
                    Debug.Log(random);
            }
            Debug.Log(startLocations[random]);
            chosen[random] = true;
            parts[i].transform.localPosition = startLocations[random];
            
        }
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
            countdown.UpdateTime(puzzleTimeLeft, puzzleTime);
            if (puzzleTimeLeft <= 0f)
            {
                Time.timeScale = 1f;
                Debug.Log("Timeout!");
                canvas.enabled = false;
            }

        }
    }

}
