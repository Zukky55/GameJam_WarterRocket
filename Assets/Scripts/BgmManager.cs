using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    /// <summary>AudioSource</summary>
    private AudioSource m_audio;
    /// <summary>PrologueCanvas</summary>
    GameObject m_PCanvas;

    public void Start()
    {
        //AudioSource取得
        m_audio = GetComponent<AudioSource>();
        //再生
        m_audio.Play();
        //PrologueCanvas取得
        m_PCanvas = GameObject.Find("PrologueCanvas");
    }

    private void Update()
    {
        //PrologueCanvasがシーンから消えたらBGMをフェードアウトさせる
        if(m_PCanvas == null)
        {
            m_audio.volume -= Time.deltaTime;
            if(m_audio.volume <= 0)
            {
                m_audio.volume = 0f;
            }
            Debug.Log("called");
        }
    }

}
