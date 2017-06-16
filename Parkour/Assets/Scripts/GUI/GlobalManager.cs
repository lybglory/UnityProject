/***
*	title:全局管理
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

/// <summary>
/// 音量枚举，枚举定义在类的外边
/// </summary>
public enum EnumVolumn {
    None,
    MinVolu,
    NormalVolu,
    MaxVolu
}

/// <summary>
/// 全局参数管理类
/// </summary>
public class GlobalManager : MonoBehaviour {
    /// <summary>
    /// 全局静态音量变量
    /// </summary>
    public static EnumVolumn GlVol;
	
}
