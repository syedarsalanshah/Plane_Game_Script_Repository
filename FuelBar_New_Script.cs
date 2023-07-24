using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar_New_Script : MonoBehaviour
{
    public Slider New_Fuel_Slider_OB;


    
    public bool Checking = true;
    public float test_value = 33;
    public float calling_time;



    public GameObject Properller_Object;
    private propeller_rotation propeller_script;
    // Start is called before the first frame update
    void Start()
    {
        calling_time = Time.time;
        propeller_script = Properller_Object.GetComponent<propeller_rotation>();
    }

    // Update is called once per frame
    void Update()
    {
        print("propeller " + propeller_script.fuel_spill_value);

        if (propeller_script.fuel_spill_value == true)
        {
            value_to_start();
        }
    }
    void value_to_start()
    {


        InvokeRepeating("inc", 1.0f, 1.0f);
        InvokeRepeating("dec", 1.0f, 2.0f);



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
        Checking = false;
    }
    void dec()
    {
        if (!(New_Fuel_Slider_OB.value <= 0))
        {


            if (Checking == false)
            {
                /* slider_image.rectTransform.localScale = new Vector3(0.9f, 1f, 1f);*/
                // print(slider_val.gameObject.transform.localScale.x);    
                New_Fuel_Slider_OB.value -= 0.1f;


                if (New_Fuel_Slider_OB.value <= 0)
                {
                    New_Fuel_Slider_OB.value = 0;

                }
            }
        }


    }

}
