using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSystem : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    protected HealthSystem healthSystem;

    public float speed = 5f;
    [SerializeField] protected float rotationTime = 5f;

    private Vector2 inputVector;

    public virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
    }
    protected void Rotation()
    {
        if (inputVector.magnitude == 0.0f) return;
        float targetRotation = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
        float smoothRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, rotationTime * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, smoothRotation);
    }
}
