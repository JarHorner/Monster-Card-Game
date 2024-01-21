using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TimeTracker timeTracker;

    void Start()
    {
       
    }

    void Update()
    {
        if (timeTracker.timeRunning)
        {
            timeTracker.TrackTime();
        }
    }

    public void setCard(GameObject position, CardDisplay cardSelected)
    {

    }
}
