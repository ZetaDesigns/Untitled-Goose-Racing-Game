using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LapTime : MonoBehaviour
{
    public GameObject lapTimeText;
    public GameObject bestTimeText;

    public static float MinuteCount, SecondCount, MillisecondCount;
    public static float bestMinute, bestSecond, bestMillisecond = 0;
    public bool roundStarted;

    private void Start()
    {
        MinuteCount = SecondCount = MillisecondCount = bestMillisecond = bestSecond = bestMillisecond = 0;
    }
    void Update()
    {
        if(roundStarted)
        {
            MillisecondCount += Time.deltaTime * 10;

            if (MillisecondCount >= 10)
            {
                MillisecondCount = 0;
                SecondCount++;
            }

            if (SecondCount >= 60)
            {
                SecondCount = 0;
                MinuteCount++;
            }
            lapTimeText.GetComponent<TextMeshProUGUI>().text = getDisplayText(MillisecondCount, SecondCount, MinuteCount);
        }
    }

    public void stopTime()
    {
        if (MillisecondCount <= bestMillisecond && SecondCount <= bestSecond && bestMinute <= MinuteCount || bestMillisecond == 0 && bestSecond == 0 && bestMinute == 0)
        {
            bestMillisecond = MillisecondCount;
            bestSecond = SecondCount;
            bestMinute = MinuteCount;

            Debug.Log("Best time beaten");
            bestTimeText.GetComponent<TextMeshProUGUI>().text = getDisplayText(bestMillisecond, bestSecond, bestMinute);
        }
        MillisecondCount = SecondCount = MinuteCount = 0;

    }

    string getDisplayText(float ms, float s, float m)
    {
        string displayText = "";

        if (MinuteCount <= 9)
        {
            displayText += "0" + MinuteCount.ToString() + ".";
        }
        else
        {
            displayText += MinuteCount.ToString() + ".";
        }

        if (SecondCount <= 9)
        {
            displayText += "0" + SecondCount.ToString() + ".";
        }
        else
        {
            displayText += SecondCount.ToString() + ".";
        }
        if (MillisecondCount <= 9)
        {
            displayText += "0" + MillisecondCount.ToString("F0");
        }
        else
        {
            displayText += MillisecondCount.ToString("F0");
        }

        return displayText;
    }
}
