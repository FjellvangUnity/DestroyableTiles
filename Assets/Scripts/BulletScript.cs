using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {


    public GameObject particles;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            var health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                
                health.TakeDamage(10);
            }
            Instantiate(particles, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }
    }
}
