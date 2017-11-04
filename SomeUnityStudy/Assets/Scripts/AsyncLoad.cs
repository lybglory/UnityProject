using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoad : MonoBehaviour {
    /// <summary>
    /// Slider句柄
    /// </summary>
    public Slider loadSlider;
    /// <summary>
    /// 间隔时间
    /// </summary>
    private float flInterval = 0;
    /// <summary>
    /// 异步加载操作
    /// </summary>
    AsyncOperation asyncOperaNext;
    /// <summary>
    /// 进度值
    /// </summary>
    private float flProgress;
    private void Start()
    {
        StartCoroutine("LoadNextScene");
    }

    private void Update()
    {
        loadSlider.value += flProgress;
        Debug.Log("异步加载进度值：=" + asyncOperaNext.progress.ToString());
        Debug.Log("Slide进度值：=" + loadSlider.value);
    }

    IEnumerator LoadNextScene() {

        asyncOperaNext = SceneManager.LoadSceneAsync("LevelOneScene");
        //Debug.Log("异步加载进度值：=" + flProgress);
        flProgress = asyncOperaNext.progress;
        yield return asyncOperaNext;
        //loadSlider.value;
    }
}
