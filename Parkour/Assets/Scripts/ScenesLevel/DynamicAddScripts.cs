using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAddScripts : MonoBehaviour {
    /// <summary>
    /// 目标游戏道具对象
    /// </summary>
    public GameObject ObjNeed;

    private void OnTriggerEnter(Collider other)
    {
        if (ObjNeed != null)
        {
            //第一次碰撞之后禁用，避免每次碰撞都会加载此类导致播放音效
            ObjNeed.AddComponent<ThornStandMoving>();
            this.gameObject.SetActive(false);
        }
    }
}
