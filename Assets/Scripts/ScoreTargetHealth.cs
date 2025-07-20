using UnityEngine;

public class ScoreTargetHealth : Health
{
    // スコアターゲットのスコア
    private int score = 10;

    private FPSGameManager fPSGameManager = null;

    protected override void Start()
    {
        base.Start();
        // ゲーム中にランダムに生まれる可能性があるので、Sceneに登場してから
        // 取得する
        fPSGameManager = FindAnyObjectByType<FPSGameManager>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (IsDead())
        {
            fPSGameManager.AddScore(score);
            // 自分を消す
            Destroy(this.gameObject);
        }
    }
}
