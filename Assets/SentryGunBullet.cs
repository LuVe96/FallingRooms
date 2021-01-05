using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryGunBullet : MonoBehaviour
{

    private Vector3 direction;
    public float speed = 3;

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;

    }

    public void Shoot(Vector3 _direction)
    {
        direction = _direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
    }
}
