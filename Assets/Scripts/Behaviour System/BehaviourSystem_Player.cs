using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSystem_Player : BehaviourSystem
{
    private PlayerInput inputActions;
    private Vector2 inputVector;
    public override void Awake()
    {
        base.Awake();
        inputActions = new PlayerInput();
        inputActions.Gameplay.Enable();
    }
    private void FixedUpdate()
    {
        inputVector = inputActions.Gameplay.Move.ReadValue<Vector2>();
        Rotation();
        Move();
    }
    private void Move()
    {
        if (!healthSystem.isKnockBacked)
        {
            rb2d.velocity = inputVector * speed * Time.deltaTime;
        }
    }
}
