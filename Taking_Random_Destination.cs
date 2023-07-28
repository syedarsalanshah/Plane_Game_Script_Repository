using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taking_Random_Destination : MonoBehaviour
{
    private List<int> Destination_Spawn_Points_List = new List<int>() {-477,-410,-366,-195,-565,-904,-1080};
    private int Selected_Value;
    // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Random.Range(0, 7);
        float Taking_destination_postions = Destination_Spawn_Points_List[randomIndex];
        transform.position = new Vector3(Taking_destination_postions,transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
