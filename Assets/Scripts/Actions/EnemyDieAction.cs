using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieAction : MonoBehaviour, IDie
{
    [SerializeField] private AimIK aimIK;
    [SerializeField] private Transform gun;
    [SerializeField] private Color deadColor = Color.black;
    private Enemy enemy;
    private Animator animator;
    private Renderer[] allRenderers;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        allRenderers = gameObject.GetComponentsInChildren<Renderer>();
    }

    public void Die()
    {
        // Tell level manager that an enemy was killed
        LevelManager.Instance.EnemyKilled();

        // Change enemy tag so bullets will react to it as scenario
        gameObject.tag = "Untagged";

        // Drop gun
        if (gun != null)
            gun.parent = null;

        // Disable aim
        if (aimIK != null)
            aimIK.enabled = false;

        // Stop all actions and freeze animation
        enemy.StopEnemyActions();
        animator.speed = 0;

        // Set enemy to dead color
        foreach (Renderer childRenderer in allRenderers)
        {
            foreach (Material material in childRenderer.materials)
            {
                material.color = deadColor;
            }
        }
    }
}
