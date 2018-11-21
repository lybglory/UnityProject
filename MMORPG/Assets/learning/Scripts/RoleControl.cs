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
    /// <summary>
    /// 目标位置的方向和距离
    /// </summary>
    private Vector3 direction;
    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField]
    private float moveSpeed=5;

    // Use this for initialization
    void Start () {
        characterController = this.transform.GetComponent<CharacterController>();
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

        
        if (targetPosition!=Vector3.zero) {
            //Scene视图画射线
            Debug.DrawLine(Camera.main.transform.position, targetPosition,Color.red);
            //移动到目标点，先要得到目标点的方向和距离
            direction = targetPosition - this.transform.position;
            //方向和距离要进行归一化
            direction = direction.normalized;
            //当目标点和角色点之间的距离>0.1才会进行移动，解决角色抖动的bug
            if (Vector3.Distance(this.transform.position, targetPosition) >0.1f) {
                
                //角色移动需要乘以一个时间变量，平滑移动
                characterController.Move(direction*Time.deltaTime*moveSpeed);
            }
            
        }
        
    }
}
