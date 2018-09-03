using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ステージ2のレーザービームを管理するスクリプト
public class LaserBeamManager : MonoBehaviour {

    private static LaserBeamManager _laserBeamManager;
    public static LaserBeamManager GetLaserBeamManager() {
        return _laserBeamManager;
    }


    private LineRenderer[] laser = new LineRenderer[2];
    //プレイヤーに攻撃するレーザー. 一定周期で撃ってくる
    private LineRenderer attackLaser = new LineRenderer();
    private Ray2D attackRay = new Ray2D();

    [SerializeField] private GameObject laserPrefab;

    [SerializeField] private float laser1ShootTime;
    [SerializeField] private float laser2ShootTime;
    [SerializeField] private float attackLaserShootTime;

    private float laser1Timer;
    private float laser2Timer;
    private float attackTimer = 0f;

    public LayerMask hitMask;

    [SerializeField] public bool lFlag;

    //レーザービームの方向を設定する
    private void SetLaserPos(int i, float min, float max) {
        laser[i].SetPosition(0, new Vector3(0, Random.Range(min, max), -1));
    }

	void Start () {
        _laserBeamManager = this;

        for (int i = 0; i < laser.Length; i++) {
            GameObject l = Instantiate(laserPrefab, this.gameObject.transform, true);
            laser[i] = l.GetComponent<LineRenderer>();
            laser[i].startWidth = 0f;
            }
        SetLaserPos(0, 2.7f, 5.5f);
        SetLaserPos(1, -4, 1);

        GameObject la = Instantiate(laserPrefab, this.gameObject.transform, true);
        attackLaser = la.GetComponent<LineRenderer>();
        attackLaser.startWidth = 0f;
        attackLaser.SetPosition(0, new Vector3(Random.Range(17, 19), 20, -1));
        attackLaser.SetPosition(1, new Vector3(0, 1, -1));
        attackRay.origin = attackLaser.GetPosition(0);
        attackRay.direction = attackLaser.GetPosition(1) - attackLaser.GetPosition(0);
	}
	
	void Update () {
        if (lFlag) {
            laser1Timer += Time.deltaTime;
            laser2Timer += Time.deltaTime;
            attackTimer += Time.deltaTime;

            //レーザー1の処理
            if (laser1Timer >= 1f && laser1Timer < 1.5f) {
                laser[0].startWidth = 0.1f;
            }
            else if (laser1Timer >= 1.5f) {
                laser[0].startWidth += 0.01f;
            }

            if (laser1Timer >= laser1ShootTime) {
                laser[0].startWidth = 0f;
                laser1Timer = 0f;
                SetLaserPos(0, 2.7f, 5.5f);
            }

            //レーザー2の処理
            if (laser2Timer >= 1f && laser2Timer < 1.5f) {
                laser[1].startWidth = 0.1f;
            }
            else if (laser2Timer >= 1.5f) {
                laser[1].startWidth += 0.01f;
            }

            if (laser2Timer >= laser2ShootTime) {
                laser[1].startWidth = 0f;
                laser2Timer = 0f;
                SetLaserPos(1, -4, 1);
            }

            //攻撃レーザーの処理
            if(attackTimer >= 1f && attackTimer < 2f) {
                attackLaser.startWidth = 0.1f;
            }else if(attackTimer >= 2f) {
                attackLaser.startWidth += 0.01f;
                RaycastHit2D hit = Physics2D.Raycast(attackRay.origin, attackRay.direction, 30f, hitMask);
                //プレイヤーと当たった場合の処理
                if (hit.collider.gameObject == BottleManager.GetBottleManager().gameObject) {
                    Debug.Log("Hit Player");
                    //シールドと当たった時の処理
                }
                else if (hit.collider.gameObject == ShieldManager.GetShieldManager().shield.gameObject) {
                    attackLaser.SetPosition(1, hit.point);
                    Debug.Log("Hit Shield");
                }
            }

            if(attackTimer >= attackLaserShootTime) {
                attackLaser.startWidth = 0f;
                attackTimer = 0f;
                attackLaser.SetPosition(0, new Vector3(Random.Range(17, 19), 20, -1));
                attackLaser.SetPosition(1, new Vector3(0, 1, -1));
                attackRay.origin = attackLaser.GetPosition(0);
                attackRay.direction = attackLaser.GetPosition(1) - attackLaser.GetPosition(0);
            }
        }
        else {
            for(int i = 0; i < laser.Length; i++) {
                laser[i].startWidth = 0f;
            }

            attackLaser.startWidth = 0f;
        }
	}
}
