using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject
{
    private Vector3 defaultScale;
    public float RangeToPlayer = 2;
    public float jumpTakeOffSpeed = 7f;
    bool WithinRange = false;
    public GameObject player;

    public LayerMask jumpLayer;

    public float maxSpeed = 7;
    public Animator animator;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        defaultScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
    }


    public override void FixedUpdate(){
        var offset = transform.right/2;
        Debug.DrawRay(transform.position + offset, offset * 2, Color.red);

        var hit = Physics2D.Raycast(this.transform.position + offset, transform.right, 3, jumpLayer);
        Debug.DrawRay(this.transform.position+offset,Vector2.up, Color.red);
        if (hit.collider != null)
        {
          //TODO: OPTIMIZE
          Debug.Log("HIT " + hit.collider.tag);
            if (hit.collider.tag == "Ground")
            {
                velocity.y = jumpTakeOffSpeed;
            }
        }
        base.FixedUpdate();
    }

    public override void Update(){
      base.Update();
      animator.SetBool("withinRange", WithinRange);
    }

    protected override void ComputeVelocity()
    {
        if (player == null)
        {
            return;
        }
        var move = (player.transform.position - this.transform.position);

        if (grounded)
        {
           velocity.y = 0; 
        }
        move.y = 0;
        if (move.x > 0 && !facingRight)
        {
          Flip();
            //transform.localScale = new Vector3(defaultScale.x, defaultScale.y, 1);
        }
        else if (move.x < 0 && facingRight)
        {
          Flip();
            //transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, 1);
        }
        animator.SetFloat("runSpeed", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetBool("isFalling", !grounded);
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

    }

}

