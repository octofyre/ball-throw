using UnityEngine;

public class BallThrow : MonoBehaviour
{
    [Header("References")]
    public Rigidbody ball;
    public Transform launchPoint;
    public LineRenderer trajectory;
    public BasketballController basketballController;
    public BallReturn ballReturn;

    [Header("Throw Force")]
    public float startForce = 15f;
    public float maxForce = 80f;
    public float forceIncreaseSpeed = 40f;

    [Header("Throw Angle")]
    [Range(0f, 80f)]
    public float throwAngle = 30f;

    [Header("Trajectory")]
    public int trajectoryPoints = 40;
    public float timeStep = 0.05f;

    [Header("Trajectory Width")]
    public float startWidth = 0.01f;
    public float endWidth = 0.003f;

    private float force;
    private bool holding;
    private bool canThrow = true;

    void Start()
    {
        force = startForce;

        if (trajectory != null)
        {
            trajectory.useWorldSpace = true;
            trajectory.positionCount = 0;
            trajectory.startWidth = startWidth;
            trajectory.endWidth = endWidth;
        }
    }

    void Update()
    {
        if (!canThrow)
            return;

        if (holding)
        {
            force += forceIncreaseSpeed * Time.deltaTime;
            force = Mathf.Clamp(force, startForce, maxForce);

            ShowTrajectory();
        }
    }

    // BUTTON POINTER DOWN
    public void StartThrow()
    {
        if (!canThrow)
            return;

        if (holding)
            return;

        holding = true;
        force = startForce;
    }

    // BUTTON POINTER UP
    public void ReleaseThrow()
    {
        if (!holding)
            return;

        ThrowBall();
    }

    Vector3 GetThrowDirection()
    {
        Vector3 direction = launchPoint.forward;

        Quaternion rotation =
            Quaternion.AngleAxis(
                -throwAngle,
                launchPoint.right
            );

        return (rotation * direction).normalized;
    }

    Vector3 GetThrowVelocity()
    {
        return GetThrowDirection() * force;
    }

    void ShowTrajectory()
    {
        if (trajectory == null)
            return;

        Vector3 startPosition = launchPoint.position;
        Vector3 velocity = GetThrowVelocity();

        trajectory.positionCount = trajectoryPoints;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float time = i * timeStep;

            Vector3 position =
                startPosition +
                velocity * time +
                0.5f * Physics.gravity * time * time;

            trajectory.SetPosition(i, position);
        }
    }

    void ThrowBall()
    {
        holding = false;
        canThrow = false;

        Vector3 velocity = GetThrowVelocity();

        if (basketballController != null)
        {
            basketballController.ReleaseBall();
        }

        ball.linearVelocity = velocity;

        if (ballReturn != null)
        {
            ballReturn.StartChecking();
        }

        if (trajectory != null)
        {
            trajectory.positionCount = 0;
        }
    }

    public void EnableThrowing()
    {
        canThrow = true;
        holding = false;
        force = startForce;

        if (trajectory != null)
        {
            trajectory.positionCount = 0;
        }
    }
}