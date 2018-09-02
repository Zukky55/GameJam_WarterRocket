using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ResultManager : MonoBehaviour
{
    /// <summary>Score of Stage1</summary>
    [SerializeField] Text[] m_scoreTexts = new Text[4];
    /// <summary>ResultCanvas</summary>
    [SerializeField] GameObject m_resultTexts;
    [SerializeField] float m_speed;
    /// <summary>ステージ毎のScore(圧力)</summary>
    private int[] m_scores = new int[3];
    /// <summary>highScore</summary>
    private int m_highScore = 0;
    /// <summary>PlayerPrefsで保存する為のキー</summary>
    private string m_highScoreKey = "highScore";
    /// <summary>Total Score</summary>
    private int m_totalScore;
    /// <summary>ResultFadeInフラグ</summary>
    private bool m_isRunning = true;
    
    


    /// <summary>初期化</summary>
    public void Initialize()
    {
        //scoreを全て0に戻す
        for (int i = 0; i < m_scores.Length; i++)
        {
            m_scores[i] = 0;
        }

        //トータルスコアも初期化
        m_totalScore = 0;

        //highSCoreを取得.保存されてなければ0を取得.
        m_highScore = PlayerPrefs.GetInt(m_highScoreKey, 0);
    }


    /// <summary>各スコアのキーを配列に代入する</summary>
    /// <param name="stage1">stage1のスコアキー</param>
    /// <param name="stage2">stage2のスコアキー</param>
    /// <param name="stage3">stage3のスコアキー</param>
    private void AddScores(string stage1, string stage2, string stage3)
    {
        //各要素にステージ毎のスコアを入れる
        m_scores[0] = PlayerPrefs.GetInt(stage1, 0) * 1000;
        m_scores[1] = PlayerPrefs.GetInt(stage2, 0) * 1000;
        m_scores[2] = PlayerPrefs.GetInt(stage3, 0) * 1000;

        //トータルスコアを算出
        m_totalScore = m_scores.Sum() * 1000;

        //トータルスコアが暫定ハイスコアより高い時ハイスコア更新、保存する
        if(m_highScore < m_totalScore)
        {
            Save();
        }
    }


    /// <summary>ハイスコアの保存</summary>
    public void Save()
    {
        PlayerPrefs.SetInt(m_highScoreKey, m_highScore);
        PlayerPrefs.Save();
    }

    /// <summary>各テキスト欄にスコアを表示させる</summary>
    private void ShowScore()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(m_scoreTexts.Length);
            m_scoreTexts[i].text = m_scores[i].ToString() + "圧力";
            Debug.Log("m_scoreTexts" + m_scoreTexts[i]);
        }
        m_scoreTexts[3].text = m_totalScore.ToString() + "圧力";

    }

    public void Start()
    {
        Initialize();
        AddScores("stage1","stage2","stage3");
        ShowScore();
        Save();
        ResultFadeIn();
    }

    private void ResultFadeIn()
    {
        if (m_isRunning)
        {
            Debug.Log(m_resultTexts.transform);
            m_resultTexts.transform.Translate(transform.up * m_speed);
/*            if (transform.localPosition.y >= -83)
            {
                m_isRunning = false;
                return;
            }
*/        }
    }   

}
