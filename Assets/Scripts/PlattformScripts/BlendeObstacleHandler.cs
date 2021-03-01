using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendeObstacleHandler : MonoBehaviour
{

    public Transform[] diagonals;
    public float upTime = 2;
    public float riseFallTime = 0.5f;
    public float upPos = -1.7f;
    private float upTimeSum = 0;
    private float riseFallTimeSum = 0;

    private WallState wallstate = WallState.down;
    private int currentDiagonale = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (diagonals.Length <= 0) return;

        if (wallstate == WallState.down)
        {
           StartCoroutine( MoveWalls(diagonals[currentDiagonale], riseFallTime, true));

           wallstate = WallState.rising;
        }

        if (wallstate == WallState.up)
        {
            upTimeSum += Time.deltaTime;

            if (upTimeSum >= upTime){
                StartCoroutine( MoveWalls(diagonals[currentDiagonale], riseFallTime, false));
                wallstate = WallState.falling;
                upTimeSum = 0;
            }

        }


     
    }

    IEnumerator MoveWalls(Transform diagonale, float time, bool rise)
    {
        //Transform dia2;
        //if (currentDiagonale + 1 < diagonals.Length)
        //{
        //    dia2 = diagonals[currentDiagonale + 1];
        //}
        //else
        //{
        //    dia2 = diagonals[0];
        //}

        var currentScale = diagonale.localScale;
        var targetScale = rise ? upPos : 0;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            diagonale.localScale = Vector3.Lerp(currentScale, new Vector3 (currentScale.x, currentScale.y, targetScale), t);
            //dia2.localScale = Vector3.Lerp(currentScale, new Vector3(currentScale.x, currentScale.y, targetScale), t);
            yield return null;
        }

        wallstate = rise ? WallState.up : WallState.down;
        if (!rise)
        {
            if (currentDiagonale + 1 < diagonals.Length)
            {
                currentDiagonale += 1;
            }
            else
            {
                currentDiagonale = 0;
            }
            //if (currentDiagonale + 2 < diagonals.Length)
            //{
            //    currentDiagonale += 2;
            //} else
            //{
            //    currentDiagonale = 0;
            //}
        }
    }

    enum WallState
    {
        up, rising, down, falling
    }

}
