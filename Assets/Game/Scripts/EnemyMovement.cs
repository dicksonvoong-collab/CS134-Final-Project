using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerLocation;

    private NavMeshAgent agent;

    //sets nav mesh agent
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // enemy moves toward player
    void Update()
    {
        if (playerLocation != null)
        {
            agent.SetDestination(playerLocation.position);
        }
    }

}
