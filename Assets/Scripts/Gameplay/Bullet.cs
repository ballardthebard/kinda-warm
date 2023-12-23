﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Color playerColor = Color.black;
    [SerializeField] private Color enemyColor = Color.green;
    private Rigidbody rb;
    private Renderer[] renderers;
    private TrailRenderer trail;
    private bool isPlayerColor;

    private void OnEnable()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (renderers == null)
        {
            renderers = GetComponentsInChildren<Renderer>();
            trail = GetComponent<TrailRenderer>();
        }

        trail.Clear();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        // Return if bullet has the correct colors
        if (gameObject.CompareTag("PlayerWeapon") && isPlayerColor || gameObject.gameObject.CompareTag("EnemyWeapon") && !isPlayerColor)
            return;

        // Set shooter color
        Color newColor = enemyColor;
        isPlayerColor = false;
        if (gameObject.CompareTag("PlayerWeapon"))
        {
            newColor = playerColor;
            isPlayerColor = true;
        }

        // Update color to match shooter
        foreach (Renderer renderer in renderers)
            renderer.material.color = newColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.collider.tag;


        if (collisionTag == gameObject.tag || collisionTag == "EnemyWeapon" || collisionTag == "PlayerWeapon") // Collided with the shooting weapon or another bullet
        {
            // Ignore collision
            return;
        }

        Recycle();
    }

    private void Recycle()
    {
        // Return bullet to pool
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}