using UnityEngine;

public class Tiros : MonoBehaviour
{
    public float speed = 200.0f;

    public float maxLifetime = 4.0f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }


}
