using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 场景枚举
/// </summary>
public enum EnumScene {
    StartScene,
    LoadScene,
    LevelOneScene,
    LevelTwoScene
}
public class GlobalParamater : MonoBehaviour {
    /// <summary>
    /// 全局静态枚举变量
    /// </summary>
    public static EnumScene GlScene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
