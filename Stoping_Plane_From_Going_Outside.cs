using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoping_Plane_From_Going_Outside : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -1423 || transform.position.x > 231 || transform.position.z < -845 || transform.position.z > 851 || transform.position.y < -50)
        {
            print("Gameover");
        }
    }
}
