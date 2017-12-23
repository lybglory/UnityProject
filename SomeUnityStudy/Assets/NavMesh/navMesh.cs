using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//NavMeshAgent命名空间


public class navMesh : MonoBehaviour {
    private NavMeshAgent NMAgent;
    /// <summary>
    /// 目标位置
    /// </summary>
    public Transform TransTarget;

	void Start () {
		//get component
        	NMAgent=transform.GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        	if (NMAgent&TransTarget) {
            		//设置自动寻路
            		NMAgent.SetDestination(TransTarget.position);
        	}
	}
}
