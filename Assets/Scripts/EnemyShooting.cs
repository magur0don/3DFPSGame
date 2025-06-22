using System.Collections;
using UnityEngine;
/// <summary>
/// 敵の射撃処理
/// </summary>
public class EnemyShooting : Shooting
{
    /// <summary>
    /// 弾を撃つ間隔
    /// </summary>
    [SerializeField]
    private float shootInterval = 2f;

    /// <summary>
    /// プレイヤーが入ってくる想定
    /// </summary>
    private Transform target;

    /// <summary>
    /// 前回射撃した時間
    /// </summary>
    private float lastShootTime = -999f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // ターゲットがゲーム開始時にいなかったら何もしない
        if (target == null)
        {
            return;
        }
        // クールダウン中だったら何もしない
        if (isCooldown)
        {
            return;
        }
        // 最後に射撃された時間から現在時間を引いた値が射撃間隔より長かったら
        if (Time.time - lastShootTime > shootInterval)
        {
            // 射撃を行う
            base.Shoot(shootPoint.forward);
            lastShootTime = Time.time;
        }
    }


}
