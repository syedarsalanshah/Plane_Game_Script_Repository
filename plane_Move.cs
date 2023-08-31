using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System.Numerics;
using Quaternion = UnityEngine.Quaternion;
using JetBrains.Annotations;
using Slider = UnityEngine.UI.Slider;
using Vector3 = UnityEngine.Vector3;

public class plane_Move : MonoBehaviour
{

    
    private Sending_Game_Data_Script Specific_UserData_Script;

    public GameObject Check_Network_GO;
    private Network_Script Networking_First_Script;
    public GameObject Registration_bar;
    public GameObject NotifyBar;
    private Quaternion initial_rotation;
    private float move_X;
    private bool Stop_values_Bool = false;
    private float gyroInput_z;
    private float gyroInput_x;
    public GameObject Main_Camera;
    public GameObject Secondary_Camera;
    public GameObject Empty_Secondary_GO;

    [SerializeField] private int rotate;
    private float gyro_Z_sensitivity = 60;
    public bool Permission_TO_Control =false;
    private bool testing = true;

    public bool Allow_to_Rotate;

    private UnityEngine.Vector3 Previous_Position;

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


    public GameObject NewFuel_GO;
    private FuelBar_New_Script New_Fuel_Script;

    public Slider Slider_NF;
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

        Specific_UserData_Script = Check_Network_GO.GetComponent<Sending_Game_Data_Script>();

        Networking_First_Script = Check_Network_GO.GetComponent<Network_Script>();
        Registration_bar.SetActive(false);
        NotifyBar.SetActive(false);
        initial_rotation = transform.rotation;
       // InvokeRepeating("function_for_correction_rotation", 0, 5);

        New_Fuel_Script = GameObject.Find("Fuel_Bar_New").GetComponent<FuelBar_New_Script>();

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

