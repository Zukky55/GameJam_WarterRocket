using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueManager : MonoBehaviour
{

    private GameObject m_PCanvas;

    public void Start()
    {
        m_PCanvas = GameObject.Find("PrologueCanvas");
    }

    public void Update()
    {
        if (m_PCanvas == null)
        {
            StartCoroutine("SceneWait");
        }
    }

    private IEnumerator SceneWait()
    {
        yield return new WaitForSeconds(1f);
        FadeManager.FadeOut(2, 1.0f);
    }

}
