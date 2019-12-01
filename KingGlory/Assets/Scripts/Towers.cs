using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour {
    /// <summary>
    /// 箭头类型：0我方箭塔；1敌方箭塔
    /// </summary>
    public int towerType = -1;
    /// <summary>
    /// 存储碰触到的小兵队列
    /// </summary>
    private List<GameObject> lsSoldier = new List<GameObject>();

    /// <summary>
    /// 存储碰触到的英雄队列
    /// </summary>
    private List<GameObject> lsHero= new List<GameObject>();
    void Start () {
        if (this.gameObject.tag.Equals("OwnTower"))
        {
            towerType = 0;
        }
        else {
            towerType = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            lsHero.Add(other.gameObject);
        }
        else
        {
            SoldierMove sldMv = other.gameObject.GetComponent<SoldierMove>();
            if (sldMv&&sldMv.type!=towerType) {
                Debug.Log("lsSoldier.Count=" + lsSoldier.Count);
                lsSoldier.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            lsHero.Remove(other.gameObject);
        }
        else
        {
            lsSoldier.Remove(other.gameObject);
        }
    }
}
