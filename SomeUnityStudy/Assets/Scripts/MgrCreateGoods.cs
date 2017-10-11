using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MgrCreateGoods : MonoBehaviour {
    public GameObject ObjOriginal; //预制体的原型
    public GameObject ObjSend;     //创建道具方法所在的游戏对象
    //public GameObject ObjClone;
	// Use this for initialization
	void Start () {
        //延迟调用
        InvokeRepeating("CloneGoods", 1, 2);
	}

    //创建道具
    void CloneGoods() {
        //数据传递给ObjSend游戏对象上挂载的脚本，
        //也就是说把当前类中的ObjOriginal这个数据，传给ObjSend游戏对象上挂载的脚本里的ClPrefabs方法
        ObjSend.SendMessage("ClPrefabs", ObjOriginal, SendMessageOptions.DontRequireReceiver);
    }
}
