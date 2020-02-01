using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outline : MonoBehaviour
{
    public minigameLogic minigameLogic;

    int currentCorrectParts = 0;
    private int parts_needed_for_repair = 5;

    public void partCorrectlyPlaced()
    {
        currentCorrectParts++;
        if (currentCorrectParts == parts_needed_for_repair)
        {
            //TODO Game is done!
        }
    }
}
