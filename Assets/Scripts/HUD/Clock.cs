using System;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

    private Text textClock;

    // Use this for initialization
    void Start()
    {
        textClock = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update ()
    {
        UpdateTime();
	}

    public void UpdateTime()
    {
        DateTime time = DateTime.Now;
        string hour = UtilityFunctions.LeadingZero(time.Hour);
        string minute = UtilityFunctions.LeadingZero(time.Minute);
        string second = UtilityFunctions.LeadingZero(time.Second);
        textClock.text = hour + ":" + minute + ":" + second;
    }
}
