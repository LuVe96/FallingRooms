using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftingTides : MonoBehaviour
{

    public bool horizontal;
    public bool invert;
    public float duration;
    public float range;

    private float timeSum;
    private Vector3 startPos;
    private ShiftPos shiftPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        if(invert)
        {
            shiftPos = horizontal ? ShiftPos.Right : ShiftPos.Bottom;
        }
        else
        {
            shiftPos = horizontal ? ShiftPos.Left : ShiftPos.Top;
        }


        Debug.Log("sP: " + shiftPos);
    }

    // Update is called once per frame
    void Update()
    {

        if (horizontal)
        {

            if(transform.localPosition.x <= (startPos.x - range)) 
            {
                shiftPos = ShiftPos.Left;
                timeSum = 0;
            }

            if (transform.localPosition.x >= (startPos.x + range))
            {
                shiftPos = ShiftPos.Right;
                timeSum = 0;
            }

            timeSum += Time.deltaTime;

            if (shiftPos == ShiftPos.Left)
            {
                Vector3 newPos = Vector3.Lerp(startPos - new Vector3(range, 0, 0), startPos + new Vector3(range, 0, 0), timeSum / duration);
                transform.localPosition = newPos;

            }

            if (shiftPos == ShiftPos.Right)
            {
                Vector3 newPos = Vector3.Lerp(startPos + new Vector3(range, 0, 0), startPos - new Vector3(range, 0, 0), timeSum / duration);
                transform.localPosition = newPos;
            }

        } else
        {
            if (transform.localPosition.y <= (startPos.y - range))
            {
                shiftPos = ShiftPos.Bottom;
                timeSum = 0;
            }

            if (transform.localPosition.y >= (startPos.y + range))
            {
                shiftPos = ShiftPos.Top;
                timeSum = 0;
            }

            timeSum += Time.deltaTime;

            if (shiftPos == ShiftPos.Bottom)
            {
                Vector3 newPos = Vector3.Lerp(startPos - new Vector3(0, range, 0), startPos + new Vector3(0, range, 0), timeSum / duration);
                transform.localPosition = newPos;

            }

            if (shiftPos == ShiftPos.Top)
            {
                Vector3 newPos = Vector3.Lerp(startPos + new Vector3(0, range, 0), startPos - new Vector3(0, range, 0), timeSum / duration);
                transform.localPosition = newPos;
            }
        }

    }

    public enum ShiftPos
    {
        Left, Right, Top , Bottom
    }
}

