using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Second_Camera : MonoBehaviour
{

    public GameObject PLane_GO;
    private plane_Move Plane_Script;
    // Start is called before the first frame update
    public Transform pivot; // Reference to the pivot point GameObject
    public float rotationSpeed = 30f; // Adjust this value to control rotation speed

    private Vector3 offset = new Vector3(10,10,10); // Camera offset from the pivot point

    void Start()
    {
        Plane_Script = PLane_GO.GetComponent<plane_Move>();
        // Calculate the initial offset between the camera and the pivot
        offset = transform.position - pivot.position;
    }

    void LateUpdate()
    {
        transform.LookAt(Plane_Script.transform.position);
    }
}
