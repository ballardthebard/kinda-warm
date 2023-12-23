using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieAction : MonoBehaviour, IDie
{
    [SerializeField] private AimIK aimIK;
    [SerializeField] private Gun gun;
    [SerializeField] private Material deadMaterial;
    private Enemy enemy;
    private Animator animator;
    private Collider collider;
    private Renderer[] allRenderers;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    public void Die()
    {
        // Tell level manager that an enemy was killed
        LevelManager.Instance.EnemyKilled();

        // Change enemy tag so bullets will react to it as scenario
        gameObject.tag = "Untagged";

        // Drop gun
        if (gun != null)
        {
            gun.transform.parent = null;
            gun.rigidbody.isKinematic = false;
            gun.spread = 0;
        }

        // Disable aim
        if (aimIK != null)
            aimIK.enabled = false;

        // Stop all actions and freeze animation
        enemy.StopEnemyActions();
        animator.speed = 0;

        // Disable collider to allow bullets to pass through
        collider.enabled = false;

        // Set enemy to dead color
        allRenderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer childRenderer in allRenderers)
        {
            childRenderer.materials = new Material[1] { deadMaterial };
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "PlayerWeapon")
        {
            Die();
        }
    }
}
