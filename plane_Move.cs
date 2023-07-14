using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System.Numerics;
using Quaternion = UnityEngine.Quaternion;

public class plane_Move : MonoBehaviour
{

    public bool Permission_TO_Control =false;
    private bool testing = true;

    public FixedJoystick Joystick;

    public AudioSource CoinCollect_Sound;

    public bool fuel_value = false;
    public Rigidbody plane_RB;
    private bool fall;
    public ParticleSystem smoke_Particle;
    public ParticleSystem Dust_boom;
    public ParticleSystem Spark1;
    public ParticleSystem Spark2;
    public ParticleSystem Fire;
    public ParticleSystem F1;
    public ParticleSystem F2;
    public ParticleSystem F3;
    private int Fire_counter = 0;
    public bool indicating_propeller_tocollisionwithpillars = false;


    public Material White_to_gray;
    public float rotationSpeed/* = 20f*/;
    public float tiltRotate_speed/* = 50f*/;
    public float moveSpeed/* = 1f*/;
    public float sideSpeed/* = 3f*/;
    public float translationInput;
    public float mouseInput;


    public bool isPlanecrashed = false;

    public float boost= 0.01f;
    public GameObject Consumption;
    private Fuel_Consumption Fuel_taking;

    public int fuel_counter = 1;
    private int Plane_Blast_Couter = 0;

    //Scoring Section
    public int current_Score = 0;
    public Text Current_Score_text;
    public int current_Total_Score = 0;
    public int Best_Total_Score;
    private int gemCount;
    private int gem_Count_repeat_timer = 0;

   
    public GameObject Screen_UI_Object;
    private Screen_UI_Script Main_Screen_UI_Script;
    public GameObject Propeller_Gameobject;
    private propeller_rotation Propeller_Script;
    public GameObject Score_gameobject;
    private ScorePrototype Score_Script;
    private Checking_Script Plane_Fuel_Slider;


    //checking of gyro values
    [SerializeField] private Text g1_text;
    [SerializeField] private Text g2_text;
    /* [SerializeField] private Text g3_text;
     [SerializeField] private Text g4_text;*/
    private int inv_rotating;
    private void Awake()
    {
       transform.localRotation = Quaternion.Euler(0, 90, 0);
    }

    private void Start()
    {


        Input.gyro.enabled = true;

        if(Input.gyro.enabled == true)
        {
            g1_text.text = "true";
        }
        else
        {
            g1_text.text = "false";
        }

        Fuel_taking = Consumption.GetComponent<Fuel_Consumption>();
        Plane_Fuel_Slider = Consumption.GetComponent<Checking_Script>();
        Score_Script = Score_gameobject.GetComponent<ScorePrototype>();
        Propeller_Script = Propeller_Gameobject.GetComponent<propeller_rotation>();
        Main_Screen_UI_Script = Screen_UI_Object.GetComponent<Screen_UI_Script>();

        /* plane_RB = GetComponent<Rigidbody>();*/
       
       // transform.Rotate(0, 90, 0);
        smoke_Particle.Stop();
        White_to_gray.SetFloat("_Metallic", 0f);
        
        gemCount = GameObject.FindGameObjectsWithTag("Gems").Length;
        Debug.Log("Number of gems: " + gemCount);

        Best_Total_Score = PlayerPrefs.GetInt("HighestScore", 0);

        indicating_propeller_tocollisionwithpillars = false;
        fuel_value = false;
    }
    void Update()
    {




        if (fall == true)
        {
            transform.Translate(UnityEngine.Vector3.down * 5 * Time.deltaTime);
        }

        gemCount = GameObject.FindGameObjectsWithTag("Gems").Length;

        if (Time.time < 0.5f)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
           
        }
        translationInput = Input.GetAxis("Vertical");

