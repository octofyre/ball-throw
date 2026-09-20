using UnityEngine;

public class BasketballController : MonoBehaviour
{
    [Header("References")]
    public Transform Ball;
    public Transform Arms;
    public Transform PosOverHead;
    public Transform PosDribble;

    [Header("Arm Settings")]
    public Vector3 OverHeadArmRotation = new Vector3(-100f, 0f, 0f);

    [Header("Dribble Settings")]
    public float DribbleBounceHeight = 0.5f;
    public float DribbleBounceSpeed = 5f;

    [Header("State Variables")]
    private bool isBallInHands = true;
    private bool isHoldingOverHead = false;

    void Update()
    {
        if (!isBallInHands)
            return;

        if (isHoldingOverHead || Input.GetKey(KeyCode.Space))
        {
            Ball.position = PosOverHead.position;
            Arms.localEulerAngles = OverHeadArmRotation;
        }
        else
        {
            float bounce =
                Mathf.Abs(
                    Mathf.Sin(Time.time * DribbleBounceSpeed)
                ) * DribbleBounceHeight;

            Ball.position = PosDribble.position + Vector3.up * bounce;
            Arms.localEulerAngles = Vector3.zero;
        }
    }

    public void SetHoldOverHeadTrue()
    {
        isHoldingOverHead = true;
    }

    public void SetHoldOverHeadFalse()
    {
        isHoldingOverHead = false;
    }

    public void ReleaseBall()
    {
        isBallInHands = false;
        isHoldingOverHead = false;
    }

    public void CatchBall()
    {
        isBallInHands = true;
        isHoldingOverHead = false;
    }
}