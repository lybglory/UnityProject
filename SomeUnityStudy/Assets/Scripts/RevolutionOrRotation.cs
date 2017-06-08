using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolutionOrRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private GameObject earthGo;
    private GameObject moonGo;
    private GameObject sunGo;

    private void Awake()
    {
        sunGo = GameObject.Find("Sun");
        moonGo = GameObject.Find("Moon");
        earthGo = GameObject.Find("Earth");
    }
    // Update is called once per frame
    void Update () {
        sunGo.transform.Rotate(Vector3.up,Space.World);
        moonGo.transform.Rotate(Vector3.up, Space.World);
        earthGo.transform.Rotate(Vector3.up, Space.World);

        earthGo.transform.RotateAround(sunGo.transform.position,Vector3.up,45f*Time.deltaTime);
        moonGo.transform.RotateAround(earthGo.transform.position, Vector3.up, 1f);


    }
}
