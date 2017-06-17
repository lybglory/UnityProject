/***
*	title:游戏主角控制类
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

public class Ctrl_Player : MonoBehaviour {
    public float runSpeed=1f;               //跑步速度
    private Animation plAnimation;          //持有Animation组件
    private Vector3 startPoint;
    // Use this for initialization
    void Start () {
        plAnimation=this.gameObject.GetComponent<Animation>();
        startPoint = this.gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //根据游戏状态，判定玩家是否奔跑。当倒计时结束后玩家才开始奔跑
        if (GlobalManager.GlGameState != EnumGameState.Playing) {
            plAnimation.Play("Stand");
        } else if (GlobalManager.GlGameState== EnumGameState.Playing) {
            plAnimation.Play("Run");
            this.transform.Translate(Vector3.forward * runSpeed, Space.Self);//以第一人视角跑动
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(Vector3.down * runSpeed, Space.Self);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.up * runSpeed, Space.Self);
            }
            if (this.transform.position.z > 93)
            {
                this.transform.position = startPoint;
            }
        }
        
    }
}
