using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject {

	public Transform player;

	public float maxSpeed = 7;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


    }
    // Update is called once per frame
    void Update()
    {


    }

    protected override void ComputeVelocity()
    {
		var move = (this.transform.position - player.position);
		move.y = 0;
		move.Normalize();

		targetVelocity = move * maxSpeed;
		Debug.Log(string.Format("move: {0}, player{1}", move, player.position));

    }
}
