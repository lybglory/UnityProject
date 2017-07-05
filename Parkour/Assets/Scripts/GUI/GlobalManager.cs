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
/// 音量枚举。
/// </summary>
public enum EnumVolume {
    None,
    MinVolu,
    NormalVolu,
    MaxVolu
}

/// <summary>
/// 游戏状态枚举
/// </summary>
public enum EnumGameState {
    None,
    Ready,
    Playing,
    Pause,
    End
}

/// <summary>
/// 全局参数管理类
/// </summary>
public class GlobalManager : MonoBehaviour {
    //public static 

    /// <summary>
    /// 全局静态音量变量（默认最大音）
    /// </summary>
    public static EnumVolume GlVol=EnumVolume.MaxVolu;


    /// <summary>
    /// 全局静态游戏状态（默认为none）
    /// </summary>
    public static EnumGameState GlGameState=EnumGameState.None;

    /// <summary>
    /// 全局吃到的红宝石数量
    /// </summary>
    public static int DiamondNum=0;


    /// <summary>
    /// 获取贴图中央位置
    /// </summary>
    /// <param name="textureNum">传入贴图</param>
    /// <param name="isX">true求X轴水平位置,false求Y轴</param>
    /// <returns>返回坐标值</returns>
    public static int GetTexturePosition(Texture2D textureNum, bool isX)
    {
        int position;
        //true位求X轴坐标
        if (isX)
        {
            position = (Screen.width - textureNum.width) / 2;
        }
        else
        {//false返回Y轴坐标
            position = (Screen.height - textureNum.height) / 2;
        }
        return position;

    }

}
