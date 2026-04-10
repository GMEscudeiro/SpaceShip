using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.useFullKinematicContacts = true;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Enemy"))
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
            if (GameManager.instance != null)
            {
                GameManager.instance.OnEnemyKilled();
            }
        }
    }

    void Update()
    {
        if (transform.position.x > 6f)
        {
            Destroy(gameObject);
        }
    }
}
