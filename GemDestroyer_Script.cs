using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDestroyer_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            print("gem deleted from gem script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            print(" triggered gem deleted from gem script");
        }
    }
}
