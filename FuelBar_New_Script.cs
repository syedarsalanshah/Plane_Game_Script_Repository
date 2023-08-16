using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar_New_Script : MonoBehaviour
{
    public Slider New_Fuel_Slider_OB;

    
    
    private bool Checking = false;
    public float test_value = 33;
    public float calling_time;
    public bool Fuel_Collision_Checking = false;

  //  public GameObject Plane_GO;
    private plane_Move Plane_Script;

    public GameObject Properller_Object;
    private propeller_rotation propeller_script;
    // Start is called before the first frame update
    void Start()
    {
        
        calling_time = Time.time;
        propeller_script = Properller_Object.GetComponent<propeller_rotation>();
        InvokeRepeating("dec", 0, 0.5f);
        Plane_Script = GameObject.Find("biplane_main").GetComponent<plane_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(New_Fuel_Slider_OB.value == 0)
        {
            New_Fuel_Slider_OB.fillRect.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
        }

      //  print("Checking Collision value: "+Fuel_Collision_Checking);
        //print("propeller " + propeller_script.fuel_spill_value);

        if (propeller_script.fuel_spill_value == true)
        {
           // print("You were right.");
            value_to_start();
        }

        if(Fuel_Collision_Checking == true)
        {
            New_Fuel_Slider_OB.value = 10;
            StartCoroutine(Finish_Fuel_Ful_Timer());
        }


        if(Plane_Script.isPlanecrashed == true)
        {
            CancelInvoke("dec");
        }

    }
    void value_to_start()
    {

      //  print("I am in value_to_start");
       // InvokeRepeating("inc", 0f, 1.0f);
        


        
    }

    public void inc()
    {
        if (Checking == true)
        {
            // slider_val.LeanScaleX(1, 1);
            New_Fuel_Slider_OB.value = 10;
            StartCoroutine(Finish_Fuel_Ful_Timer());

        }
    }
    IEnumerator Finish_Fuel_Ful_Timer()
    {
        yield return new WaitForSeconds(1f);
        Fuel_Collision_Checking = false;
    }
    void dec()
    {
        if (propeller_script.Engine_ON == true)
        {

            if (!(New_Fuel_Slider_OB.value <= 0))
            {
                //  print("Iam in decreasing");

                if (Fuel_Collision_Checking == false)
                {
                    /* slider_image.rectTransform.localScale = new Vector3(0.9f, 1f, 1f);*/
                    // print(slider_val.gameObject.transform.localScale.x);    
                    New_Fuel_Slider_OB.value -= 0.10f;


                    if (New_Fuel_Slider_OB.value <= 0)
                    {
                        New_Fuel_Slider_OB.value = 0;

                    }
                }

            }
        }


    }

}
