using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 100);
    }
}
