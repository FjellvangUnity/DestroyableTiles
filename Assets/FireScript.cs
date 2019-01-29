using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour {

    public GameObject bullet;
	public Transform FirePoint;
    private float lastfired;
    public float timeToFire = 0.05f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Fire()
    {
        lastfired += Time.deltaTime;
        if (lastfired <= timeToFire )
        {
            return;
        }
        CameraShake.shakeDuration = 0.1f;

        var bul = Instantiate(bullet, FirePoint.position, FirePoint.rotation);
        //bul.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        //var rig = bul.GetComponent<Rigidbody2D>();
        //rig.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bul, 2f);
        lastfired = 0;
    }
}
