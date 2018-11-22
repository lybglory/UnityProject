//===================================================
//AuthorName：小斌
//CreateTime：2018-11-22 15:15:06
//Description：按键操作，让角色匀速转身
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float ver = 0;
    float hor = 0;
    public float turnspeed = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// 匀速转身方法
    /// </summary>
    /// <param name="hor"></param>
    /// <param name="ver"></param>
    void Rotating(float hor, float ver)
    {
        //获取方向
        Vector3 dir = new Vector3(-hor, 0,-ver);
        //将方向转换为四元数
        Quaternion quaDir = Quaternion.LookRotation(dir, Vector3.up);
        //缓慢转动到目标点
        transform.rotation = Quaternion.Lerp(transform.rotation, quaDir, Time.fixedDeltaTime * turnspeed);
    }

    void FixedUpdate()
    {
        if (hor != 0 || ver != 0)
        {
            //转身
            Rotating(hor, ver);
        }
    }

}