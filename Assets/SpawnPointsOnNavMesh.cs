using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPointsOnNavMesh : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private GameObject PointPrefab;
    [SerializeField] private float maxDistanceFromCenter;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(PointPrefab, GetRandomLocation(), Quaternion.identity);
        }
    }

    Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        Vector3 point = new Vector3(0, 0, amount + 1);
        while (Vector3.Distance(Vector3.zero, point) > maxDistanceFromCenter)
        {
            // Pick the first indice of a random triangle in the nav mesh
            int t = Random.Range(0, navMeshData.indices.Length - 3);

            // Select a random point on it
            point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
            Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);
        }


        return new Vector3(point.x, 0, point.z);
    }
}
