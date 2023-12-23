using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class EnemyMoveAction : MonoBehaviour, IMove
    {
        [SerializeField] private Transform target;
        [SerializeField] private float destinationUpdateRate; // The frequency the NavMeshAgent will update it's destination

        private NavMeshAgent agent;
        private float timer;
        private bool isMoving;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
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
        }
    }
}