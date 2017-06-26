/***
*	title:"parkour"项目开发
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
public class Diamonds : MonoBehaviour {

    public float DiamondRotateSpeed = 1f;
    private AudioSource audioEffect;
    private void Start()
    {
        audioEffect= GameObject.Find("_LevelOneAudioManager/EffectAudio").GetComponent<AudioSource>();
        //根据开始场景保存下来的全局静态音量枚举设置音量大小
        switch (GlobalManager.GlVol)
        {
            case EnumVolume.MaxVolu:
                audioEffect.volume = 1;
                break;
            case EnumVolume.NormalVolu:
                audioEffect.volume = 0.5f;
                break;
            case EnumVolume.MinVolu:
                audioEffect.volume = 0;
                break;
            case EnumVolume.None:
                audioEffect.volume = 0;
                break;
        }

        //回调函数，方法，时间，时间间隔。1秒钟调用1次
        InvokeRepeating("DiamondRotate",1,0.1f);
    }

    /// <summary>
    /// 红宝石自转
    /// </summary>
    void DiamondRotate() {
        //红宝石世界坐标系自转
        this.transform.Rotate(Vector3.up * DiamondRotateSpeed, Space.World);
    }

    /// <summary>
    /// 碰撞器IsTrigger勾选。表示不受力的作用。可以穿透该物体
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Hero"))
        {
            ++GlobalManager.DiamondNum;
            //播放吃红宝石音效
            audioEffect.Play();
            Debug.Log("吃到的红宝石数量" + GlobalManager.DiamondNum);
            Destroy(this.gameObject);
            
        }
    }
    
}
