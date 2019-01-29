using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject toFollow;
    public float followFactor = .1f;

    //Private
    Vector3 delta;
    Vector3 thisPos;
    Vector3 thatPos;

    float counter = 0; // for testing
        
	// Update is called once per frame
	void Update () {
        //TODO: verify if this branch is needed
        if (toFollow == null)
        {
            return;
        }
        thisPos = gameObject.transform.position;
        thatPos = toFollow.transform.position;
        thatPos = new Vector3(thatPos.x, thatPos.y, thisPos.z); // Fix z
        counter += Time.deltaTime;
        if (thisPos != thatPos)
        {
            delta = thatPos - thisPos;
            //Vector3 newVec = Vector3.Lerp(thisPos, thatPos, delta.magnitude * followFactor * Time.deltaTime);
            gameObject.transform.position = Vector3.MoveTowards(thisPos, thatPos, followFactor * delta.sqrMagnitude );
        }	
	}
}
