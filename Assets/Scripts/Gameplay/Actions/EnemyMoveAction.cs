using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveAction : MonoBehaviour, IMove
{
    [SerializeField] private Transform target;
    [SerializeField] private float destinationUpdateRate; // The frequency the NavMeshAgent will update it's destination
    [SerializeField] private int animationLayer;

    private NavMeshAgent agent;
    private Animator animator;
    private float timer;
    private bool isMoving;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        animator.SetLayerWeight(animationLayer, 1);
    }

    private void Update()
    {
        if (!isMoving) return;

        // Update timer and check if it should update it's destination
        timer += Time.unscaledDeltaTime;
        if (timer >= destinationUpdateRate)
        {
            timer -= destinationUpdateRate;
            agent.SetDestination(target.position);
        }
    }

    public void Move()
    {
        isMoving = true;
    }

    public void StopMove()
    {
        isMoving = false;
        // Stop NavMeshAgent and zero it's velocity to make it immediate
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        // Disable agent so that it wont interfere with pathfinding
        agent.enabled = false;
    }
}