using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动态生成道具
/// </summary>
public class CreatePrefab : MonoBehaviour {

    void CreateDiamondPrefab(System.Object[] objs) {
        /*  8个参数：
            原型
            红宝石x坐标最小值，最大值，
            y坐标(定值)
            z坐标min、max。
            红宝石数量
            销毁时间
         */
        GameObject originObj=null;
        float minX = 0;
        float maxX = 0;
        float y = 0;
        float minZ = 0;
        float maxZ = 0;
        int diamondNum = 0;
        float destroyTime = 0;
        if (objs!=null) {
            if (objs.Length==8) {
                originObj= (GameObject)objs[0];
                minX= System.Convert.ToSingle(objs[1]);
                maxX= System.Convert.ToSingle(objs[2]);
                y = System.Convert.ToSingle(objs[3]);
                minZ = System.Convert.ToSingle(objs[4]); ;
                maxZ = System.Convert.ToSingle(objs[5]); ;
                diamondNum = System.Convert.ToInt32(objs[6]);
                destroyTime = System.Convert.ToSingle(objs[7]);

            }
        }
        GameObject cloneObj = (GameObject)Instantiate(originObj);
        cloneObj.transform.position = new Vector3(GetRandomValue(minX,maxX), y, GetRandomValue(minZ,maxZ));
        //销毁时间
        Destroy(cloneObj, destroyTime);
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
