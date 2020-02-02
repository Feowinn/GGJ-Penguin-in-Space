using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showScrap : MonoBehaviour
{
    public GameObject[] scrapParts = new GameObject[5];
    // Start is called before the first frame update
    public void ShowScrapParts(int number)
    {
        int i = 0;
        for( ; i< number; i++)
        {
            scrapParts[i].SetActive(true);
        }
        for(; i< scrapParts.Length; i++)
        {
            scrapParts[i].SetActive(false);
        }
    }
}
