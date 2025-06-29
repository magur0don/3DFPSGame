using UnityEngine;

public class PlayerHealth : Health
{
    /// <summary>
    /// override: virtual修飾子がついたメソッドを上書きする
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage)
    {
        // 親クラスのTakeDameの処理をまず行う
        base.TakeDamage(damage);

        if (IsDead())
        {
            Debug.Log("プレイヤーの死亡");
            // Scene上からFPSGameManagerを見つけてGameOverメソッドを発火させる
            FindAnyObjectByType<FPSGameManager>().GameOver();
        }
    }
}
