using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornStandMoving : MonoBehaviour {
    private float flStandMovingSpeed=4f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.back * Time.deltaTime * flStandMovingSpeed, Space.World);
        if (transform.position.z<=-90) {
            transform.position = new Vector3(1,-4.07f,90);
        }
	}
}
