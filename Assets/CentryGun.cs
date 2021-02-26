using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlatformHandler;

public class CentryGun : MonoBehaviour
{

    private GameObject player;
    public GameObject bullet;

    public float frequenzTime = 1;
    public float timeSum = 1.5f;

    public Transform gun;
    public PlayerEnterTrigger playerEnterTrigger;
    private bool triggered;

    public LightContainer lightLED;
    public Material stdLight;
    public Material warLight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerEnterTrigger.triggeredForSentryGun) { return; }

        Material[] mats = lightLED.renderer.materials;
        mats[lightLED.index] = warLight;
        lightLED.renderer.materials = mats;

        timeSum += Time.deltaTime;
        if(timeSum >= frequenzTime)
        {
            timeSum = 0;
            Shoot();
        }

        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    void Shoot()
    {
        var b = Instantiate(bullet);
        b.transform.position = gun.position;
        var forward = player.transform.Find("Astronaut").forward;
        Vector3 direction =  new Vector3(player.transform.position.x, transform.parent.position.y + 1, player.transform.position.z) + forward  - b.transform.position ;
        b.GetComponent<SentryGunBullet>().Shoot(direction);

    }

}
