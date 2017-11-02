using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonobehaviorTest : MonoBehaviour {
    private void Awake(){
        Debug.Log("I'm Awake()");
    }

    private void OnEnable(){
        Debug.Log("I'm OnEnable()");
        StartCoroutine("IETest");//开始协程
    }
    // Use this for initialization
    void Start () {
        Debug.Log("I'm Start()");
        //StartCoroutine("IETest");
	}
	
	void Update () {
        Debug.Log("I'm Update()");
    }

    private void LateUpdate(){
        Debug.Log("I'm LateUpdate()");
    }

    private void OnGUI(){
        Debug.Log("I'm OnGUI()");
    }
    private void OnDisable(){
        Debug.Log("I'm OnDisable()");
        StopCoroutine("IETest");//停止协程
    }

    private void OnDestroy(){
        Debug.Log("I'm OnDestroy()");
    }

    IEnumerator IETest() {
        yield return new WaitForSeconds(1f);
        while (true){
            yield return new WaitForSeconds(0.1f);
            Debug.Log("协程");
        }
    }
}
