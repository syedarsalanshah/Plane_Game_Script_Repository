using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    
    public GameObject Plane;
    public float followSpeed = 5f; // Adjust this value to control the smoothness of camera movement
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 Plane_distance = new Vector3(-8.20f, 1.28f, 0.26f);
        transform.position = Plane.transform.position + Plane_distance;
        print(Plane.gameObject.transform.rotation);
        transform.rotation = Quaternion.Euler(Plane.gameObject.transform.rotation.x, -(Plane.gameObject.transform.rotation.y), Plane.gameObject.transform.rotation.z);
*/
        Vector3 planeDistance = new Vector3(-8.20f, 1.28f, 0.26f);
        Vector3 cameraOffset = new Vector3(0f, 0f, 0f); // Adjust this offset as needed
        /*
        Vector3 desiredPosition = Plane.transform.position + Plane.transform.rotation * planeDistance;
        transform.position = desiredPosition + Plane.transform.rotation * cameraOffset;

        // Point the camera towards the plane's back
        transform.LookAt(Plane.transform.position);
*/
        

        Vector3 desiredPosition = Plane.transform.position + Plane.transform.rotation * planeDistance;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);
        transform.position = smoothedPosition + Plane.transform.rotation* cameraOffset;
        transform.LookAt(Plane.transform.position);

    }
}
