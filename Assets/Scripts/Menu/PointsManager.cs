using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{

    private float scoreTime = 0;
    private int scoreShocks = 0;

    public float refTime;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreTime += Time.deltaTime;
        timeText.text = createTimeString(scoreTime);
    }

    private string createTimeString(float seconds, bool useMS = false)
    {
        int s = (int)seconds % 60;
        int m = (int)seconds / 60;
        int ms = (int)((seconds - (int)seconds) * 100);
        string time = (m.ToString().Length < 2 ? "0" : "") + m + ":" + (s.ToString().Length < 2 ? "0" : "") + s;
        if (useMS)
        {
            time += ":" + (ms.ToString().Length < 2 ? "0" : "") + ms;
        }
      
        return time;
    }

    public void startScoreTracker()
    {
        scoreTime = 0;
        scoreShocks = 0;
    }

    public void addShock()
    {
        scoreShocks++;
    }

    public Score calculatePoints()
    {
        float timePoints = refTime / scoreTime * 6000;
        int points = (int)timePoints - (scoreShocks * 200);
        int stars = 0;

        switch (points)
        {
            case 0: stars = 1; break;
            case 33333: stars = 2; break;

            default:
                break;
        }

        if(points < 4500)
        {
            stars = 1;
        } else if(points < 6000)
        {
            stars = 2;
        } else if(points >= 6000)
        {
            stars = 3;
        }

        return new Score(createTimeString( scoreTime, true), scoreShocks, points, stars);
    }

    public struct Score
    {
        public string scoreTime;
        public int scoreShocks;
        public int points;
        public int stars;

        public Score(string scoreTime, int scoreShocks, int points, int stars)
        {
            this.scoreTime = scoreTime;
            this.scoreShocks = scoreShocks;
            this.points = points;
            this.stars = stars;
        }
    }
}