        /*---------------------if (Input.GetKey(KeyCode.LeftArrow))
        {
            *//* transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
             transform.Rotate(Vector3.forward, tiltSpeed * Time.deltaTime);*//*
            transform.Translate(Vector3.forward * sideSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(-Vector3.forward * sideSpeed * Time.deltaTime);
        }*/


        /* ----------------------------if (Input.GetKey(KeyCode.LeftArrow))
         {
             *//* transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
              transform.Rotate(Vector3.forward, tiltSpeed * Time.deltaTime);*//*
             transform.Rotate(Vector3.right * tiltRotate_speed * Time.deltaTime);
         }
         else if (Input.GetKey(KeyCode.RightArrow))
         {
             transform.Rotate(-Vector3.right * tiltRotate_speed * Time.deltaTime);
         }
 */
        /*        if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                }*/
        /*else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(-(Vector3.forward), rotationSpeed * Time.deltaTime);
        }*/
        /* if (Input.GetKey(KeyCode.W) && Fuel_taking.barr.gameObject.transform.localScale.x >= 0.50)
         {
             *//* transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
              transform.Rotate(Vector3.forward, tiltSpeed * Time.deltaTime);*//*
             transform.Translate(Vector3.right * boost * Time.deltaTime);
         }*/




        /*  mouseInput = Input.GetAxis("Mouse X");*/
        /* mouseInput = Input.gyro.attitude.x * Time.deltaTime;
        // transform.Rotate(-Vector3.right, mouseInput * rotationSpeed * Time.deltaTime);
         transform.rotation = Quaternion.Euler(mouseInput, 90, 0);*/

        /* transform.Translate(Vector3.forward * translationInput * moveSpeed * Time.deltaTime);
         transform.Translate(Vector3.right * sideSpeed * Time.deltaTime);*/


        if (Permission_TO_Control == true)
        {
        
            float move_X = Joystick.Horizontal * 15 * Time.deltaTime;
           
            transform.Translate(0, 0, -move_X);
        }

        transform.Translate(UnityEngine.Vector3.right * moveSpeed * Time.deltaTime);


        if(Fuel_taking.value_of_fuel_zero == 0)
        {
           // print("yes");
           // transform.Translate(Vector3.down *10* Time.deltaTime);
        }

        if (gemCount == 0)
        {        
            if (gem_Count_repeat_timer == 0)
            {
                current_Total_Score = current_Score;
                print("Total score is " + current_Total_Score);
                gem_Count_repeat_timer = 1;
             


            }
        }


        if(Best_Total_Score < current_Score)
        {
            Best_Total_Score = current_Score;
            print("Total Best score is " + Best_Total_Score);
            PlayerPrefs.SetInt("HighestScore", Best_Total_Score);
        }

        float xval = Input.gyro.rotationRate.x;
        

      
    
