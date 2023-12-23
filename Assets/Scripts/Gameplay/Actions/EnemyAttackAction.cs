using RootMotion.FinalIK;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyAttackAction : MonoBehaviour, IAttack
{
    [SerializeField] private Transform target;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate; // The frequency the enemy will shoot
    [SerializeField] private int animationLayer;

    private Animator animator;
    private AimIK aimIK;
    private Gun gun;
    private bool isAttacking;
    private float timer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        aimIK = GetComponent<AimIK>();
        gun = GetComponentInChildren<Gun>();

        animator.SetLayerWeight(animationLayer, 1);
    }

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

    public void RemoveWeapon()
    {
        StopAttack();
        aimIK.enabled = false;
        gun.rigidbody.velocity = Vector3.zero;
        gun.spread = 0;
        animator.SetLayerWeight(animationLayer, 0);
    }
}