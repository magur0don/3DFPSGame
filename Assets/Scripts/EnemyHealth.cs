using UnityEngine;

public class EnemyHealth : Health
{
    /// <summary>
    /// 死亡時のエフェクト
    /// </summary>
    public ParticleSystem DeathEffect;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (IsDead())
        {
            // 死亡時のエフェクトがnullじゃなかったら(設定されていたら)
            if (DeathEffect != null)
            {
                // EnemyHealthが追加された敵の場所に
                // 死亡時のエフェクトを産む
                Instantiate(DeathEffect.gameObject, this.transform);
            }
            // 自分を消す
            Destroy(this.gameObject);
        }
    }


}
