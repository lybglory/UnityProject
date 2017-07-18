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
using UnityEngine.SceneManagement;
public class LevelOne_Manager : MonoBehaviour {
    private AudioSource aduioLevelOne;      //持有音频组件
    private string strGameOverScene;                //结算场景名称
    private void Awake()
    {
        strGameOverScene = "3_GameOver";
        aduioLevelOne =GameObject.Find("_LevelOneAudioManager/LevelOneAudio").GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        //执行协程
        StartCoroutine("Countdown");
        StartCoroutine("GameStateCheck");
        aduioLevelOne = GameObject.Find("_LevelOneAudioManager/LevelOneAudio").GetComponent<AudioSource>();
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

    IEnumerator GameStateCheck()
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("游戏状态等待一秒");
        while (true)
        {
            //Debug.Log("进入while循环开始执行等待一秒");
            yield return new WaitForSeconds(1f);
            //全局里程
            ++GlobalManager.Shifting;

            //Debug.Log("while循环等待一秒已到开始判定游戏状态");
            if (GlobalManager.GlGameState == EnumGameState.End)
            {
                //Debug.Log("游戏状态="+GlobalManager.GlGameState.ToString());
                yield return new WaitForSeconds(2f);
                //Debug.Log("等待2秒结束，开始加载结算界面");
                SceneManager.LoadScene(strGameOverScene);
            }
        }
    }
}
