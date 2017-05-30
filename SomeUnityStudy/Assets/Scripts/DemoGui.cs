using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGui : MonoBehaviour {

    Rect rectPostion = GetCenter(200, 100);    //只返回一个Window，实现拖拽
    void OnGUI()
    {
        //Window一定要有返回值，否则不能实现拖拽
        rectPostion = GUILayout.Window(1, rectPostion, Func, "登录界面");
    }

    //在窗口上绘制内容
    private void Func(int id)
    {   //传入窗口id
        if (id == 1)
        {
            GUILayout.Label("用户名");
            //GUI.DragWindow();窗口拖拽
            GUI.DragWindow();
        }
    }
    //封装获取屏幕中央位置的方法
    private static Rect GetCenter(float windWidth, float windHeight)
    {
        //求出屏幕水平中间的位置
        float horizon = (Screen.width - windWidth) / 2;
        //求出屏幕垂直中间的位置
        float vertical = (Screen.height - windHeight) / 2;
        return new Rect(horizon, vertical, windWidth, windHeight);
    }

}
