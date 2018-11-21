using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleControl : MonoBehaviour {
    /// <summary>
    /// 角色控制器
    /// </summary>
    private CharacterController characterController;
    /// <summary>
    /// 射线
    /// </summary>
    private Ray ray;
    /// <summary>
    /// 目标位置
    /// </summary>
    private Vector3 targetPosition;
    RaycastHit raycastHit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0)) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out raycastHit)){
                //判断射线碰到的土体是否是"Plane"
                if (raycastHit.collider.gameObject.name.Equals("Plane",System.StringComparison.CurrentCultureIgnoreCase)) {
                    targetPosition = raycastHit.point;
                }
            }
        }

        //Scene视图画射线
        if (targetPosition!=Vector3.zero) {
            Debug.DrawLine(Camera.main.transform.position, targetPosition,Color.red);
        }
        
    }
}
