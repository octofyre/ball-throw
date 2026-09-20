using UnityEngine;

public class BallReturn : MonoBehaviour
{
    [Header("References")]
    public BallThrow ballThrow;
    public BasketballController basketballController;

    [Header("Return Settings")]
    public float returnDelay = 4f;

    private bool waitingForReturn = false;
    private float timer = 0f;

    void Update()
    {
        if (!waitingForReturn)
            return;

        timer += Time.deltaTime;

        if (timer >= returnDelay)
        {
            ReturnBall();
        }
    }

    public void StartChecking()
    {
        waitingForReturn = true;
        timer = 0f;
    }

    void ReturnBall()
    {
        waitingForReturn = false;
        timer = 0f;

        if (basketballController != null)
        {
            basketballController.CatchBall();
        }

        if (ballThrow != null)
        {
            ballThrow.EnableThrowing();
        }

        Debug.Log("Ball returned to dribbling.");
    }
}