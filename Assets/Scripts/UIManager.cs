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

    [SerializeField] Text timerText;
    [SerializeField] Text clickText;


    public void UpdateTimer(float t) {
        if(t >= 0)
        timerText.text = "Time: " + t.ToString("00.00");
        else {
            timerText.text = "Time: 00.00";
        }
    }

    public void SetTimer(bool show) {
        timerText.enabled = show;
    }

    public void ClickUI(bool show) {
        clickText.enabled = show;
    }
	
	void Start () {
        _uiManager = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
