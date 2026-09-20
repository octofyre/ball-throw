using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody rb;
    float move;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(move * speed, rb.linearVelocity.y, 0);
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