using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    
    public GameObject Plane;
    public float followSpeed = 15f;
    private Vector3 cameraOffset;
    private Vector3 smoothedPosition;
    private int valuetoint;
    // Adjust this value to control the smoothness of camera movement
    // Start is called before the first frame update
    private Vector3 planeDistance;
    private Vector3 desiredPosition;
    private Timer_Script Time_Script;
    public GameObject Canvas_GO;
    private int value2;
    void Start()
    {
         Time_Script= Canvas_GO.GetComponent<Timer_Script>();
        InvokeRepeating("Camera", 0, 0.009f);
    }
    void Camera()
    {
        if (valuetoint >= 453)
        {

            planeDistance = new Vector3(-8.20f, 1.28f, 0.26f);
            cameraOffset = new Vector3(0f, 0f, 0f);
            desiredPosition = Plane.transform.position + Plane.transform.rotation * planeDistance;
        }

       
        print("Working");

    }
    private void LateUpdate()
    {
        string valuesss = Time_Script.minutes+""+Time_Script.seconds;
        valuetoint = int.Parse(valuesss);
        if(valuetoint == 50 && Time.time < 40 )
        {
            valuetoint = 500;
        }

        if(valuetoint < 453)
        {
            planeDistance = new Vector3(-8.20f, 1.28f, 0.26f);
            cameraOffset = new Vector3(0f, 0f, 0f);
            desiredPosition = Plane.transform.position + Plane.transform.rotation * planeDistance;
          //  smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);
          //  CancelInvoke("Camera");
            print("no worries okah");
        }
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);
        transform.position = smoothedPosition + Plane.transform.rotation * cameraOffset;
        transform.LookAt(Plane.transform.position);

    }

   

    // Update is called once per frame
    void Update()
    {
        /*Vector3 Plane_distance = new Vector3(-8.20f, 1.28f, 0.26f);
        transform.position = Plane.transform.position + Plane_distance;
        print(Plane.gameObject.transform.rotation);
        transform.rotation = Quaternion.Euler(Plane.gameObject.transform.rotation.x, -(Plane.gameObject.transform.rotation.y), Plane.gameObject.transform.rotation.z);
*/
        // Adjust this offset as needed
        /*
        Vector3 desiredPosition = Plane.transform.position + Plane.transform.rotation * planeDistance;
        transform.position = desiredPosition + Plane.transform.rotation * cameraOffset;

        // Point the camera towards the plane's back
        transform.LookAt(Plane.transform.position);
*/





    }
 
}
