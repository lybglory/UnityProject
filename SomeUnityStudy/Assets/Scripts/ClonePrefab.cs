using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePrefab : MonoBehaviour {
    //创建道具的方法
    void ClPrefabs(GameObject ObjClOriginal)
    {
        GameObject ObjClone = Instantiate(ObjClOriginal);
    }
}
