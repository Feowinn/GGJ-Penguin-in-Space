using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    [SerializeField] private Image countdownCircleTimer;
    [SerializeField] private TextMeshProUGUI countdownText;
    //[SerializeField] private float startTime = 30.0f;

    public void UpdateTime(float currentTime, float startTime)
    {
        float normalizedValue = Mathf.Clamp(currentTime / startTime, 0.0f, 1.0f);
        countdownCircleTimer.fillAmount = normalizedValue;
        //countdownCircleTimer.fillAmount = currentTime/maxTime;
        countdownText.text = (int)currentTime+"";

    }

}
