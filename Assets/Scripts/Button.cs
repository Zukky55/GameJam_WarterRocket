using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    /// <summary>ボタン押されたらシーン遷移</summary>
    /// <param name="sceneNumber">遷移先インデックス</param>
    public void OnClick(int sceneNumber)
    {
        Debug.Log("OnClick()");
        FadeManager.FadeOut(sceneNumber,1.0f);
    }
}