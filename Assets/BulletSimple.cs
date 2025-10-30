using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletSimple : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 1.5f;
    Rigidbody2D rb;
    float t;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    void OnEnable()
    {
        t = lifetime;
        rb.linearVelocity = transform.up * speed; // anda para a direção do "up"
        rb.angularVelocity = 0f;
    }

    void Update()
    {
        t -= Time.deltaTime;
        if (t <= 0f) Destroy(gameObject);
    }
}
