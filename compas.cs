using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compas : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject From_GO;
    public GameObject To_GO;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("Minus location "+(To_GO.transform.position - From_GO.transform.position));

        Vector3 direction = From_GO.transform.position - To_GO.transform.position;
        transform.rotation  = Quaternion.LookRotation(direction,Vector3.up);
    }
}

