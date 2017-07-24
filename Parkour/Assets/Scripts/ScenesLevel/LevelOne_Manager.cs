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
    private AudioSource aduioLevelOne;              //持有音频组件
    private string strGameOverScene;                //结算场景名称
    public GameObject orinalObj;                    //预制原型
    public GameObject DynamicCreateObj;             //动态创建道具对象
    public GameObject ObjLeft;                      //参考对象，X坐标最小值 （左边）
    public GameObject ObjRight;                     //参考对象，X坐标最大值 （右边）
    public GameObject ObjHero;
    private void Awake()
    {
        strGameOverScene = "3_GameOver";
        aduioLevelOne =GameObject.Find("_LevelOneAudioManager/LevelOneAudio").GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        //try again之后置零
        GlobalManager.GlGameState = EnumGameState.Ready;
        GlobalManager.Shifting = 0;
        GlobalManager.DiamondNum = 0;

        //执行协程
        StartCoroutine("GameStateCheck");

        //动态生成道具
        //1秒创建一次
        InvokeRepeating("DynamicCreateAllObj", 1, 1);


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
            //当游戏在玩的时候，开始统计里程
            if (GlobalManager.GlGameState==EnumGameState.Playing) {
                //全局里程
                ++GlobalManager.Shifting;
            }
           

            //Debug.Log("while循环等待一秒已到开始判定游戏状态");
            if (GlobalManager.GlGameState == EnumGameState.End)
            {
                //Debug.Log("游戏状态="+GlobalManager.GlGameState.ToString());
                yield return new WaitForSeconds(2f);
                //Debug.Log("等待2秒结束，开始加载结算界面");
                SceneManager.LoadScene(strGameOverScene);
                Debug.Log("LevelOne最终里程："+ GlobalManager.Shifting);
            }
        }
    }

    /// <summary>
    /// 动态创建道具的方法
    /// </summary>
    /// <param name="originalObj">道具预制原型</param>
    /// <param name="minX">坐标X最小值</param>
    /// <param name="maxX">坐标X最大值</param>
    /// <param name="y">y坐标，定值</param>
    /// <param name="minZ">坐标X最小值</param>
    /// <param name="maxZ">坐标Z最大值</param>
    /// <param name="destoryTime">销毁时间</param>
    /// <param name="diamondNum">克隆数量</param>
    private void DynamicCreatePrefab(GameObject originalObj,float minX, float maxX, float y, float minZ, float maxZ, float destroyTime, int diamondNum) {
        /*  8个参数：
            红宝石x坐标最小值，最大值，
            z坐标min、max。
            y坐标(定值)
            红宝石数量
            销毁时间
         */
        System.Object[] Objs = new System.Object[8];
        Objs[0] = originalObj;
        Objs[1] = minX;
        Objs[2] = maxX;
        Objs[3] = y;
        Objs[4] = minZ;
        Objs[5] = maxZ;
        Objs[6] = diamondNum;
        Objs[7] = destroyTime;

        //发送数据
        DynamicCreateObj.SendMessage("CreateDiamondPrefab",Objs, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// 根据里程数来动态创建所有道具
    /// </summary>
    private void DynamicCreateAllObj() {
        if (GlobalManager.Shifting==0) {
            DynamicCreatePrefab(orinalObj,ObjLeft.transform.position.x,ObjRight.transform.position.x,ObjLeft.transform.position.y,ObjHero.transform.position.z+100,ObjHero.transform.position.z+300,30,10);
        }
    }
}
