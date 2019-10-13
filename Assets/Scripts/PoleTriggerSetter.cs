using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleTriggerSetter : MonoBehaviour
{
    public BoxCollider StartLine;
    public BoxCollider HalfWayLine;

    public LapTime lapTimer;

    public static bool roundStarted;

    private void Start()
    {
        StartLine.enabled = true;
        HalfWayLine.enabled = false;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other == StartLine && roundStarted == false)
        {
            roundStarted = true;
            lapTimer.roundStarted = roundStarted;
            HalfWayLine.enabled = true;
            StartLine.enabled = false;
        } else if (other == HalfWayLine && roundStarted == true)
        {
            HalfWayLine.enabled = false;
            StartLine.enabled = true;
        } else if (other == StartLine && roundStarted == true)
        {
            HalfWayLine.enabled = true;
            StartLine.enabled = false;
            lapTimer.stopTime();
        }
    }
}
