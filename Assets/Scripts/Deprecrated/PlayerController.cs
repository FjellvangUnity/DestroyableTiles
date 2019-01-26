using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    public float maxSpeed = 5f;

    Vector3 velocity;
    public float acceleration = 20;
    public float jumpSpeed = 5;
    public float groundY = -5;
    public float bulletSpeed = 20f;
    public float timeToFire = 1f;
    float lastfired = 0;
    bool grounded = false;
    public float gravity = 15;
    bool facingRight = true;
    Vector3 defaultScale;
    int jumps = 0;
    Rigidbody2D rig;
    Animator animator;

    public GameObject bullet;
    //public Vector2 influence = new Vector2(5, 5);

	// Use this for initialization
	internal void Start () {
        lastfired = timeToFire;
        defaultScale = transform.localScale;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        var x = Input.GetAxis("Horizontal");

        Move(x);
        if (Input.GetKey(KeyCode.E))
        {
            Fire();
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        bool falling = (velocity.y < 1 && !grounded) ? true : false;
        Debug.Log(grounded);
        animator.SetBool("isFalling", falling);
//      Debug.Log(velocity);
        //transform.position = newPos;
        //transform.Translate(newPos);
    }

    public void Move(float horizontalDirection)
    {
        bool running = Mathf.Abs(horizontalDirection) > 0 ? true : false;
        animator.SetBool("isRunning", running);

        velocity.x = Mathf.MoveTowards(velocity.x, horizontalDirection * maxSpeed, Time.deltaTime * acceleration);
        if (!grounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        Vector2 newPos = transform.position + (velocity * Time.deltaTime);
        if (newPos.y < groundY)
        {
            //HitGround();
            Destroy(gameObject);
        }
        else
        {
            grounded = false;
        }

        if (velocity.x > 0)
        {
            facingRight = true;
            transform.localScale = new Vector3(defaultScale.x, defaultScale.y, defaultScale.z);
        }
        else if (velocity.x < 0)
        {
            facingRight = false;
            transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
        }


        rig.velocity = velocity;
    }

    private void HitGround()
    {
        velocity.y = 0;
        grounded = true;
        jumps = 0;
    }

    void Jump()
    {
        if (grounded || jumps < 2)
        {
            velocity.y = jumpSpeed;
            jumps++;
        }
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
        rig.AddForce(facingRight ? Vector3.right * bulletSpeed : Vector3.right * -bulletSpeed, ForceMode2D.Impulse);
        Destroy(bul, 2f);
        lastfired = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") ||
            collision.collider.CompareTag("Enemy"))
        {
            HitGround();
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("Ground"))
        {
            if (velocity.y < 0)
            {
                velocity.y = 0;
            }
        }
    }
}
