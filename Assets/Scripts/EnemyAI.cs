using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // ターゲットが見つかれば
        if (target != null)
        {
            // ターゲットを追跡する
            navMeshAgent.SetDestination(target.position);
        }
    }

}
