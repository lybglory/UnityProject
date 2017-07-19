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
        GameObject originObj = null;
        int minX = 0;
        int maxX = 0;
        int y = 0;
        int minZ = 0;
        int maxZ = 0;
        int diamondNum = 0;
        int destroyTime = 0;
        if (objs!=null) {
            if (objs.Length==8) {
                objs[0]= originObj;
                objs[1] = minX;
                objs[2] = maxX;
                objs[3] = y;
                objs[4] = minZ;
                objs[5] = maxZ;
                objs[6] = diamondNum;
                objs[7] = destroyTime;

            }
        }
        GameObject cloneObj = GameObject.Instantiate<GameObject>(originObj);
        cloneObj.transform.position = new Vector3(GetRandomValue(minX,minZ), y, GetRandomValue(minZ,maxZ));
        //销毁时间
        Destroy(cloneObj, destroyTime);
    }

    /// <summary>
    /// 获取随机值
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private int GetRandomValue(int min,int max) {
        int randomValue;
        randomValue = Random.Range(min, max);
        return randomValue;
    }
}
