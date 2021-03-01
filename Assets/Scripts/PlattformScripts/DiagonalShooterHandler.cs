
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalShooterHandler : MonoBehaviour
{

    public GameObject BulletPrefab;
    public float shootingFrequenz = 0.5f;
    private float shootingFrequenzSum = 0;

    private ArrayList diagonals = new ArrayList();

    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            diagonals.Add(transform.GetChild(i).gameObject);
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.parent.GetComponent<PlatformHandler>().isFalling)
        {
            if (transform.parent.transform.Find("PlatformElevateTrigger").GetComponent<PlayerEnterTrigger>().triggered)
            {
                shootingFrequenzSum += Time.deltaTime;
                if (shootingFrequenzSum >= shootingFrequenz)
                {
                    Shoot(diagonals[currentIndex] as GameObject);
                    currentIndex++;
                    if (currentIndex > 2)
                    {
                        currentIndex = 0;
                    }

                    shootingFrequenzSum = 0;

                }

            }
        }

        
    }

    void Shoot(GameObject diagonale)
    {
        Transform d1 = diagonale.transform.GetChild(0);
        Transform d2 = diagonale.transform.GetChild(1);

        var bullet = Instantiate(BulletPrefab);
        bullet.transform.position = d1.transform.position + new Vector3(0,1,0);
        bullet.GetComponent<DiagonalShooterBullet>().Shoot(d2.position - d1.position);


    }
}
