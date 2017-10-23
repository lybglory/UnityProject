using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
    private string strPlName = "Hero";
    private void OnTriggerEnter(Collider other)
    {   //Is Trigger需要勾选
        if (other.name.Equals(strPlName)) {
            //触发的时候判断是否是俯冲动画，如果是俯冲，表明穿过去了。否则游戏结束
            if (GlobalManager.EnumPlAction!=EnumPlayerAnima.Subduction) {
                GlobalManager.GlGameState = EnumGameState.End;
            }
        }
    }
}
