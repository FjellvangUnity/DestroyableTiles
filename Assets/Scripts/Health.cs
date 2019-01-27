using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health = 100;
    float origHealth;
    Animator animator;
    public GameObject Death;
    private void Start() {
        origHealth = health;
        animator = GetComponentInChildren<Animator>();
    }
    public float d = 0; //WTF is this?

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            Debug.Log("OUCH");
            health -= 50;
            d += 0.5f;
            GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.black, d);
            if (health < 0)
            {
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(float damage){
        health -= damage;

        animator.Play("PlayerHurt", 0);
        //var delta = 1-(health/origHealth); //from 1 to 0
        //TODO: Get beter solution
        //GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.black, delta);
        if (health <= 0)
        {
            Instantiate(Death, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
