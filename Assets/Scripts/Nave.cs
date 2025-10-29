using UnityEngine;

public class Player : MonoBehaviour
{
    public Tiros tirosPrefab;
    
    private Rigidbody2D _rigidbody;

    public float moveSpeed = 5.0f;

    public float fireRate = 0.25f;

    private float _lastShootTime = 0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float moveDirection = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = -1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = 1.0f;
        }

        _rigidbody.linearVelocity = new Vector2(moveDirection * moveSpeed, _rigidbody.linearVelocity.y);

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(0)) && Time.time - _lastShootTime >= fireRate)
        {
            Shoot();
            _lastShootTime = Time.time;
        }

    }
    
    private void Shoot()
    {
        Tiros tiros = Instantiate(this.tirosPrefab, this.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(tiros.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        tiros.Project(Vector2.up);
    }
}