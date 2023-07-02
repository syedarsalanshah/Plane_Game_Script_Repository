using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Coins_Spawn : MonoBehaviour
{
    public GameObject coinPrefab; // Reference to the coin prefab
    public float spawnHeight = 0.5f; // Height above the surface to spawn the coins
    public int maxCoins = 50; // Maximum number of coins to spawn

    void Start()
    {
        Instantiate(coinPrefab, new Vector3(965.3131713867188f, 6f, 261.44940185546877f), Quaternion.Euler(270, 17.9849396f, 0));
    }

    
}
