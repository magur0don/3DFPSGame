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
        // �^�[�Q�b�g���������
        if (target != null)
        {
            // �^�[�Q�b�g��ǐՂ���
            navMeshAgent.SetDestination(target.position);
        }
    }

}
