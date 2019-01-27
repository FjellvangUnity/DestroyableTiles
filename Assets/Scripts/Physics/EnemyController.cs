using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject
{
    private Vector3 defaultScale;
    public float RangeToPlayer = 2;
    public float jumpTakeOffSpeed = 7f;
    bool WithinRange = false;
    public Transform player;

    public float maxSpeed = 7;
    public Animator animator;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        defaultScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();
    }


    public override void FixedUpdate(){
        var offset = new Vector3(transform.localScale.x, 0, 0);
        Debug.DrawRay(transform.position + offset, offset * 2, Color.red);

        var hit = Physics2D.Raycast(this.transform.position + offset, offset * 2, 1);
        if (hit.collider != null)
        {
          //TODO: OPTIMIZE
            if (hit.collider.tag == "Ground")
            {
                velocity.y = jumpTakeOffSpeed;
            }
        }
        base.FixedUpdate();
    }

    public override void Update(){
      base.Update();

    }

    protected override void ComputeVelocity()
    {
        var move = (player.position - this.transform.position);

        if (grounded)
        {
           velocity.y = 0; 
        }
        move.y = 0;
        if (move.x > 0)
        {
            transform.localScale = new Vector3(defaultScale.x, defaultScale.y, 1);
        }
        else if (move.x < 0)
        {
            transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, 1);
        }
        animator.SetFloat("runSpeed", Mathf.Abs(velocity.x) / maxSpeed);
        if (move.magnitude < RangeToPlayer)
        {
            WithinRange = true;
            return;
        }
        WithinRange = false;
        move.Normalize();
        if(hitWall){
          velocity.y = jumpTakeOffSpeed;
        }

        targetVelocity = move * maxSpeed;
        Debug.Log(string.Format("velocity: {0}, rig{1}", velocity, rigidbody.velocity));

    }
}

