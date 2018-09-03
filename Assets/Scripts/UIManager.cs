using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UIを管理するスクリプト
public class UIManager : MonoBehaviour {

    private static UIManager _uiManager = null;
    public static UIManager GetUIManager() {
        return _uiManager;
    }

    //タイマーと連打用テキスト（差し替え予定）
    [SerializeField] Text timerText;
    [SerializeField] Text clickText;
    [SerializeField] Text shieldText;

    //フェードイン・アウト用
    [SerializeField] SpriteRenderer fadeTexture;

    //シールドゲージ用
    [SerializeField] Image shieldGauge;

    //連打時の制限時間を更新する処理
    public void UpdateTimer(float t) {
        if(t >= 0)
        timerText.text = "Time: " + t.ToString("00.00");
        else {
            timerText.text = "Time: 00.00";
        }
    }

    //連打時の制限時間を表示
    public void SetTimer(bool show) {
        timerText.enabled = show;
    }

    //連打せよ！のUIを表示
    public void ClickUI(bool show) {
        clickText.enabled = show;
    }

    public void ChangeClickUI(bool c) {
        if (c) {
            clickText.text = "圧力エネルギーを充填せよ！\n (スペースキー)\n\n";
        }
        else {
            clickText.text = "圧力エネルギーを再充填 !";
        }
    }


    //シールド展開のUI表示
    public void ShowShieldText(bool show) {
        shieldText.enabled = show;
    }

    //フェードイン・アウト用のテクスチャのアルファ値を設定する
    public void SetFadeAlpha(float a) {
        fadeTexture.color = new Color(0, 0, 0, a);
    }

    //フェードイン処理
    public void FadeIn() {
        fadeTexture.color = new Color(0, 0, 0, Mathf.SmoothStep(fadeTexture.color.a, 0, 0.1f));
    }

    //フェードアウト処理
    public void FadeOut() {
        fadeTexture.color = new Color(0, 0, 0, Mathf.SmoothStep(fadeTexture.color.a, 1, 0.1f));
    }

    //シールドゲージのチャージ処理
    public void ChargeShieldGauge() {
        shieldGauge.fillAmount += 0.02f;
    }

    //シールドゲージの発動処理
    public void DecreaseShieldGauge(float d) {
        if (shieldGauge.fillAmount > d) {
            shieldGauge.fillAmount -= d;
        }

        if(shieldGauge.fillAmount <= d) {
            shieldGauge.fillAmount = d;
        }
    }
	
	void Start () {
        _uiManager = this;
	}
	
	void Update () {
		
	}
}
