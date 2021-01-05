using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShockerSpinner : MonoBehaviour
{
    public float rotationTime = 1;

    public GameObject [] lasers;


    private float timeSum = 0;
    private int currentIndex = 0;
    private int rotationDir = 0;


    // Start is called before the first frame update
    void Start()
    {
        rotationDir = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {

        timeSum += Time.deltaTime;

        if(rotationTime <= timeSum)
        {
            timeSum = 0;
            EnableLaser(currentIndex);
            
            if(rotationDir == 0)
            {
                if (currentIndex + 1 >= lasers.Length)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentIndex++;
                }
            }else
            {
                if (currentIndex <= 0)
                {
                    currentIndex = lasers.Length-1;
                }
                else
                {
                    currentIndex--;
                }
            }
 


        }
    }

    void EnableLaser(int index)
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            if(i == index)
            {
                lasers[i].SetActive(true);
            }else
            {
                lasers[i].SetActive(false);
            }
        }
    }
}
