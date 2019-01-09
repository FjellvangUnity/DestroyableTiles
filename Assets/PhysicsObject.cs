using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public float gravityModifier = 1f;
    public float minGroundNormalY = .65f;

    protected bool grounded;
    protected Vector2 groundNormal;
    protected Vector2 targetVelocity;
    protected Vector2 velocity;
    protected const float minMoveDistance = 0.0001f; // delta movement limit for collision
    protected Rigidbody2D rigidbody;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected const float shellRadius = 0.01f; //make sure we dont get stuck in another gameObject
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        contactFilter.useTriggers = false; // burg ikke triggers da det er movement	
        // brug layer mask fra gameObject
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
	}
	
	// Update is called once per frame
	void Update () {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
	}

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;


        Vector2 deltaPosition = velocity * Time.deltaTime;

        // reflekter ground normal omkring y, så vi kan se hvilken
        // retning vi skal bevæge os om jorden
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="move"></param>
    /// <param name="yMovement">to make slopemovement easier</param>
           
    void Movement(Vector2 move, bool yMovement) // ymovement for nemmer
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            var count = rigidbody.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            { // brug count variablen???
                Vector2 currentNormal = hitBufferList[i].normal;
                // find ud af om player er grounded, og om han rammer fra vinkel.
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                // tag prik prodrukt i cases hvor
                // spiller rammer loftet
                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection > 0)
                {
                    // cancel out shit that would be stopped by collision.
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        rigidbody.position += move.normalized * distance;
    }

    protected virtual void ComputeVelocity() { }
}
