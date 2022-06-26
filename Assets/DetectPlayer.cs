using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parent.GetComponent<EnemyAI>().Follow();
        }
    }
}
