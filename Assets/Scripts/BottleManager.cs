using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ボトルを管理するスクリプト
public class BottleManager : MonoBehaviour {

    private static BottleManager _bottleManager = null;
    public static BottleManager GetBottleManager() {
        return _bottleManager;
    }

    public enum GameState : byte
    {
        ClickState, TraverseState, OverState, ClearState, NextSceneState
    }

    public enum StageState : byte
    {
        LaunchState, Boss1State, Boss2State, Boss3State, ClearState
    }

    //ゲームの状態を表す. クリック時はClickState, 移動中はTraverseState, ClickStateで失敗したらOverState, 最後の敵を倒したらClearState
    [SerializeField] public GameState gameState;

    //ステージの進行を表す. LaunchStateは発射前, Boss1Stateは最初のボスの前, Boss2Stateは二体目のボスの前, Boss3Stateは三体目のボスの前
    [SerializeField] public StageState stageState;

    //ステージの各場面における制限時間
    private const float launchTime = 10f;
    private const float boss1Time = 3f;
    private const float boss2Time = 4f;
    private const float boss3Time = 5f;

    //ステージの各場面におけるクリック数
    private const float launchClick = 50;
    private const float boss1Click = 20;
    private const float boss2Click = 30;
    private const float boss3Click = 50;

    public float clickNum;
    private float clickTimer;
    //クリック数の情報を格納する
    private float limitClicks;

    //レーザーを受けた時のダメージ量
    public float laserDamage;

    //連打が開始されたか否かの真偽値
    private bool clickFlag;

    //フェードイン用の真偽値
    private bool fadeIn;
    private float fadeTimer;

    //ペットボトルのTransform
    private Transform bottle;
    [SerializeField] private float rotateSpeed;

    //背景のスクリプトとゲームオブジェクト
    [SerializeField] GameObject m_background;
    Background m_rb;

    //敵のTransform
    [SerializeField] private Transform enemyBoss;

    //クリック処理
    private void Click() {
        clickTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) {
            clickNum += 1;
            UIManager.GetUIManager().ChargeShieldGauge();
            if(clickNum >= limitClicks / 3 && !BottleAnimator.GetBottleAnimator().fireRenderer.gameObject.activeSelf) {
                BottleAnimator.GetBottleAnimator().ShowFire();
            }
            if(clickNum >= limitClicks / 3) {
                BottleAnimator.GetBottleAnimator().EnlargeFireSize();
            }
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
            stageState += 1;
            SetParameters(stageState);
            SetUI(gameState);

            //リザルト画面用にクリック回数を保存するならここでPlayerPrefsを呼び出す. その後リセット
            ResetClickNum();

            //次のシーンへ移行する場合の処理
            if(stageState == StageState.Boss2State || stageState == StageState.Boss3State) {
                gameState = GameState.NextSceneState;
                NextSceneController.GetNextSceneController().n_flag = true;
            }

            clickFlag = false;
        }else {
            //クリック回数が必要回数を超えなかった処理
            gameState = GameState.OverState;
            SetUI(gameState);
            Debug.Log("GameOver");
        }
    }

    //新しい制限時間とクリック数を格納する
    private void SetParameters(StageState state) {
        switch (state){
            case StageState.LaunchState:
                clickTimer = launchTime;
                limitClicks = launchClick;
                UIManager.GetUIManager().ChangeClickUI(true);
                gameState = GameState.ClickState;
                break;
            case StageState.Boss1State:
                clickTimer = boss1Time;
                limitClicks = boss1Click;
                UIManager.GetUIManager().ChangeClickUI(false);
                gameState = GameState.TraverseState;
                break;
            case StageState.Boss2State:
                clickTimer = boss2Time;
                limitClicks = boss2Click;
                gameState = GameState.TraverseState;
                break;
            case StageState.Boss3State:
                clickTimer = boss3Time;
                limitClicks = boss3Click;
                gameState = GameState.TraverseState;
                break;
            case StageState.ClearState:
                //クリア時の処理
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
                UIManager.GetUIManager().UpdateTimer(clickTimer);
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
            case GameState.NextSceneState:
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
        Debug.Log("Game Clear!");
    }

    //ゲームオーバー処理
    private void GameOver() {
        float dis = Vector2.Distance(Camera.main.transform.position, gameObject.transform.position);
        
        if (stageState != StageState.LaunchState) {
            gameObject.transform.Translate(new Vector2(1, 0.5f) * Time.deltaTime * 2.5f);
            bottle.transform.Rotate(Vector3.back * Time.deltaTime * rotateSpeed);

            if(dis >= 10) {
                UIManager.GetUIManager().SetTimer(false);
                UIManager.GetUIManager().FadeOut();
            }
            if(dis >= 15) {
                //ゲームオーバーシーン演出

            }
        }
        else {
            fadeTimer += Time.deltaTime;
            if (fadeTimer >= 3) {
                UIManager.GetUIManager().SetTimer(false);
                UIManager.GetUIManager().FadeOut();
            }

            //ゲームオーバー演出
        }
        BottleAnimator.GetBottleAnimator().ShrinkFireSize();
        
    }

    //クリック数をリセットする
    private void ResetClickNum() {
        int num = PlayerPrefs.GetInt("ClickNums", 0);
        Debug.Log(num);
    }

    //クリック数を減らす
    public void DecreaseClickNum() {
        clickNum -= laserDamage;
    }

	void Start () {
        _bottleManager = this;
        clickFlag = false;
        SetParameters(stageState);
        SetUI(gameState);

        fadeTimer = 0;

        bottle = gameObject.transform.GetChild(0);

        if(stageState == StageState.LaunchState) {
            UIManager.GetUIManager().SetFadeAlpha(0);
            fadeIn = false;
        }
        else {
            //フェードインを行う
            UIManager.GetUIManager().SetFadeAlpha(1);
            fadeIn = true;
        }

        //Background
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
                    
                    clickNum = 0;
                }
            }
        }else if(gameState == GameState.TraverseState) {
            //移動中の処理
            m_rb.m_flag = true;

            //フェードインメソッドを呼び出す
            if (fadeIn) {
                UIManager.GetUIManager().FadeIn();
            }

            //敵との距離が近くなると、ビームが消える
            float dis = Vector2.Distance(enemyBoss.position, gameObject.transform.position);
            if(dis <= 5) {
                LaserBeamManager.GetLaserBeamManager().lFlag = false;
            }

            if(stageState != StageState.Boss1State)UIManager.GetUIManager().ShowShieldText(true);
        }else if(gameState == GameState.OverState) {
            //ゲームオーバー時の処理
            GameOver();
        }else if(gameState == GameState.ClearState) {
            //ゲームクリア時の処理
            GameClear();
        }
        else {
            //シーン移行時の処理
            m_rb.m_flag = true;

            float dis = Vector2.Distance(Camera.main.transform.position, gameObject.transform.position);
            if(dis >= 10) {
                UIManager.GetUIManager().FadeOut();
            }
            if(dis >= 15) {
                NextSceneController.GetNextSceneController().LoadNextScene();
            }
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            clickNum = 50;
            clickTimer = 1;
        }
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_rb.m_flag = false;
        gameState = GameState.ClickState;
        SetUI(gameState);
        if(stageState != StageState.Boss1State)UIManager.GetUIManager().ShowShieldText(false);
    }
}
