using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueManager : MonoBehaviour
{
    /// <summary>PrologueCanvas</summary>
    private GameObject m_PCanvas;
    /// <summary>コルーチンのフラグ</summary>
    private bool m_isRunning = true;

    public void Start()
    {
        m_PCanvas = GameObject.Find("PrologueCanvas");
    }

    public void Update()
    {
        if (m_PCanvas == null && m_isRunning)
        {
                StartCoroutine("SceneWait");
        }
    }

    private IEnumerator SceneWait()
    {
        m_isRunning = false;
        yield return new WaitForSeconds(1f);
        FadeManager.FadeOut(2, 1.0f);
    }

}
