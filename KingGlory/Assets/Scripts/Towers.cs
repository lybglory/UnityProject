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
    public List<GameObject> lsSoldier = new List<GameObject>();

    /// <summary>
    /// 存储碰触到的英雄队列
    /// </summary>
    public List<GameObject> lsHero= new List<GameObject>();
    /// <summary>
    /// 子弹起始位置
    /// </summary>
    [SerializeField]
    private Transform bulletPosition;

    /// <summary>
    /// 子弹原型
    /// </summary>
    [SerializeField]
    private GameObject prefabBullet;
    /// <summary>
    /// 子弹父类
    /// </summary>
    [SerializeField]
    private Transform bulletParent;


    void Start () {
        if (this.gameObject.tag.Equals("OwnTower"))
        {
            towerType = 0;
        }
        else {
            towerType = 1;
        }
        bulletPosition = this.transform.Find("BulletPosition");
        bulletParent = this.transform.Find("BulletParent");

        InvokeRepeating("CreateBullet", 0.1f, 1f);
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

    /// <summary>
    /// 创建子弹
    /// </summary>
    void CreateBullet()
    {
        if (lsSoldier.Count==0&&lsHero.Count==0) {
            return;
        }
        GameObject objBullet = (GameObject)Instantiate(prefabBullet, bulletPosition.position, Quaternion.identity);
        objBullet.transform.parent = bulletParent;
        BulletTarget(objBullet);
    }

    /// <summary>
    /// 子弹攻击目标，先从小兵集合获取，再从英雄集合获取
    /// </summary>
    /// <param name="obj">目标物</param>
    public void BulletTarget(GameObject obj) {
        if (lsSoldier.Count > 0)
        {
            obj.GetComponent<Bullet>().SetBulletTarget(lsSoldier[0]);
        }
        else {
            obj.GetComponent<Bullet>().SetBulletTarget(lsHero[0]);
        }
    }
}
