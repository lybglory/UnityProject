using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 实现相机跟随缩放、视野旋转
/// </summary>
public class FollowPlayer : MonoBehaviour
{

    public float scrollSpeed = 1;//相机视野缩放系数
    public float distance;//相机到目标的距离
    public float rotateSpeed = 2;//相机视野旋转系数

    private Transform player;//跟随的目标
    private Vector3 offsetPosition;//位置偏移
    private bool isRotating = false;//用来判断是否正在旋转

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;//获取跟随目标
        transform.LookAt(player.position);//使相机朝向目标
        offsetPosition = transform.position - player.position;//获得相机与目标的位置的偏移量
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offsetPosition + player.position;//实现相机跟随
        //处理视野的旋转
        RotateView();
        //处理视野的拉近拉远效果
        ScrollView();
    }

    /// <summary>
    /// 处理视野的拉近拉远效果方法
    /// </summary>
    void ScrollView()
    {
        distance = offsetPosition.magnitude;//得到偏移向量的长度
        distance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;//获取鼠标中键*相机视野缩放系数
        distance = Mathf.Clamp(distance, 4f, 10);//限定距离最小及最大值
        offsetPosition = offsetPosition.normalized * distance;//更新位置偏移
    }

    /// <summary>
    /// 处理视野的旋转方法
    /// </summary>
    void RotateView()
    {
        //      Input.GetAxis ("Mouse X");//得到鼠标在水平方向的滑动
        //      Input.GetAxis ("Mouse Y");//得到鼠标在垂直方向的滑动
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 originalPos = transform.position;//保存相机当前的位置
            Quaternion originalRotation = transform.rotation;//保存相机当前的旋转
            transform.RotateAround(player.position, player.up, rotateSpeed * Input.GetAxis("Mouse X"));//沿目标y轴在水平方向旋转
            transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));//沿自身x轴在竖直方向旋转
            float x = transform.eulerAngles.x;//获得x轴的角度
            if (x < 10 || x > 80)
            {//限制x轴的旋转在10到80之间
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
        }

        offsetPosition = transform.position - player.position;//更新位置偏移量
    }

}
