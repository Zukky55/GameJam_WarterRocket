using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シールドを管理するスクリプト
public class ShieldManager : MonoBehaviour {
    private static ShieldManager _shieldManager;

    public static ShieldManager GetShieldManager() {
        return _shieldManager;
    }

    //シールドの素材
    [SerializeField] public Transform shield;
    private BoxCollider2D col;
    private SpriteRenderer sprite;
    
    //シールド発動用の真偽値
    [SerializeField] public bool sFlag;

    //シールドのゲージ用
    private float shieldPoints;

    //シールドゲージの時間処理
    private float shieldTimer = 0;

    //シールドを表示する処理
    private void ShowShield(bool b) {
        col.enabled = b;
        sprite.enabled = b;
    }

	void Start () {
        _shieldManager = this;

        col = shield.GetComponent<BoxCollider2D>();
        sprite = shield.GetComponent<SpriteRenderer>();
        ShowShield(false);
	}
	
	void Update () {
        if (sFlag && BottleManager.GetBottleManager().gameState == BottleManager.GameState.TraverseState) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                ShowShield(true);
            }

            if (Input.GetKey(KeyCode.Space)) {
                shieldTimer += Time.deltaTime;
                if(shieldTimer >= 3f) {
                    if (BottleManager.GetBottleManager().stageState == BottleManager.StageState.Boss2State) {
                        UIManager.GetUIManager().DecreaseShieldGauge(0.2f);
                    }
                    shieldTimer = 0f;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                ShowShield(false);
                if (BottleManager.GetBottleManager().stageState == BottleManager.StageState.Boss2State) {
                        UIManager.GetUIManager().DecreaseShieldGauge(0.2f);
                    }
                shieldTimer = 0;
            }
        }else {
            if (col.enabled) {
                ShowShield(false);
            }
        }
	}
}
