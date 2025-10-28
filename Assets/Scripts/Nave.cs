using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public float moveSpeed = 5.0f;

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

        _rigidbody.linearVelocity = new Vector2(moveDirection * moveSpeed, 0f);
        
    }
}