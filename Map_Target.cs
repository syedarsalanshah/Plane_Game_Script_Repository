using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Target : MonoBehaviour
{
    public GameObject Plane_GameObject;
    private plane_Move Planes_Script;

    public MiniMapComponent MiniMapComponent_Script;
    // Start is called before the first frame update
    void Start()
    {
        Planes_Script = Plane_GameObject.GetComponent<plane_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = Planes_Script.gameObject.transform.position.z;
        float x_value_of_mapDestination = MiniMapComponent_Script.size.x;
        float y_value_of_mapDestination = MiniMapComponent_Script.size.y;

        if(value >= 620)
        {

        }

    }
}
