using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent_PLane_Script : MonoBehaviour
{
    public GameObject Plane_Script_Gameobject;
    private plane_Move Plane_Script;
    // Start is called before the first frame update
    void Start()
    {
        Plane_Script = Plane_Script_Gameobject.GetComponent<plane_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(1, 0, 0);
        Plane_Script.gameObject.transform.rotation = transform.rotation; 
        transform.position = Plane_Script.transform.position;
    }
}
