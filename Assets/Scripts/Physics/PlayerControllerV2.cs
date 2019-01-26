using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : PhysicsObject {

    public float jumpTakeOffSpeed = 7f;
    public float maxSpeed = 7f;

    Vector3 defaultScale;
    private Animator animator;
    private float lastfired;
    public float timeToFire = 0.05f;
    public GameObject bullet;
    public float bulletSpeed = 20f;
    public int jumps = 1;

    // Use this for initialization
    void Awake() {
        defaultScale = transform.localScale;
        animator = GetComponentInChildren<Animator>();
	}
    void Fire()
    {
        lastfired += Time.deltaTime;
        if (lastfired <= timeToFire )
        {
            return;
        }
        CameraShake.shakeDuration = 0.1f;

        var bul = Instantiate(bullet, transform.position, Quaternion.identity);
        bul.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        var rig = bul.GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector3(transform.localScale.x,0,0).normalized * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bul, 2f);
        lastfired = 0;
    }

    protected override void ComputeVelocity()
    {
        var move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        if (grounded)
        {
           velocity.y = 0; 
        }

        if (Input.GetButtonDown("Jump") && (grounded || jumps > 0))
        {
            jumps--;
            velocity.y = jumpTakeOffSpeed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            Fire();
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
        animator.SetBool("isFalling", !grounded);

        targetVelocity = move * maxSpeed;
        if (grounded)
        {
            //TODO: change if we want to change num of jumps
            jumps = 1;
            
        }
    }
}
