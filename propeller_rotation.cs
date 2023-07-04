using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class propeller_rotation : MonoBehaviour
{
   /* public int Space_Counter = 0;*/

    public bool fall = false;
    public bool fall_zero = false;
    public bool Engine_ON = false;
    public float propeller_value = 5;
    // Start is called before the first frame update


    public GameObject Plane_as_Object;
    private plane_Move Planes_Script_for_flying;


    void Start()
    {
        Planes_Script_for_flying = Plane_as_Object.GetComponent<plane_Move>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Engine_ON == false)
        {

            Planes_Script_for_flying.rotationSpeed = 0f;
            Planes_Script_for_flying.tiltRotate_speed = 0f;
            Planes_Script_for_flying.moveSpeed = 0f;
            Planes_Script_for_flying.sideSpeed = 0f;
          /*  Planes_Script_for_flying.translationInput = 0;*/
           /* Planes_Script_for_flying.mouseInput = 0;*/
                propeller_value = 0;
            
            if (Input.GetKeyUp(KeyCode.Space))
            {
                


                    Planes_Script_for_flying.rotationSpeed = 100f;
                    Planes_Script_for_flying.tiltRotate_speed = 50f;
                    Planes_Script_for_flying.moveSpeed = 15f;
                    Planes_Script_for_flying.sideSpeed = 10f;
                /*Planes_Script_for_flying.translationInput ;*/
                /* Planes_Script_for_flying.mouseInput = 0;*/
                    fall_zero = false;
                    propeller_value = 60;
                    Engine_ON = true;
                    
                
            }
        }


        if (Engine_ON == true)
        {

            if (Input.GetKeyUp(KeyCode.R))
            {
                Planes_Script_for_flying.rotationSpeed = 50f;
                Planes_Script_for_flying.tiltRotate_speed = 25f;
                Planes_Script_for_flying.moveSpeed = 5f;
                Planes_Script_for_flying.sideSpeed = 2f;
                fall = true;
                propeller_value = 20;


            }


            if (fall == true)
            {
                Planes_Script_for_flying.gameObject.transform.Translate(Vector3.down * 3 * Time.deltaTime);
                if (Input.GetKeyUp(KeyCode.E))
                {
                    Planes_Script_for_flying.rotationSpeed = 0f;
                    Planes_Script_for_flying.tiltRotate_speed = 0f;
                    Planes_Script_for_flying.moveSpeed = 0f;
                    Planes_Script_for_flying.sideSpeed = 0f;
                    fall_zero = true;
                    StartCoroutine(falling());
                    Engine_ON = false;
                    propeller_value = 0;


                }

            }

        }

        if(fall_zero == true)
        {
            Planes_Script_for_flying.gameObject.transform.Translate(Vector3.down * 6 * Time.deltaTime);
        }

        transform.Rotate(propeller_value, 0, 0);
    }
    IEnumerator falling()
    {
        yield return new WaitForSecondsRealtime(5f);
        fall = false;
    }
}
