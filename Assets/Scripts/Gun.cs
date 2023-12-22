using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float spread = 0.3f;

    private int currentAmmo;
    private float nextTimeToFire = 0f;

    private void Start()
    {
        Reload();
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
        bullet.tag = transform.tag;
        bullet.SetActive(true);

        nextTimeToFire = Time.time + 1f / fireRate;
        currentAmmo--;
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
