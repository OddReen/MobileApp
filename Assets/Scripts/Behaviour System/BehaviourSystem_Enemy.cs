using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSystem_Enemy : BehaviourSystem
{
    GameObject player;

    public override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (!healthSystem.isKnockBacked && player != null)
        {
            Rotation();
            CatchPlayer();
        }
    }
    private void CatchPlayer()
    {
        Vector2 playerDir = (player.transform.position - transform.position);
        rb2d.velocity = playerDir.normalized * speed * Time.deltaTime;
    }
}
