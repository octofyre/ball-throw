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
    public int trajectoryPoints = 30;
    public float timeStep = 0.1f;

    [Header("Keyboard")]
    public KeyCode throwKey = KeyCode.Space;

    private bool holding = false;
    private bool canThrow = true;
    private float force;

    void Start()
    {
        force = startForce;

        if (trajectory != null)
        {
            trajectory.startWidth = 0.02f;
            trajectory.endWidth = 0.02f;
            trajectory.positionCount = 0;
        }
    }

    void Update()
    {
        if (!canThrow)
            return;

        if (Input.GetKeyDown(throwKey))
        {
            StartThrow();
        }

        if (holding)
        {
            force += forceIncreaseSpeed * Time.deltaTime;
            force = Mathf.Clamp(force, startForce, maxForce);

            ShowTrajectory();
        }

        if (Input.GetKeyUp(throwKey))
        {
            ReleaseThrow();
        }
    }

    public void StartThrow()
    {
        if (!canThrow)
            return;

        if (holding)
            return;

        holding = true;
        force = startForce;
    }

    public void ReleaseThrow()
    {
        if (!holding)
            return;

        if (!canThrow)
            return;

        holding = false;
        canThrow = false;

        if (basketballController != null)
        {
            basketballController.ReleaseBall();
        }

        Vector3 direction = GetThrowDirection();

        ball.linearVelocity = direction * force;

        if (ballReturn != null)
        {
            ballReturn.StartChecking();
        }

        if (trajectory != null)
        {
            trajectory.positionCount = 0;
        }
    }

    Vector3 GetThrowDirection()
    {
        Vector3 forward = launchPoint.forward;

        Quaternion angleRotation =
            Quaternion.AngleAxis(
                -throwAngle,
                launchPoint.right
            );

        Vector3 direction =
            angleRotation * forward;

        return direction.normalized;
    }

    void ShowTrajectory()
    {
        if (trajectory == null)
            return;

        Vector3 startPosition = launchPoint.position;
        Vector3 startVelocity = GetThrowDirection() * force;

        trajectory.positionCount = trajectoryPoints;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float time = i * timeStep;

            Vector3 position =
                startPosition +
                startVelocity * time +
                0.5f *
                Physics.gravity *
                time *
                time;

            trajectory.SetPosition(i, position);
        }
    }

    public void EnableThrowing()
    {
        canThrow = true;
        holding = false;
        force = startForce;
    }
}