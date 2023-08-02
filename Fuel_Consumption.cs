using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Fuel_Consumption : MonoBehaviour
{

    private bool Fuel_Indicator_BOOL;
    public int Timer;
  
    public int val;
    public int value_of_fuel_zero = 1;

    public bool bool_of_fuel_to_Allow = false;
   


    public Slider New_Fuel_Slider_GO;

    public GameObject Plane_as_Object;
    private plane_Move Planes_Script;


    public GameObject Fuel_Icon_as_Object;
    public GameObject Fuel_Background_as_Object;


    public AudioSource fuel_alaram_tune;
    public AudioSource Crash_tune;
    public AudioSource Petrol_Filling_tune;
    public AudioSource Plane_Engine_tune;
    public AudioSource Plane_Blast;

    public int Plane_value = 0;
    private int counter_for_fuel_alaram = 1;
    public float time_timer;


    private int decline_of_plane_counter = 1;

    public GameObject Plane_partsScript_object;
    private Plane_Parts_Rigidbody PlanePart_RB_Script;
    public GameObject Propeller_Object;
    private propeller_rotation Propeller_Script;

    public float Clock_for_fuel;
    // Start is called before the first frame update
    void Start()
    {
      //  AnimateBar();
        Planes_Script = Plane_as_Object.GetComponent<plane_Move>();
        Propeller_Script = Propeller_Object.GetComponent<propeller_rotation>();
        /*bool_of_fuel_to_Allow=false;
        bar_decrease_sequence();
        Clock_for_fuel = 1;
        time_timer = 5.0f;*/
        /*    InvokeRepeating("bar_increase_sequence", 1.0f, 1.0f);


            InvokeRepeating("bar_decrease_sequence", 1.0f, 3.0f);*/


        PlanePart_RB_Script = Plane_partsScript_object.GetComponent<Plane_Parts_Rigidbody>();

        InvokeRepeating("Fuel_ON", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
      
        

        if (/*barr.gameObject.transform.localScale.x*/New_Fuel_Slider_GO.value <= 4f && /*barr.gameObject.transform.localScale.x*/New_Fuel_Slider_GO.value >= 0.01f)
        {
          
            if (counter_for_fuel_alaram == 1)
             {
                
                 fuel_alaram_tune.Play();
                counter_for_fuel_alaram = 0;
                /* StartCoroutine(Fuel_Warning_Image_Timer());*/
              

               
            }
        }
        else
        {
            fuel_alaram_tune.Stop();
            counter_for_fuel_alaram = 1;
            
        }


        if ((New_Fuel_Slider_GO.value <= 1.5f) && (New_Fuel_Slider_GO.value >= 0.001f))
        {
            if (decline_of_plane_counter == 1)
            {

                Crash_tune.Play();
                StartCoroutine(stopTimer());
                
               

                decline_of_plane_counter = 0;
               
               
            }


            Planes_Script.gameObject.transform.Rotate(-(Vector3.forward) * 12  * Time.deltaTime);

            

        }
        


    }


    void Fuel_ON()
    {
        if ( New_Fuel_Slider_GO.value  > 0 && Fuel_Indicator_BOOL && New_Fuel_Slider_GO.value <= 5f || New_Fuel_Slider_GO.value == 0.01f)
        {
            Fuel_Background_as_Object.gameObject.SetActive(true);
            Fuel_Icon_as_Object.gameObject.SetActive(true);
            Fuel_Indicator_BOOL = false;
           
        }
        else if(!Fuel_Indicator_BOOL)
        {
            Fuel_Background_as_Object.gameObject.SetActive(false);
            Fuel_Icon_as_Object.gameObject.SetActive(false);
            Fuel_Indicator_BOOL = true;

        }


    }

    void Fuel_OFF()
    {
      
       
    }
    IEnumerator Fuel_Warning_Image_Timer()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Fuel_Icon_as_Object.gameObject.SetActive(true);
        Fuel_Background_as_Object.gameObject.SetActive(true);
        StartCoroutine(next());
       /* yield return new WaitForSecondsRealtime(1.0f);
        Fuel_Indicator_BOOL = true;
        Fuel_Background_as_Object.gameObject.SetActive(false);
       Fuel_Icon_as_Object.gameObject.SetActive(false);*/
     /*   yield return new WaitForSecondsRealtime(0.2f);
        Fuel_Icon_as_Object.gameObject.SetActive(false);
        Fuel_Background_as_Object.gameObject.SetActive(false);*/

    }
    IEnumerator next()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Fuel_Indicator_BOOL = true;
        Fuel_Background_as_Object.gameObject.SetActive(false);
        Fuel_Icon_as_Object.gameObject.SetActive(false);

    }
    void FixedUpdate()
    {
        if(Time.time == time_timer)
        {
          
            time_timer = Time.time+5.0f;
           // print(time_timer);
        }

        if(New_Fuel_Slider_GO.value <= 0.0)
          {
            Planes_Script.rotationSpeed = 0f;
            Planes_Script.tiltRotate_speed = 0f;
            Planes_Script.moveSpeed = 0f;
            Planes_Script.sideSpeed = 0f;
            Propeller_Script.propeller_value = 0;

            Planes_Script.plane_RB.useGravity = true;
            Planes_Script.plane_RB.freezeRotation = false;

            Plane_Engine_tune.Stop();
           
        }




        if (bool_of_fuel_to_Allow == false)
        {

            StartCoroutine(timer_for());
        }

        if (Propeller_Script.Engine_ON == true)
        {
            if(Plane_value == 0)
            {
                Plane_Engine_tune.Play();
                Plane_value = 1;
            }
            
        }
    }
    IEnumerator timer_for()
    {
        yield return new WaitForSeconds(5f);
       /* bar_decrease_sequence();*/
        
    }



   /* public void bar_decrease_sequence()
    {

        *//*
              if(bool_of_fuel_to_Allow == true)
                {
                    current_leanScale = 1.0f;
                    barr.gameObject.LeanScaleX(1.0f, 2f);
                    
                }
                else
                {
                    current_leanScale = current_leanScale - 0.1f ;
                    barr.gameObject.LeanScaleX(current_leanScale, 5f);
                }

                if (bool_of_fuel_to_Allow == false)
                {
                    if(barr.rectTransform.localScale.x <= 0.0)
                    {
                        barr.gameObject.LeanScaleX(0.0f, 2f);

                    }



                }
        *//*
        if (!(barr.gameObject.transform.localScale.x <= 0))
        {


            if (bool_of_fuel_to_Allow == false)
            {
                barr.gameObject.LeanScaleX(barr.gameObject.transform.localScale.x - 0.1f, 3);

                if (barr.gameObject.transform.localScale.x <= 0)
                {
                    barr.gameObject.LeanScaleX(0, 1);
                }
            }
        }



    }*/
    IEnumerator Bool_true_convertingto_false()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        bool_of_fuel_to_Allow = true;
    }

   /* public void bar_increase_sequence()
    {
        StartCoroutine(Bool_true_convertingto_false());
        Petrol_Filling_tune.Play();
        StartCoroutine(Sound());
        *//*barr.gameObject.LeanScaleX(1, 1f);*//*
        if ( bool_of_fuel_to_Allow == true)
            
        {
            barr.gameObject.LeanScaleX(1, 1);
        }
    }*/
    IEnumerator Sound()
    {
        yield return new WaitForSeconds(1.0f);
        Petrol_Filling_tune.Stop();
    }


    IEnumerator bar_animate()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        val = 0;
    }

    IEnumerator stopTimer()
    {
        
        
        yield return new WaitForSecondsRealtime(2f);
        Crash_tune.Stop();
        decline_of_plane_counter = 1;
    }

 

}