        if (Permission_TO_Control == true)
        {
            // float gyroInput = Input.gyro.rotationRate.x * Time.deltaTime;
            /* float gyroInput_y = Input.gyro.rotationRate.y * Time.deltaTime;*/
            float gyroInput_z = Input.gyro.rotationRate.z * Time.deltaTime;

            // Calculate the amount of rotation.
            //    float rotating = gyroInput * 40;
            /* float rotating_y = gyroInput_y * 40;*/
            float rotating_z = gyroInput_z * 60;

            // Rotate the plane.
            //   transform.Rotate(rotating, 0,0);
            // transform.Rotate(rotating_y, 0, 0);
           transform.Rotate(rotating_z, 0,0);

            g2_text.text = rotating_z.ToString();
            print("values: "+inv_rotating);

        }
    }

    private void FixedUpdate()
    {
        if(Permission_TO_Control == true)
        {
            float rotation_Z = Joystick.Vertical * 50 * Time.deltaTime;
            //float rotation_X = Joystick.Horizontal * 25 * Time.deltaTime;
/*            float move_X = Joystick.Horizontal * 17 * Time.deltaTime;*/
            transform.Rotate(0, 0, rotation_Z);

           /* transform.Translate(0, 0, -move_X);*/
        }
        /*transform.Rotate(Joystick.Horizontal * Time.deltaTime * 50, transform.rotation.y , Joystick.Vertical * Time.deltaTime * 20);*/









    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel"))
        {
            /* Fuel_taking.bool_of_fuel_to_Allow = true;
             */
            /*StartCoroutine(calling_decrease());*/
            Plane_Fuel_Slider.Checking = true;
            Destroy(other.gameObject);
            fuel_counter = 2;
            StartCoroutine(fuel_counter_timing());


        }
       

        if (other.gameObject.name.Contains("5 Side Diamond"))
        {
            Destroy(other.gameObject);
            current_Score += 50;
            Score_Script.Diamond_fun(1);
           
            
        }

        if (other.gameObject.name.Contains("Diamondo"))
        {
            Destroy(other.gameObject);
            current_Score += 30;
            Score_Script.Emberled_fun(1);

        }

        if (other.gameObject.name.Contains("SoftStar"))
        {
            Destroy(other.gameObject);
            current_Score += 10;
            Score_Script.Start_fun(1);

        }

        if (other.gameObject.CompareTag("Gems"))
        {
            CoinCollect_Sound.Play();
        }


        Current_Score_text.text = current_Score.ToString();

    }
    IEnumerator fuel_counter_timing()
    {
        yield return new WaitForSecondsRealtime(1);
        fuel_counter = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            indicating_propeller_tocollisionwithpillars = true;
            smoke_Particle.Play();
            Dust_boom.Play();
            Spark1.Play();
            Spark2.Play();
            fall = true;

        }

        if (collision.gameObject.CompareTag("Plane"))
        {
            White_to_gray.SetFloat("_Metallic", 1f);
            Fire.Play();
            if (Fire_counter == 0)
            {
                F1.Play();
                F2.Play();
                F3.Play();
                Fire_counter = 1;

            }

            moveSpeed = 0f;
            boost = 0f;
            rotationSpeed = 0f;
            mouseInput = 0f;
            sideSpeed = 0f;
            tiltRotate_speed = 0f;
            isPlanecrashed = true;
            if (Plane_Blast_Couter == 0)
            {
                Fuel_taking.Plane_Blast.Play();
                Fuel_taking.Plane_Engine_tune.Stop();
                Plane_Blast_Couter = 1;
            }

            StartCoroutine(CallingLoserFunction());

        }

        if (collision.gameObject.CompareTag("savor"))
        {
            print("gamefinished");
            Propeller_Script.fall_zero = false;
            rotationSpeed = 0f;
            tiltRotate_speed = 0f;
            moveSpeed = 0f;
            sideSpeed = 0f;
            /*  Planes_Script_for_flying.translationInput = 0;*/
            /* Planes_Script_for_flying.mouseInput = 0;*/
            Propeller_Script.propeller_value = 0;

        }

        if (collision.gameObject.CompareTag("savor") && current_Score >= 100)
        {
            Main_Screen_UI_Script.Winner_Menu_Function();
        }

        if (collision.gameObject.CompareTag("savor") && current_Score < 100)
        {
            Main_Screen_UI_Script.Loser_Menu_Function();
        }

        
    }
    IEnumerator CallingLoserFunction()
    {
        yield return new WaitForSecondsRealtime(2f);
        Main_Screen_UI_Script.Loser_Menu_Function();
    }



 /*   IEnumerator calling_decrease()
    {
        yield return new WaitForSecondsRealtime(1f);
        Fuel_taking.bool_of_fuel_to_Allow = false;
        Fuel_taking.bar_decrease_sequence();
       
    }*/


    IEnumerator stop_rotation()
    {
        yield return new WaitForSecondsRealtime(1f);
    }


    // Section for W button
    public void Down()
    {
        InvokeRepeating("Replacing_W_ForButton", 0, 0.001f);
    }
    public void Replacing_W_ForButton()
    {
        if (Fuel_taking.barr.gameObject.transform.localScale.x >= 0.50)
        {
            /* transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
             transform.Rotate(Vector3.forward, tiltSpeed * Time.deltaTime);*/
            transform.Translate(UnityEngine.Vector3.right * boost * Time.deltaTime);
        }
    }
    public void Up()
    {
        CancelInvoke("Replacing_W_ForButton");
    }


    //Movement Area
    // Up Button Section

    public void Down_UPArrow()
    {
        InvokeRepeating("Replacing_UP_Arrow", 0, 0.001f);
    }
    public void Replacing_UP_Arrow()
    {
        transform.Rotate(UnityEngine.Vector3.forward * rotationSpeed * Time.deltaTime);
    }
    public void Up_UPArrow()
    {
        CancelInvoke("Replacing_UP_Arrow");
    }


    // Down Button Section

    public void Down_DownArrow()
    {
        
        InvokeRepeating("Replacing_Down_Arrow", 0, 0.001f);
    }
    public void Replacing_Down_Arrow()
    {
        transform.Rotate(-(UnityEngine.Vector3.forward), rotationSpeed * Time.deltaTime);
    }

    public void Up_DownArrow()
    {
        CancelInvoke("Replacing_Down_Arrow");
    }

    // Left Button Section

    public void Down_LeftArrow()
    {
        InvokeRepeating("Replacing_Left_Arrow", 0, 0.001f);
       /* InvokeRepeating("Rotate_Replacing_Left_Arrow", 0, 0.002f);*/
    }
    public void Replacing_Left_Arrow()
        
    {
        transform.Translate(-UnityEngine.Vector3.forward * sideSpeed * Time.deltaTime);

        if (testing == true)
        {
            transform.Rotate(-UnityEngine.Vector3.right * tiltRotate_speed * Time.deltaTime);
        }

        // transform.Rotate(-Vector3.right * tiltRotate_speed * Time.deltaTime);
    }

  /*  public void Roate_Replacing_Left_Arrow()
    {
        //transform.Translate(-Vector3.forward * sideSpeed * Time.deltaTime);
       

        // transform.Rotate(-Vector3.right * tiltRotate_speed * Time.deltaTime);
    }*/


    public void Up_LeftArrow()
    {
        CancelInvoke("Replacing_Left_Arrow");
/*        CancelInvoke("Rotate_Replacing_Left_Arrow");*/
    }

    // Right Button Section

    public void Down_RightArrow()
    {
        
        InvokeRepeating("Replacing_Right_Arrow", 0, 0.001f);
       /* InvokeRepeating("RotateReplacing_Right_Arrow", 0, 0.002f);*/
    }
    public void Replacing_Right_Arrow()
    {
        transform.Translate(UnityEngine.Vector3.forward * sideSpeed * Time.deltaTime);

        if (testing == true)
        {
           /* transform.Rotate(Vector3.right * tiltRotate_speed * Time.deltaTime);*/
            /*  transform.rotation += Quaternion.Euler(Vector3.right * tiltRotate_speed * Time.deltaTime);*/
            Quaternion tiltRotation = Quaternion.Euler(UnityEngine.Vector3.right * tiltRotate_speed * Time.deltaTime);

            // Rotate the transform by the tilt rotation.
            transform.rotation *= tiltRotation;
        }

        // transform.Rotate(Vector3.right * tiltRotate_speed * Time.deltaTime);
    }

  /*  public void Rotate_Replacing_Right_Arrow()
    {
        //transform.Translate(Vector3.forward * sideSpeed * Time.deltaTime);

       

        // transform.Rotate(Vector3.right * tiltRotate_speed * Time.deltaTime);
    }*/
    public void Up_RightArrow()
    {
        CancelInvoke("Replacing_Right_Arrow");
      /*  CancelInvoke("Rotate_Replacing_Right_Arrow");*/
    }
}
