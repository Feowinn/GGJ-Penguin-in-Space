﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public SceneManager sceneManager;

    private int parts_collected = 0;
    private int parts_needed_for_repair = 5;
    private bool puzzle_completed = false;
    private bool puzzle_failed = false;


    // Update is called once per frame
    void Update()
    {
        if(parts_collected == parts_needed_for_repair)
        {
            //spawn new scene in our scene
            SceneManager.LoadScene("PuzzleGame", LoadSceneMode.Additive);
            parts_collected = 0;
        }

        if (puzzle_completed)
        {
            SceneManager.UnloadSceneAsync("PuzzleGame");
            puzzle_completed = false;
            //call method to repair ship
        }

        if (puzzle_failed)
        {
            SceneManager.UnloadSceneAsync("PuzzleGame");
            puzzle_failed = false;
        }
        
    }

    public void AddCollectible()
    {
        parts_collected += 1;
    }
}