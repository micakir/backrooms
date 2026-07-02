using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance < 15)
        {
            agent.SetDestination(player.position);
        }
        if(distance < 1.8f)
        {
            // Game Over
        }
    }
}