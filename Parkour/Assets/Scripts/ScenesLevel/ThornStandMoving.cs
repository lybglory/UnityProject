using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornStandMoving : MonoBehaviour {
    private float flStandMovingSpeed=4f;
    /// <summary>
    /// AudioSource组件数组
    /// </summary>
    private AudioSource[] arrayAduios;
    // Use this for initialization
    void Start () {
        //数组第一个元素是吃红宝石特效声音
        //第二个元素倒刺声音特效
        arrayAduios = GameObject.Find("_LevelOneAudioManager/EffectAudio").GetComponents<AudioSource>();
        //一旦动态加载的时候，就会播放
        //倒刺音量特效只播放一次。若放在触发函数里，每次碰触都会播放。
        arrayAduios[1].loop = false;
        arrayAduios[1].volume = 3;
        arrayAduios[1].Play();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.back * Time.deltaTime * flStandMovingSpeed, Space.World);
        if (transform.position.z<=-90) {
            transform.position = new Vector3(1,-4.07f,GlobalManager.ORIGINALPOINT);
        }
	}
}
