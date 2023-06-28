using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Reference to the coin prefab
    public float spawnHeight = 0.5f; // Height above the surface to spawn the coins
    public int maxCoins = 50; // Maximum number of coins to spawn

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        GameObject[] planeObjects = GameObject.FindGameObjectsWithTag("Plane");
        GameObject[] finishObjects = GameObject.FindGameObjectsWithTag("Finish");

        int coinsSpawned = 0;

        foreach (GameObject planeObject in planeObjects)
        {
            if (coinsSpawned >= maxCoins)
                break;

            Vector3 topSpawnPosition = GetTopSurfacePosition(planeObject.transform);

            if (topSpawnPosition != Vector3.zero && IsWithinRange(topSpawnPosition.x, topSpawnPosition.z))
            {
                Instantiate(coinPrefab, topSpawnPosition, Quaternion.identity);
                coinsSpawned++;
            }
        }

        foreach (GameObject finishObject in finishObjects)
        {
            if (coinsSpawned >= maxCoins)
                break;

            Vector3 topSpawnPosition = GetTopSurfacePosition(finishObject.transform);

            if (topSpawnPosition != Vector3.zero && IsWithinRange(topSpawnPosition.x, topSpawnPosition.z))
            {
                Instantiate(coinPrefab, topSpawnPosition, Quaternion.identity);
                coinsSpawned++;
            }
        }
    }

    Vector3 GetTopSurfacePosition(Transform surfaceObject)
    {
        Renderer renderer = surfaceObject.GetComponent<Renderer>();

        if (renderer == null)
        {
            Debug.LogWarning("Renderer component not found on surface object: " + surfaceObject.name);
            return Vector3.zero;
        }

        Bounds bounds = renderer.bounds;
        float spherecastRadius = bounds.extents.y + spawnHeight;

        Vector3 origin = new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);
        Vector3 direction = Vector3.down;

        RaycastHit hit;
        if (Physics.SphereCast(origin, spherecastRadius, direction, out hit))
        {
            Vector3 coinPosition = hit.point;
            coinPosition.y = Mathf.Max(coinPosition.y, -7.06f + spawnHeight); // Adjust the minimum Y position
            return coinPosition;
        }

        return Vector3.zero;
    }

    bool IsWithinRange(float x, float z)
    {
        float xMin = -11.86f;
        float xMax = 356f;
        float zMin = -1.73f;
        float zMax = 206f;

        return x >= xMin && x <= xMax && z >= zMin && z <= zMax;
    }


}
