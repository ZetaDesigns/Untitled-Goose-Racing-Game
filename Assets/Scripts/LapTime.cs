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
        var t1 = System.TimeSpan.Parse(getDisplayText(bestMillisecond, bestSecond, bestMinute));
        var t2 = System.TimeSpan.Parse(getDisplayText(MillisecondCount, SecondCount, MinuteCount));

        if (t1 > t2 || bestMillisecond == 0 && bestSecond == 0 && bestMinute == 0)
        {
            bestMillisecond = MillisecondCount;
            bestSecond = SecondCount;
            bestMinute = MinuteCount;

            Debug.Log(getDisplayText(bestMillisecond, bestSecond, bestMinute));
            bestTimeText.GetComponent<TextMeshProUGUI>().text = getDisplayText(bestMillisecond, bestSecond, bestMinute);
        }
        MillisecondCount = SecondCount = MinuteCount = 0;

    }

    string getDisplayText(float ms, float s, float m)
    {
        string displayText = "";

        if (m <= 9)
        {
            displayText += "0" + m.ToString() + ":";
        }
        else
        {
            displayText += m.ToString() + ":";
        }

        if (s <= 9)
        {
            displayText += "0" + s.ToString() + ":";
        }
        else
        {
            displayText += s.ToString() + ":";
        }
        if (ms <= 9)
        {
            displayText += "0" + ms.ToString("F0");
        }
        else
        {
            displayText += ms.ToString("F0");
        }

        return displayText;
    }
}