        InvokeRepeating("Check_Position", 1, 1.0f);
    }

    void function_for_correction_rotation()
    {
        if(Propeller_Script.Engine_ON == true && Joystick.Horizontal == 0)
        {
            transform.Rotate(-transform.rotation.x+10, 0, 0);
            
          //  transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
           
        }
    }

    private void LateUpdate()
    {
     


    }
    void Update()
    {
        if(transform.position.y>= 55 && New_Fuel_Script.New_Fuel_Slider_OB.value == 0)
        {
            transform.Rotate(10 * Time.deltaTime, 0, 0);

        }
       



        if (fall == true)
        {
            transform.Translate(UnityEngine.Vector3.down * 5 * Time.deltaTime);
        }

        gemCount = GameObject.FindGameObjectsWithTag("Gems").Length;

        if (Time.time < 0.5f)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
           
        }
        // translationInput = Input.GetAxis("Vertical");

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


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
           transform.Translate(Vector3.forward * 10 * Time.deltaTime);
            transform.Rotate(Vector3.right * tiltRotate_speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.right * tiltRotate_speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.forward, 70 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(-(Vector3.forward), 70 * Time.deltaTime);
        }
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
        //  print("Joystick Horizontal"+Joystick.Horizontal);

        if (Permission_TO_Control == true)
        {

            if(Joystick.Horizontal >= -0.4  || Joystick.Horizontal <= 0.4)
            {
                move_X = Joystick.Horizontal * 15 * Time.deltaTime;

                transform.Translate(0, 0, -move_X);
                
            }
            else if(Joystick.Horizontal >= 0.5 && Joystick.Horizontal <= 0.75 || Joystick.Horizontal <= 0.5 && Joystick.Horizontal >= 0.75)
            {
                move_X = Joystick.Horizontal * 30 * Time.deltaTime;

                transform.Translate(0, 0, -move_X);

            }
            else
            {
                move_X = Joystick.Horizontal * 45 * Time.deltaTime;

                transform.Translate(0, 0, -move_X);
                

             
            }
        
           
        }
       
        if(Joystick.Vertical >= 0.4 && /*Fuel_taking.barr.gameObject.transform.localScale.x >= 0.5f*/ Slider_NF.value >= 5f && Propeller_Script.Engine_ON == true && Stop_values_Bool == false)
        {
            transform.Translate(UnityEngine.Vector3.right * 30 * Time.deltaTime);
        }
        else if(Joystick.Vertical <= -0.4 && Propeller_Script.Engine_ON == true && Stop_values_Bool == false)
        {
            transform.Translate(UnityEngine.Vector3.right * 8 * Time.deltaTime);
        }
        else
        {
            transform.Translate(UnityEngine.Vector3.right * moveSpeed * Time.deltaTime);
        }

        


        if (Fuel_taking.value_of_fuel_zero == 0)
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
        

      
    
        if (Permission_TO_Control == true && Stop_values_Bool == false)
        {
            // float gyroInput = Input.gyro.rotationRate.x * Time.deltaTime;
            /* float gyroInput_y = Input.gyro.rotationRate.y * Time.deltaTime;*/
            gyroInput_z = Input.gyro.rotationRate.z * Time.deltaTime;

            // Calculate the amount of rotation.
            //    float rotating = gyroInput * 40;
            /* float rotating_y = gyroInput_y * 40;*/
            float rotating_z = gyroInput_z * gyro_Z_sensitivity;

            // Rotate the plane.
            //   transform.Rotate(rotating, 0,0);
            // transform.Rotate(rotating_y, 0, 0);
           transform.Rotate(rotating_z, 0,0);

            g2_text.text = rotating_z.ToString();
          //  print("values: "+inv_rotating);

        }
    }

    

    private void FixedUpdate()
    {

        if(Stop_values_Bool == true)
        {
            gyroInput_x = 0;
            gyroInput_z = 0;
            move_X = 0;



        }
       
       
        if (Permission_TO_Control == true && Stop_values_Bool == false)
        {
            // float gyroInput = Input.gyro.rotationRate.x * Time.deltaTime;
            /* float gyroInput_y = Input.gyro.rotationRate.y * Time.deltaTime;*/
            gyroInput_x = Input.gyro.rotationRate.x * Time.deltaTime;

            // Calculate the amount of rotation.
            //    float rotating = gyroInput * 40;
            /* float rotating_y = gyroInput_y * 40;*/
            float rotating_x = gyroInput_x * 75;

            // Rotate the plane.
            //   transform.Rotate(rotating, 0,0);
            // transform.Rotate(rotating_y, 0, 0);
            transform.Rotate(0, 0, rotating_x);
        }

            if (Permission_TO_Control == true)
        {
            //float rotation_Z = Joystick.Vertical * gyro_Z_sensitivity * Time.deltaTime;
            //float rotation_X = Joystick.Horizontal * 25 * Time.deltaTime;
/*            float move_X = Joystick.Horizontal * 17 * Time.deltaTime;*/
          //  transform.Rotate(0, 0, rotation_Z);

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

            New_Fuel_Script.Fuel_Collision_Checking = true;
             print("Yes this fuel taken in trigger");
          //   Destroy(other.gameObject);
             fuel_counter = 2;


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
            Stop_values_Bool = true;
            print("gamefinished");
            Main_Camera.SetActive(false);
            Empty_Secondary_GO.SetActive(true);
            Secondary_Camera.SetActive(true);
            Propeller_Script.fall_zero = false;
            rotationSpeed = 0f;
            tiltRotate_speed = 0f;
            moveSpeed = 0f;
            sideSpeed = 0f;
            transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
            transform.position = new UnityEngine.Vector3(transform.position.x, 21.75f, transform.position.z);
          
            /*  Planes_Script_for_flying.translationInput = 0;*/
            /* Planes_Script_for_flying.mouseInput = 0;*/
            Propeller_Script.propeller_value = 0;
            StartCoroutine(ShowingResult_GUI());
           

            if (Network_Script.Caretaker_of_Player != "No" && Network_Script.Caretaker_of_Player.Length != 0)
            {
                Specific_UserData_Script.CalltoSendDataFun();

            }

        }

      

        
    }
    IEnumerator ShowingResult_GUI()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        if (current_Score >= 100)
        {
            if(Network_Script.Caretaker_of_Player == "No" || Network_Script.Caretaker_of_Player.Length == 0)
            {
                NotifyBar.SetActive(true);
            }
            
            Main_Screen_UI_Script.Winner_Menu_Function();
        }

        if (current_Score < 100)
        {
            Main_Screen_UI_Script.Loser_Menu_Function();
        }

    }
    IEnumerator CallingLoserFunction()
    {
        yield return new WaitForSecondsRealtime(2f);
        Main_Screen_UI_Script.Loser_Menu_Function();
    }
    public void CloseNotifybar()
    {
        NotifyBar.SetActive(false);
    }
    public void OpeningRegistrationbar()
    {
        Registration_bar.SetActive(true);
        NotifyBar.SetActive(false);
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
        if (Slider_NF.value >= 0.50 && Propeller_Script.Engine_ON == true)
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


    void Check_Position()
    {
        Previous_Position = transform.position;
        StartCoroutine(Check_Position_Timer());

    }
    IEnumerator Check_Position_Timer()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        if(Previous_Position == transform.position && Propeller_Script.Engine_ON == true)
        {
            Main_Screen_UI_Script.Loser_Menu.gameObject.SetActive(true);
        }
    }


    public void Adjust_Plane_Position_Fun()
    {
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        ;
    }
}
