using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

	public Transform FirePoint;
	public float damage = 10;
	
	// Update is called once per frame
	void Update () {
	}


	public void Attack(){
		var hit = Physics2D.Raycast(FirePoint.position, transform.right, 2);
		if (hit.collider != null)
		{
			
			if(hit.collider.tag.Equals("Player")){
				Debug.Log("HIT PLAYER");
				var health = hit.collider.GetComponent<Health>();
				if (health != null)
				{
					health.TakeDamage(damage);
				}
			}
		}
	}
}
