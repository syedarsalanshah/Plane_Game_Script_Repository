using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Parent_EO : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Child_PlaneMove_GO;
    private plane_Move PLane_Move_Script;

    public Joystick JStick;
    void Start()
    {
        PLane_Move_Script = Child_PlaneMove_GO.GetComponent<plane_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PLane_Move_Script.gameObject.transform.position;
        if(JStick.Horizontal > 0.8)
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}
