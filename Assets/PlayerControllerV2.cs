using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : PhysicsObject {

    public float jumpTakeOffSpeed = 7f;
    public float maxSpeed = 7f;

    Vector3 defaultScale;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
	// Use this for initialization
	void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultScale = transform.localScale;
        animator = GetComponent<Animator>();
	}

    protected override void ComputeVelocity()
    {
        var move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            // cancel midair
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
            }
        }

        if (move.x > 0)
        {
            transform.localScale = new Vector3(defaultScale.x, defaultScale.y, 1);
        }
        else if(move.x < 0)
        {
            transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, 1);
        }

        animator.SetFloat("runSpeed", Mathf.Abs(velocity.x) / maxSpeed);
        bool falling = velocity.y < 0 ? true : false;
        animator.SetBool("isFalling", falling);

        targetVelocity = move * maxSpeed;
    }
}
