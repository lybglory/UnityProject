using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//引用导航命名空间
public class ObstacleBridge : MonoBehaviour {
    //持有NavMeshObtacle组件
    private NavMeshObstacle NvmeshObstacle;
	void Start () {
        NvmeshObstacle = gameObject.GetComponent<NavMeshObstacle>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            //禁用组件，可以通行
            NvmeshObstacle.enabled = false;
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        } else if (Input.GetMouseButtonUp(0)) {
            //启用，禁止通行
            NvmeshObstacle.enabled = true;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
	}
}
