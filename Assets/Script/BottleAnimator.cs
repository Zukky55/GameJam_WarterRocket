using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ボトルのアニメーションを管理するスクリプト
public class BottleAnimator : MonoBehaviour {

    private static BottleAnimator _bottleAnimator = null;
    public static BottleAnimator GetBottleAnimator() {
        return _bottleAnimator;
        }


    //ロケットの炎のスプライト
    [SerializeField] public SpriteRenderer fireRenderer;

    private const float minFireSize = 0.75f;
    private const float maxFireSize = 1.5f;
    private const float minFirePosX = 2.0f;
    private const float maxFirePosX = 2.4f;

    public void ShowFire() {
        fireRenderer.gameObject.SetActive(true);
        fireRenderer.enabled = true;
    }

    //連打と共に炎が大きなる
    public void EnlargeFireSize() {
        if (fireRenderer.transform.localScale.x <= maxFireSize) {
            if (fireRenderer.transform.localScale.x <= 1) {
                fireRenderer.transform.localScale += new Vector3(0.01f, 0.01f);
            }
            else{
                fireRenderer.transform.localScale += new Vector3(0.01f, 0.01f, 0);
                
                if(fireRenderer.transform.position.x <= maxFirePosX)
                    fireRenderer.transform.localPosition += new Vector3(0.01f, 0, 0);
            }
        }
    }

    //墜落時などで炎が小さくなる
    public void ShrinkFireSize() {
        if (fireRenderer.transform.localScale.x >= minFireSize) {
            if (fireRenderer.transform.localScale.x >= 1) {
                fireRenderer.transform.localScale -= new Vector3(0.01f, 0.01f);
                if(fireRenderer.transform.position.x >= minFirePosX)
                    fireRenderer.transform.localPosition -= new Vector3(0.01f, 0, 0);
            }
            else{
                fireRenderer.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            }
        }
        else {
            fireRenderer.enabled = false;
        }
    }

	void Start () {
        _bottleAnimator = this;
        if (BottleManager.GetBottleManager().stageState == BottleManager.StageState.LaunchState) {
            fireRenderer.transform.localScale = new Vector3(0.75f, 0.75f, 1);
            fireRenderer.enabled = false;
            fireRenderer.gameObject.SetActive(false);
        }
        else {
            fireRenderer.transform.localScale = Vector3.one;
            fireRenderer.enabled = true;
        }
	}
}
