using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ボトルを管理するスクリプト
public class BottleManager : MonoBehaviour {

    public enum GameState : byte
    {
        ClickState, TraverseState, OverState, ClearState, CountState
    }

    public enum StageState : byte
    {
        LaunchState, Boss1State, Boss2State, Boss3State
    }

    //ゲームの状態を表す. クリック時はClickState, 移動中はTraverseState, ClickStateで失敗したらOverState, 最後の敵を倒したらClearState
    private GameState gameState;

    //ステージの進行を表す. LaunchStateは発射前, Boss1Stateは最初のボスの前, Boss2Stateは二体目のボスの前, Boss3Stateは三体目のボスの前
    private StageState stageState;

    //ステージの各場面における制限時間
    private const float launchTime = 10f;
    private const float boss1Time = 3f;
    private const float boss2Time = 4f;
    private const float boss3Time = 5f;

    //ステージの各場面におけるクリック数
    private const short launchClick = 50;
    private const short boss1Click = 20;
    private const short boss2Click = 30;
    private const short boss3Click = 50;

    private short clickNum;
    private float clickTimer;
    //クリック数の情報を格納する
    private short limitClicks;

    //連打が開始されたか否かの真偽値
    private bool clickFlag;

    //背景のスクリプトとゲームオブジェクト
    private GameObject m_background;
    private Background m_rb;

    //クリック処理
    private void Click() {
        clickTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) {
            clickNum += 1;

        }

        //UIをアップデートする
        UIManager.GetUIManager().UpdateTimer(clickTimer);

        if(clickTimer <= 0) {
            //クリック数がその場面で必要な回数を超えているのかをチェックする
            CheckClicks();
        }
    }

    //クリック回数をチェックする
    private void CheckClicks() {
        if(clickNum >= limitClicks) {
            //クリック回数が必要回数を超えた処理
            gameState = GameState.TraverseState;
            SetUI(gameState);
            SetNewParameters(stageState);
            Debug.Log("Clear");
            //UIManager.GetUIManager().ClearGauge();
            clickFlag = false;
        }else {
            //クリック回数が必要回数を超えなかった処理
            gameState = GameState.OverState;
            SetUI(gameState);
            Debug.Log("GameOver");
        }
    }

    //新しい制限時間とクリック数を格納する
    private void SetNewParameters(StageState state) {
        switch (state){
            case StageState.LaunchState:
                clickTimer = boss1Time;
                limitClicks = boss1Click;
                stageState = StageState.Boss1State;
                break;
            case StageState.Boss1State:
                clickTimer = boss2Time;
                limitClicks = boss2Click;
                stageState = StageState.Boss2State;
                break;
            case StageState.Boss2State:
                clickTimer = boss3Time;
                limitClicks = boss3Click;
                stageState = StageState.Boss3State;
                break;
            case StageState.Boss3State:
                //ゲームクリア処理へ移行
                gameState = GameState.ClearState;
                break;
            default:
                Debug.LogAssertion("StageState is not set properly");
                break;
        }
    }

    //GameStateに応じてUIを変更する
    public void SetUI(GameState state) {
        switch (state){
            case GameState.ClickState:
                UIManager.GetUIManager().SetTimer(true);
                UIManager.GetUIManager().ClickUI(true);
                break;
            case GameState.TraverseState:
                UIManager.GetUIManager().SetTimer(false);
                UIManager.GetUIManager().ClickUI(false);
                break;
            case GameState.OverState:
                UIManager.GetUIManager().ClickUI(false);
                break;
            case GameState.ClearState:
                UIManager.GetUIManager().SetTimer(false);
                UIManager.GetUIManager().ClickUI(false);
                break;
            default:
                Debug.Log("Game State is not set properly");
                break;
            }
    }

    //ゲームクリア処理
    private void GameClear() {
        
    }

    //ゲームオーバー処理
    private void GameOver() {

    }

	void Start () {
        FadeManager.FadeIn();
        gameState = GameState.TraverseState;
        stageState = StageState.LaunchState;
        clickTimer = launchTime;
        limitClicks = launchClick;
        clickFlag = false;
        SetUI(gameState);
        //UIManager.GetUIManager().UpdateTimer(clickTimer);
        //Background
        m_background = GameObject.Find("Background");
        m_rb = m_background.GetComponent<Background>();
	}
	
	void Update () {
        if(gameState == GameState.ClickState) {
            //クリック時の処理
            if (clickFlag) {
                Click();
            }
            else {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    clickFlag = true;
                }
            }
        }else if(gameState == GameState.TraverseState) {
            //移動中の処理
            m_rb.m_flag = true;
        }else if(gameState == GameState.OverState) {
            //ゲームオーバー時の処理
            GameOver();
        }else {
            //ゲームクリア時の処理
            GameClear();
        }

		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_rb.m_flag = false;
        gameState = GameState.ClickState;
        SetUI(gameState);
        Debug.Log("On Trigger");
    }
}
