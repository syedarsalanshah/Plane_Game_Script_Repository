using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass_Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Artifical_Compass;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("Compass "+Artifical_Compass.transform.localEulerAngles.y);
        transform.rotation = Quaternion.Euler(0,0,-Artifical_Compass.transform.localEulerAngles.y);
    }
}
