using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutCrackerHandler : MonoBehaviour
{

    public Transform wall_1;
    public Transform wall_2;
    public Transform wall_3;

    public float mainRotationSpeed;
    public float wallsRoationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector4(0, 0, 1), mainRotationSpeed * Time.deltaTime);

        wall_1.Rotate(new Vector4(0, 0, 1 ), wallsRoationSpeed * Time.deltaTime);
        wall_2.Rotate(new Vector4(0, 0, 1), wallsRoationSpeed * Time.deltaTime);
        wall_3.Rotate(new Vector4(0, 0, 1), wallsRoationSpeed * Time.deltaTime);
    }
}
