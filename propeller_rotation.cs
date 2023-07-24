using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class propeller_rotation : MonoBehaviour
{
    /* public int Space_Counter = 0;*/

    public bool fuel_spill_value;
    public bool fall = false;
    public bool fall_zero = false;
    public bool Engine_ON = false;
    public float propeller_value = 5;
    // Start is called before the first frame update
    private int on_off_value = 0;
    public GameObject Start_ON_imgae;
    public GameObject Start_OFF_imgae;

    public GameObject Plane_as_Object;
    private plane_Move Planes_Script_for_flying;

    public GameObject Camera_as_Object;
    private Fuel_Consumption Fuel_Script_for_Sound;

    void Start()
    {
        Planes_Script_for_flying = Plane_as_Object.GetComponent<plane_Move>();

        Fuel_Script_for_Sound = Camera_as_Object.GetComponent <Fuel_Consumption>();
        fuel_spill_value = false;
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

                fuel_spill_value = true;

                Fuel_Script_for_Sound.Plane_Engine_tune.volume = 1.0f;
                Planes_Script_for_flying.rotationSpeed = 100f;
                Planes_Script_for_flying.tiltRotate_speed = 50f;
                Planes_Script_for_flying.moveSpeed = 15f;
                Planes_Script_for_flying.sideSpeed = 14f;
               // Planes_Script_for_flying.translationInput;
                Planes_Script_for_flying.mouseInput = 0;
                fall_zero = false;
                propeller_value = 60;
                Engine_ON = true;


            }
        }


        if (Engine_ON == true)
        {

            /*if (Input.GetKeyUp(KeyCode.R))
            {
                Fuel_Script_for_Sound.Plane_Engine_tune.volume = 0.6f;
                Planes_Script_for_flying.rotationSpeed = 50f;
                Planes_Script_for_flying.tiltRotate_speed = 25f;
                Planes_Script_for_flying.moveSpeed = 5f;
                Planes_Script_for_flying.sideSpeed = 2f;
                fall = true;
                propeller_value = 20;


            }*/


            if (fall == true)
            {
                Planes_Script_for_flying.gameObject.transform.Translate(Vector3.down * 3 * Time.deltaTime);
              /*  if (Input.GetKeyUp(KeyCode.E))
                {
                    Fuel_Script_for_Sound.Plane_Engine_tune.volume = 0.0f;
                    Planes_Script_for_flying.rotationSpeed = 0f;
                    Planes_Script_for_flying.tiltRotate_speed = 0f;
                    Planes_Script_for_flying.moveSpeed = 0f;
                    Planes_Script_for_flying.sideSpeed = 0f;
                    fall_zero = true;
                    StartCoroutine(falling());
                    Engine_ON = false;
                    propeller_value = 0;


                }*/

            }

        }

        if(fall_zero == true)
        {
            Planes_Script_for_flying.gameObject.transform.Translate(Vector3.down * 6 * Time.deltaTime);
            Planes_Script_for_flying.gameObject.transform.Translate(Vector3.right * 6 * Time.deltaTime);

        }

        transform.Rotate(propeller_value, 0, 0);
    }
    IEnumerator falling()
    {
        yield return new WaitForSecondsRealtime(5f);
        fall = false;
    }


    public void Replacing_Space_ForButton()
    {
        if (Engine_ON == false) {

            on_off_value = 1;

            if(on_off_value == 1)
            {
                Start_ON_imgae.gameObject.SetActive(true);
                Start_OFF_imgae.gameObject.SetActive(false);
            }
            

            Planes_Script_for_flying.Permission_TO_Control = true;
            fuel_spill_value = true;

            Fuel_Script_for_Sound.Plane_Engine_tune.volume = 1.0f;
            Planes_Script_for_flying.rotationSpeed = 20f;
            Planes_Script_for_flying.tiltRotate_speed = 10f;
            Planes_Script_for_flying.moveSpeed = 15f;
            Planes_Script_for_flying.sideSpeed = 3f;
            /*Planes_Script_for_flying.translationInput ;*/
            /* Planes_Script_for_flying.mouseInput = 0;*/
            fall_zero = false;
            propeller_value = 60;
            Engine_ON = true;

        }
       
    }

    public void Replacing_E_ForButton()
    {
        if (Engine_ON == true)
        {
            on_off_value = 0;
            if (on_off_value == 0)
            {
                Start_ON_imgae.gameObject.SetActive(false);
                Start_OFF_imgae.gameObject.SetActive(true);
            }

            
                Fuel_Script_for_Sound.Plane_Engine_tune.volume = 0.0f;
                Planes_Script_for_flying.rotationSpeed = 0f;
                Planes_Script_for_flying.tiltRotate_speed = 0f;
                Planes_Script_for_flying.moveSpeed = 0f;
                Planes_Script_for_flying.sideSpeed = 0f;
                fall_zero = true;
              //  StartCoroutine(falling());
                Engine_ON = false;
                propeller_value = 0;







            
        }

     }


    public void Replacing_R_ForButton()
    {
        if (Engine_ON == true)
        {

            Fuel_Script_for_Sound.Plane_Engine_tune.volume = 0.6f;
            Planes_Script_for_flying.rotationSpeed = 50f;
            Planes_Script_for_flying.tiltRotate_speed = 25f;
            Planes_Script_for_flying.moveSpeed = 5f;
            Planes_Script_for_flying.sideSpeed = 2f;
            fall = true;
            propeller_value = 20;




        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Planes_Script_for_flying.isPlanecrashed = true;
        }
    }
}
