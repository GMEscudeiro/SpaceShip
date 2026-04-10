using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.useFullKinematicContacts = true;
        }
    }

    void FixedUpdate()
    {
        if (rb != null) {
            float scale = GameManager.instance != null ? GameManager.instance.environmentTimeScale : 1f;
            rb.linearVelocity = new Vector2(-speed * scale, 0);
        }
    }

    void Update()
    {
        if (transform.position.x < -6f)
        {
            Destroy(gameObject);
        }
    }
}
