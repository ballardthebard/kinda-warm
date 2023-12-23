using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyAttackAction : MonoBehaviour, IAttack
    {
        [SerializeField] private Transform target;
        [SerializeField] private Gun gun;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackRate; // The frequency the enemy will shoot

        private bool isAttacking;
        private float timer;

        private void Update()
        {
            // Only attack if allowed and within range
            if (!isAttacking || Vector3.Distance(target.position, transform.position) > attackRange) return;

            // Update timer and check if it should attack
            timer += Time.deltaTime;
            if (timer >= attackRate)
            {
                timer -= attackRate;
                gun.Shoot();
            }
        }

        public void Attack()
        {
            isAttacking = true;
        }

        public void StopAttack()
        {
            isAttacking = false;
        }
    }
}