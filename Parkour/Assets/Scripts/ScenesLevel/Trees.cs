/***
*	title:跑酷项目开发
*	[副标题]
*	Description：
*	[描述]
*	Date：2017
*	Version：0.1
*	Modify Recoder：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour {
    private string plName="Hero";

    private void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {   //一旦障碍物碰到英雄，游戏结束
        if (collision.gameObject.name== plName) {
            GlobalManager.GlGameState = EnumGameState.End;
        }
    }
}
