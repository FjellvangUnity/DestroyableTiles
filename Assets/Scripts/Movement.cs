using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : PlayerController {


    SensorScript sensor;
    GameObject toFollow;
	// Use this for initialization
	void Start () {
        base.Start();
        sensor = GetComponentInChildren<SensorScript>();
	}
	
	// Update is called once per frame
	void Update () {
        toFollow = sensor.toFollow;

        Move(CalcDir());
	}

    float CalcDir()
    {
        if (toFollow == null)
        {
            return 0;
        }

        Vector3 delta = toFollow.transform.position - transform.position;

        return delta.x > 0 ? 1 : -1;
    }
}
