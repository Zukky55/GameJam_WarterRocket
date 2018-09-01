using UnityEngine;

public class QuadScroller : MonoBehaviour
{
    // スクロールするスピード
    public float m_speed = 0.1f;
    public bool m_flag = false; 

    void Update()
    {
        if(m_flag)
        {
            // 時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
            float x = Mathf.Repeat(Time.time * m_speed, 1);

            // Yの値がずれていくオフセットを作成
            Vector2 offset = new Vector2(x, 0);

            // マテリアルにオフセットを設定する
            GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
        }
    }
}