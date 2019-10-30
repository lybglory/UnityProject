//===================================================
//AuthorName：小斌
//CreateTime：2018-11-29 16:58:12
//Description：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delCtrlTest : MonoBehaviour {
    public delegate  int deltestclick(string str);
    private int age = 0;
    private deltestclick delClick;
    private string name;

    void Start () {
        delClick = GetAge;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {

            DelGetAge("LXM", delClick);
            Debug.Log(string.Format("name={0},age={1}",name, age));
        }
	}

    int GetAge(string str) {
        name = str;
        return age=29;
    }

    int DelGetAge(string str,deltestclick testDel) {
        name = str;
        age = testDel(str) + 80;
        return age;
    }
}
