using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class plane_Move : MonoBehaviour
{
    private Rigidbody plane_RB;
    private bool fall;
    public ParticleSystem smoke_Particle;
    public ParticleSystem Dust_boom;
    public ParticleSystem Spark1;
    public ParticleSystem Spark2;
    public ParticleSystem Fire;
    public ParticleSystem F1;
    public ParticleSystem F2;
    public ParticleSystem F3;


    public Material White_to_gray;
    public float rotationSpeed = 100f;
    public float tiltRotate_speed = 50f;
    public float moveSpeed = 1f;
    public float sideSpeed = 10f;
    public float translationInput;
    public float mouseInput;


    public bool isPlanecrashed = false;

    public float boost = 40f;
    public GameObject Consumption;
    private Fuel_Consumption Fuel_taking;

    public int fuel_counter = 1;
    private int Plane_Blast_Couter = 0;

    //Scoring Section
    public int current_Score = 0;
    public int current_Total_Score = 0;
    public int Best_Total_Score;
    private int gemCount;
    private int gem_Count_repeat_timer = 0;

    public GameObject Score_gameobject;
    private ScorePrototype Score_Script;
    private Checking_Script Plane_Fuel_Slider;
    private void Start()
    {
        Fuel_taking = Consumption.GetComponent<Fuel_Consumption>();
        Plane_Fuel_Slider = Consumption.GetComponent<Checking_Script>();
        Score_Script = Score_gameobject.GetComponent<ScorePrototype>();
        
        plane_RB = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        smoke_Particle.Stop();
        White_to_gray.SetFloat("_Metallic", 0f);

        gemCount = GameObject.FindGameObjectsWithTag("Gems").Length;
        Debug.Log("Number of gems: " + gemCount);

        Best_Total_Score = PlayerPrefs.GetInt("HighestScore", 0);
    }
    void Update()
    {

        if(fall == true)
        {
            transform.Translate(Vector3.down * 5 * Time.deltaTime);
        }

        gemCount = GameObject.FindGameObjectsWithTag("Gems").Length;
        if (Time.time < 0.5f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        translationInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            /* transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
             transform.Rotate(Vector3.forward, tiltSpeed * Time.deltaTime);*/
            transform.Translate(Vector3.forward * sideSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(-Vector3.forward * sideSpeed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            /* transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
             transform.Rotate(Vector3.forward, tiltSpeed * Time.deltaTime);*/
            transform.Rotate(Vector3.right * tiltRotate_speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.right * tiltRotate_speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(-(Vector3.forward), rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            /* transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
             transform.Rotate(Vector3.forward, tiltSpeed * Time.deltaTime);*/
            transform.Translate(Vector3.right * boost * Time.deltaTime);
        }


        mouseInput = Input.GetAxis("Mouse X");
        transform.Rotate(-Vector3.right, mouseInput * rotationSpeed * Time.deltaTime);

       /* transform.Translate(Vector3.forward * translationInput * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * sideSpeed * Time.deltaTime);*/
       
       transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);


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


        if(Best_Total_Score < current_Total_Score)
        {
            Best_Total_Score = current_Total_Score;
            print("Total Best score is " + Best_Total_Score);
            PlayerPrefs.SetInt("HighestScore", Best_Total_Score);
        }
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
            current_Score += 5;
            Score_Script.Diamond_fun(1);
           
            
        }

        if (other.gameObject.name.Contains("Diamondo"))
        {
            Destroy(other.gameObject);
            current_Score += 3;
            Score_Script.Emberled_fun(1);

        }

        if (other.gameObject.name.Contains("SoftStar"))
        {
            Destroy(other.gameObject);
            current_Score += 1;
            Score_Script.Start_fun(1);

        }

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
            F1.Play();
            F2.Play();
            F3.Play();
            moveSpeed = 0f;
            boost = 0f;
            rotationSpeed = 0f;
            mouseInput = 0f;
            sideSpeed = 0f;
            tiltRotate_speed = 0f;
            isPlanecrashed = true;
            if(Plane_Blast_Couter == 0)
            {
                Fuel_taking.Plane_Blast.Play();
                Fuel_taking.Plane_Engine_tune.Stop();
                Plane_Blast_Couter = 1;
            }
           
            
        }


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
}
