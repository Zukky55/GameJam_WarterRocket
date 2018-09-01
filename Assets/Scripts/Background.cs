using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float m_scrollSpeed = 1.0f;

    /// <summary>
    /// スクロール速度。正の値の時には下へスクロールする。負の値の時には上へスクロールする
    /// </summary>
    public float ScrollSpeed
    {
        get { return m_scrollSpeed; }
        set { m_scrollSpeed = value; }
    }

    void Update()
    {
        // Time.deltaTime を使って滑らかに縦にスクロールさせる
        transform.Translate(Vector2.left * Time.deltaTime * m_scrollSpeed);
    }
}