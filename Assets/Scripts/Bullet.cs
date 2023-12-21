using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;

    private void OnEnable()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        rb.AddForce(transform.forward * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Return bullet to pool
        Recycle();
    }

    private void Recycle()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
