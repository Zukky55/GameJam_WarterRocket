using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//次のシーンへの移行を管理・実行するスクリプト
public class NextSceneController : MonoBehaviour {

    private static NextSceneController _nextSceneController = null;
    public static NextSceneController GetNextSceneController() {
        return _nextSceneController;
    }

    //スクロール速度
	[SerializeField] private float n_scrollSpeed;
    public bool n_flag;
    private bool loadFlag;

    //次のシーンをロードする
    public void LoadNextScene() {
            int s = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(s);
            SceneManager.LoadScene(s + 1);
    }

    private void Start() {
        _nextSceneController = this;
        loadFlag = true;
    }

    void Update()
    {
        if(n_flag)
        {
            // Time.deltaTime を使って滑らかに縦にスクロールさせる
            transform.Translate(new Vector2(0.5f, 1) * Time.deltaTime * n_scrollSpeed);
        }
    }
}
