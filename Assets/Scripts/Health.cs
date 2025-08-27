using UnityEngine;

/// <summary>
/// 体力の管理
/// </summary>
public class Health : MonoBehaviour
{
    /// <summary>
    /// 体力の最大値
    /// </summary>
    [SerializeField]
    private int maxHealthPoint = 5;

    /// <summary>
    /// 体力の最大値を取得するプロパティ
    /// </summary>
    public int GetMaxHealthPoint
    {
        get { return maxHealthPoint; }
    }

    /// <summary>
    /// 現在の体力値
    /// </summary>
    [SerializeField]
    private int currentHealthPoint;
    
    /// <summary>
    /// 現在の体力値を取得するプロパティ
    /// </summary>
    public int GetCurrentHealthPoint
    {
        get { return currentHealthPoint; }
    }

    /// <summary>
    /// protected:自分自身と継承した子クラスからアクセスできる
    /// virtual:子クラスでメソッドの内容を上書きできる
    /// </summary>
    protected virtual void Start()
    {
        currentHealthPoint = maxHealthPoint;
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage">現在の体力値から引かれるダメージ</param>
    public virtual void TakeDamage(int damage)
    {
        currentHealthPoint -= damage;
        Debug.Log(currentHealthPoint);
    }
    /// <summary>
    /// 死亡判定
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return currentHealthPoint <= 0;
    }
}
