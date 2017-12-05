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
	// Use this for initialization
	void Start () {
        NMAgent=transform.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (NMAgent&TransTarget) {
            NMAgent.SetDestination(TransTarget.position);
        }
	}
}
