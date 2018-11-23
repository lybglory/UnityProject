using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleControl : MonoBehaviour {
    /// <summary>
    /// 角色控制器
    /// </summary>
    private CharacterController characterController;

    /// <summary>
    /// 目标位置
    /// </summary>
    private Vector3 targetPosition;
    /// <summary>
    /// 目标位置的方向和距离
    /// </summary>
    private Vector3 direction;
    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField]
    private float moveSpeed=5;
    /// <summary>
    /// 目标转身四元数
    /// </summary>
    Quaternion targetQuaternion;
    /// <summary>
    /// 转身速度
    /// </summary>
    [SerializeField]
    private float rotateSpeed=1;
    /// <summary>
    /// 标记位，标记是否转身完
    /// </summary>
    private bool isRotateOver;

    // Use this for initialization
    void Start () {
        characterController = this.transform.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        #region 鼠标右键拾取
        if (Input.GetMouseButtonUp(1)) {
            Ray rightRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rightRaycastHit;
            if (Physics.Raycast(rightRay, out rightRaycastHit,Mathf.Infinity,1<<LayerMask.NameToLayer("Item"))) {
                BoxCtrl boxCtrl = rightRaycastHit.collider.GetComponent<BoxCtrl>();
                if (boxCtrl!=null) {
                    //调用点击的方法
                    boxCtrl.BoxHit();
                }
            }
        }
        #endregion

        if (Input.GetMouseButtonUp(0)) {
            Ray leftRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit leftRaycastHit;
            if(Physics.Raycast(leftRay,out leftRaycastHit)){
                //判断射线碰到的土体是否是"Plane"
                if (leftRaycastHit.collider.gameObject.name.Equals("Plane",System.StringComparison.CurrentCultureIgnoreCase)) {
                    targetPosition = leftRaycastHit.point;
                    isRotateOver = false;
                    rotateSpeed = 0;
                }
            }
        }

        
        if (targetPosition!=Vector3.zero) {
            //Scene视图画射线
            Debug.DrawLine(Camera.main.transform.position, targetPosition,Color.red);
            //移动到目标点，先要得到目标点的方向和距离
            direction = targetPosition - this.transform.position;
            //方向和距离要进行归一化
            direction = direction.normalized ;
            direction.y = 0;//解决短距离移动时，角色向前倒的bug
            //当目标点和角色点之间的距离>0.1才会进行移动，解决角色抖动的bug
            if (Vector3.Distance(targetPosition, this.transform.position) >0.1f) {
                //--角色转身--
                if (isRotateOver==false)
                {
                    rotateSpeed += 5f;
                    targetQuaternion = Quaternion.LookRotation(direction);
                    //让角色缓慢转身,匀速旋转Slerp插值
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime * rotateSpeed);
                    //当目标和角色的夹角<1时。转身结束
                    if (Quaternion.Angle(targetQuaternion, transform.rotation)<1)
                    {
                        rotateSpeed = 1;//转身速度归1
                        isRotateOver = true;
                    }
                }
                //--角色转身--

                //角色移动需要乘以一个时间变量，平滑移动
                characterController.Move(direction * Time.deltaTime * moveSpeed);
            }
            
        }
        
    }
}
