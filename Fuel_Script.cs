using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel_Script : MonoBehaviour
{
    [SerializeField] private int value = 20;

    
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(0, 10 * Time.deltaTime * value, 0);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plane")){
            Destroy(gameObject);
          

        }
       /*
        if (other.gameObject.CompareTag("Respawn"))
        {
            print("no");
        }*/
    }
}
