using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationTime = 5f;

    private PlayerInput inputActions;
    private Rigidbody2D rb2d;
    private HealthSystem healthSystem;

    private Vector2 inputVector;

    private void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.Gameplay.Enable();

        rb2d = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
    }
    private void FixedUpdate()
    {
        inputVector = inputActions.Gameplay.Move.ReadValue<Vector2>();
        Move();
        Rotation();
    }
    private void Move()
    {
        if (!healthSystem.isKnockBacked)
        {
            rb2d.velocity = inputVector * speed * Time.deltaTime;
        }
    }
    private void Rotation()
    {
        if (inputVector.magnitude == 0.0f) return;
        float targetRotation = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
        float smoothRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, rotationTime * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, smoothRotation);
    }
}