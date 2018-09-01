using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueText : MonoBehaviour
{
    /// <summary>canvasを流すスピード</summary>
    [SerializeField] float m_speed;
    /// <summary>PrologueCanvas</summary>
    GameObject m_canvas;
    Rigidbody m_rb;


    void Start()
    {
        //PrologueCanvasを取得
        m_canvas = GameObject.Find("PrologueCanvas");
    }

    void Update()
    {
        float y = Input.GetAxisRaw("Vertical");

        float move = y * m_speed;

        //Y軸プラス方向にtextを動かしていく
        transform.Translate(transform.up * m_speed);

        if (transform.localPosition.y >= 1000)
        {
            Destroy(m_canvas);
            m_canvas = null;
        }
        Debug.Log(m_canvas == null);
    }
}
