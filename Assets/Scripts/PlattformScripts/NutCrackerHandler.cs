using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutCrackerHandler : BaseRotator
{

    public Transform wall_1;
    public Transform wall_2;
    public Transform wall_3;

    public float mainRotationSpeed;
    public float wallsRoationSpeed;
    private int dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = (direction == Direction.Right) ? -1 : 1;
        transform.Rotate(new Vector4(0, 1, 0), dir * mainRotationSpeed * Time.deltaTime);

        wall_1.Rotate(new Vector4(0, 0, 1 ), dir * wallsRoationSpeed * Time.deltaTime);
        wall_2.Rotate(new Vector4(0, 0, 1), dir * wallsRoationSpeed * Time.deltaTime);
        wall_3.Rotate(new Vector4(0, 0, 1), dir * wallsRoationSpeed * Time.deltaTime);
    }
}
