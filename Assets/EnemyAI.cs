using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private Transform playerTransform;
    private Vector3 destination;

    private bool searchingForNewPosition;
    private float walkTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        destination = transform.position;
    }

    void Update()
    {
        if ((Vector3.Distance(destination, transform.position) <= 0.1f || walkTime <=0) && !searchingForNewPosition)
        {
            searchingForNewPosition = true;
            StartCoroutine(NewPosition());
        }
        animator.SetBool("Walking", agent.velocity.magnitude > 0.1);
        walkTime -= Time.deltaTime;
    }

    private IEnumerator NewPosition()
    {
        yield return new WaitForSeconds(5f);
        destination = RandomNavmeshLocation(20f);
        agent.SetDestination(destination);
        searchingForNewPosition = false;
        walkTime = 15f;
    }

    public void Follow()
    {
        walkTime = 10f;
        StartCoroutine(FollowPlayer());
    }

    private IEnumerator FollowPlayer()
    {
        agent.speed = 6;
        searchingForNewPosition = true;
        for (int i = 0; i < 100; i++)
        {
            WalkToPlayer();
            yield return new WaitForSeconds(0.1f);
        }
        agent.speed = 4;
        searchingForNewPosition = false;
    }

    private void WalkToPlayer()
    {
        destination = new Vector3(playerTransform.transform.position.x, 0, playerTransform.transform.position.z);
        agent.SetDestination(destination);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
