using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate.time="+Time.deltaTime);
    }
}
