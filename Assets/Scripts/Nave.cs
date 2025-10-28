using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5.0f;
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

        transform.position += new Vector3(moveDirection, 0f, 0f) * moveSpeed * Time.deltaTime;
        
    }
}