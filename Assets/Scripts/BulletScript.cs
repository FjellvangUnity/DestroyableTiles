using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {


    public GameObject particles;
    public float bulletSpeed = 20f;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb.velocity = transform.right * bulletSpeed;
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
