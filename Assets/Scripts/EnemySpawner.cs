using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float timeToSpawn = 2f;
    public GameObject toSpawn;
    float timer = 0;
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= timeToSpawn)
        {
            Instantiate(toSpawn, transform.position, Quaternion.identity);
            timer = 0;
        }
	}

}
