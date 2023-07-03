using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using JetBrains.Annotations;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Fuel_Consumption : MonoBehaviour
{
  
    public int Timer;
    public GameObject barr;
    public int val;
    public int value_of_fuel_zero = 1;

    public bool bool_of_fuel_to_Allow = false;
    private float current_leanScale = 1;

    public GameObject Plane_as_Object;
    private plane_Move Planes_Script;


    public AudioSource fuel_alaram_tune;
    public AudioSource Crash_tune;
    public AudioSource Petrol_Filling_tune;
    public AudioSource Plane_Engine_tune;
    public AudioSource Plane_Blast;

    public int Plane_value = 0;
    private int counter_for_fuel_alaram = 1;
    public float time_timer;


    private int decline_of_plane_counter = 1;
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
        InvokeRepeating("bar_increase_sequence", 1.0f, 1.0f);


        InvokeRepeating("bar_decrease_sequence", 1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        


        if (barr.gameObject.transform.localScale.x <= 0.5f && barr.gameObject.transform.localScale.x >= 0.01f)
        {

            if(counter_for_fuel_alaram == 1)
             {
                
                 fuel_alaram_tune.Play();
                counter_for_fuel_alaram = 0;



             }
        }
        else
        {
            fuel_alaram_tune.Stop();
            counter_for_fuel_alaram = 1;
            
        }


        if ((barr.gameObject.transform.localScale.x <= 0.35f) && (barr.gameObject.transform.localScale.x >= 0.001f))
        {
            if (decline_of_plane_counter == 1)
            {

                Crash_tune.Play();
                StartCoroutine(stopTimer());
                
               

                decline_of_plane_counter = 0;
               
               
            }


            Planes_Script.gameObject.transform.Rotate(-(Vector3.forward) * 15  * Time.deltaTime);

        }

        

    }
    void FixedUpdate()
    {
        if(Time.time == time_timer)
        {
          
            time_timer = Time.time+5.0f;
            print(time_timer);
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
