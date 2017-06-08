/***
unity version:unity5.6.1f1
点击button，弹出窗口并显示内容，点击窗口的【关闭】按钮，则关闭窗口
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI实现窗口关闭和弹窗 : MonoBehaviour {

    private bool isShow = false;//标志位
    private void OnGUI()
    {
        if (GUILayout.Button("确定")) {
            isShow = true;
        }
        if (isShow) {
            //GUILayout.Window(int id,Rect screentRect, WindowFunction func,string text); 
            GUILayout.Window(1,GetRecPosition(200,100), Func,"My windows");
        }
        
    }

    /// <summary>
    /// 返回新建窗体，显示关闭窗体按钮
    /// </summary>
    /// <param name="windowId">返回新窗体</param>
    private void Func(int windowId) {
        if (windowId==1) {
            GUILayout.Label("But in return,you must take a life");
            if (GUILayout.Button("关闭")) {
                isShow = false;
            }   
        }
    }

    /// <summary>
    /// 获取屏幕中央位置的方法
    /// </summary>
    /// <param name="recWidth">窗体的宽度</param>
    /// <param name="recHight">窗体的高度</param>
    /// <returns>返回屏幕中央的位置</returns>
    private Rect GetRecPosition(float recWidth,float recHight) {
        //求出屏幕水平、垂直的位置
        float horizon = (Screen.width- recWidth) / 2;
        float vertical = (Screen.height - recHight) / 2;
        return new Rect(horizon, vertical, recWidth, recHight);
    }
}
