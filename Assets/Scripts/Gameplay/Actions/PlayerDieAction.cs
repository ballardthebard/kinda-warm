using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieAction : MonoBehaviour, IDie
{
    public void Die()
    {
        // Go back to hub
        LevelManager.Instance.LoadLevel(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "EnemyBullet" || collision.collider.tag == "EnemyWeapon")
        {
            Die();
        }
    }
}
