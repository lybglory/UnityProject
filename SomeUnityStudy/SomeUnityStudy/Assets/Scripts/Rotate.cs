﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //抗锯齿测试.观察cube边缘。
        this.transform.Rotate(Vector3.up);
	}
}
