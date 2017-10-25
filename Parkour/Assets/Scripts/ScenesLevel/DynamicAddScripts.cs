using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAddScripts : MonoBehaviour {
    /// <summary>
    /// 目标游戏道具对象
    /// </summary>
    public GameObject ObjNeed;
    /// <summary>
    /// AudioSource组件数组
    /// </summary>
    private AudioSource[] arrayAduios;

    private void Start()
    {   //数组第一个元素是吃红宝石特效声音
        //第二个元素倒刺声音特效
        arrayAduios = GameObject.Find("_LevelOneAudioManager/EffectAudio").GetComponents<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ObjNeed!=null) {
            //倒刺音量特效只播放一次
            arrayAduios[1].loop = false;
            arrayAduios[1].volume = 3;
            arrayAduios[1].Play();
            ObjNeed.AddComponent<ThornStandMoving>();
        }
    }
}
