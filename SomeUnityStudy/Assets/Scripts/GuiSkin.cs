/***
* unity version:unity5.6.1f1
* GUI Skin简单运用
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiSkin : MonoBehaviour {
    public GUISkin btnGuiSkin;
    public Texture2D texture2d_btn;
    private void OnGUI()
    {
        GUI.skin = btnGuiSkin;
        //GetStyle(stirng txt)这个参数需要与GUI Skin Custom Style中的Name保持一直。
        GUI.Button(new Rect(0, 0, 700, 700), texture2d_btn, btnGuiSkin.GetStyle("Btn1"));
    }

}
