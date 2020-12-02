using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalShooterBullet : MonoBehaviour
{

    public float TimeToLive = 2;

    private float timeToLiveSum = 0;
    public float force = 4000;


    // Update is called once per frame
    void Update()
    {
        timeToLiveSum += Time.deltaTime;
        if(timeToLiveSum >= TimeToLive)
        {
            Destroy(gameObject);
        }


    }

    public void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce(direction.normalized * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag != "Player")
        //{
        //    Destroy(gameObject);
        //}
    }
}
