﻿using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private AudioClip gunshotSFX;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float fireRate = 1f;
    public float spread = 0.3f;

    private int currentAmmo;
    private float nextTimeToFire;

    [HideInInspector] public Rigidbody rigidbody;

    private void Start()
    {
        Reload();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        // Verify if the Gun can shoot
        if (Time.time < nextTimeToFire || currentAmmo <= 0) return;

        // Calculate spread direction
        Vector3 spreadDirection = muzzle.transform.forward + (Random.insideUnitSphere * spread);
        Quaternion spreadRotation = Quaternion.LookRotation(spreadDirection);

        // Get bullet from pool and set its positionand rotation based on the muzzle and spread
        GameObject bullet = BulletPool.SharedInstance.GetPooledObject();
        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = spreadRotation;

        nextTimeToFire = Time.time + 1f / fireRate;

        // If gun belongs to player, decrease ammo and play SFX
        if (gameObject.CompareTag("PlayerWeapon"))
        {
            bullet.tag = "PlayerBullet";
            currentAmmo--;
            SoundManager.Instance.PlaySFX(gunshotSFX);
        }
        else
        {
            bullet.tag = "EnemyBullet";
        }

        bullet.SetActive(true);
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }

    public void Release()
    {
        transform.parent = null;
        rigidbody.isKinematic = false;
    }
}
