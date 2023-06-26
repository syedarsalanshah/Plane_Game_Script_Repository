using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propeller_rotation : MonoBehaviour
{
    public float propeller_value = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(propeller_value, 0, 0);
    }
}
