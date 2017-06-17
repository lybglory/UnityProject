/***
*	title:跑酷项目开发
*	[副标题]
*	Description：第一关卡
*	[描述]
*	Date：2017
*	Version：0.1
*	Modify Recoder：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOne_Manager : MonoBehaviour {
    private AudioSource aduioLevelOne;      //持有音频组件

    private void Awake()
    {
        aduioLevelOne=GameObject.Find("_LevelOneAudioManager/Audio").GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        aduioLevelOne = GameObject.Find("_LevelOneAudioManager/Audio").GetComponent<AudioSource>();
        aduioLevelOne.Play();
        aduioLevelOne.loop = true;              //开启循环播放
        //根据开始场景保存下来的全局静态音量枚举设置音量大小
        switch (GlobalManager.GlVol) {
            case EnumVolume.MaxVolu:
                aduioLevelOne.volume = 1;
                break;
            case EnumVolume.NormalVolu:
                aduioLevelOne.volume = 0.5f;
                break;
            case EnumVolume.MinVolu:
                aduioLevelOne.volume = 0;
                break;
            case EnumVolume.None:
                aduioLevelOne.volume = 0;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
