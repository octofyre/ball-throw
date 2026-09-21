using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    [Header("Movement Limits")]
    public float minX = -5f;
    public float maxX = 5f;

    Rigidbody rb;
    float move;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(
            move * speed,
            rb.linearVelocity.y,
            0
        );

        // Clamp player's X position
        Vector3 position = rb.position;

        position.x = Mathf.Clamp(position.x, minX, maxX);

        rb.position = position;
    }

    public void Left()
    {
        move = -1;
    }

    public void Right()
    {
        move = 1;
    }

    public void Stop()
    {
        move = 0;
    }
}