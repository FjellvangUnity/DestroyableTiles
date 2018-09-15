using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int health = 100;
    public float d = 0;

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
}
