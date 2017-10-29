using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动态生成道具
/// </summary>
public class CreatePrefab : MonoBehaviour {

    void CreateDiamondPrefab(System.Object[] objs) {
        /*  9个参数：
            原型
            红宝石x坐标最小值，最大值，
            y坐标(定值)
            z坐标min、max。
            克隆数量
            销毁时间
            是否是南桥道具参考对象
         */
        GameObject originObj=null;
        float minX = 0;
        float maxX = 0;
        float y = 0;
        float minZ = 0;
        float maxZ = 0;
        int cloneNum = 0;
        float destroyTime = 0;
        bool IsSourth = true;
        if (objs!=null) {
            if (objs.Length==9) {
                originObj= (GameObject)objs[0];
                minX= System.Convert.ToSingle(objs[1]);
                maxX= System.Convert.ToSingle(objs[2]);
                y = System.Convert.ToSingle(objs[3]);
                minZ = System.Convert.ToSingle(objs[4]); ;
                maxZ = System.Convert.ToSingle(objs[5]); ;
                cloneNum = System.Convert.ToInt32(objs[6]);
                destroyTime = System.Convert.ToSingle(objs[7]);
                IsSourth = (bool)objs[8];

            }
        }
        //克隆指定的游戏对象
        for (int i = 0; i < cloneNum; i++)
        {
            GameObject cloneObj = (GameObject)Instantiate(originObj);
            if (IsSourth)
            {   //左桥道具创建的位置
                cloneObj.transform.position = new Vector3(GetRandomValue(minX, maxX), y, GetRandomValue(minZ, maxZ));
            }
            else {
                //北桥坐标系变了
                cloneObj.transform.position = new Vector3(GetRandomValue(minZ, maxZ),  y, GetRandomValue(minX, maxX));
            }
            Debug.Log("minZ=" + minZ + ";maxZ=" + maxZ + ";创建出来道具的z轴坐标：" + cloneObj.transform.position.z);
            //销毁时间
            Destroy(cloneObj, destroyTime);
        }
        

        
    }

    /// <summary>
    /// 获取随机值
    /// </summary>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <returns>获取随机值</returns>
    private float GetRandomValue(float min,float max) {
        float randomValue;
        randomValue = Random.Range(min, max);
        return randomValue;
    }
}
