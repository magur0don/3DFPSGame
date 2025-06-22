using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// 射撃のベースクラス
/// </summary>
public class Shooting : MonoBehaviour
{
    /// <summary>
    /// 弾丸のプレハブ
    /// </summary>
    public GameObject BulletPrefab;

    /// <summary>
    /// 弾丸の発射位置
    /// </summary>
    [SerializeField]
    protected Transform shootPoint;

    /// <summary>
    /// 撃った弾丸数
    /// </summary>
    protected int bulletsFired = 0;
    /// <summary>
    /// 最大弾数
    /// </summary>
    protected int maxBullets = 10;

    /// <summary>
    /// 打ち切った場合の制限
    /// </summary>
    protected float cooldownDuration = 10f;

    /// <summary>
    /// クールダウンに使う判定
    /// </summary>
    protected bool isCooldown = false;

    /// <summary>
    /// 射撃処理
    /// </summary>
    /// <param name="direction">撃つ方向</param>
    /// <param name="remainingText">残弾を表示する必要があれば指定:nullで代入</param>
    protected virtual void Shoot(Vector3 direction,TextMeshProUGUI remainingText = null)
    {
        if (bulletsFired >= maxBullets)
        {
            StartCoroutine(StartCooldown(remainingText));
            return;
        }
        GameObject bullet = Instantiate(BulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        float speed = bullet.GetComponent<Bullet>().GetBulletSpeed;
        bulletRb.AddForce(direction * speed, ForceMode.Impulse);
        bulletsFired++;
    }

    protected IEnumerator StartCooldown(TextMeshProUGUI remainingText = null)
    {
        isCooldown = true;
        Debug.Log("弾切れ！クールダウン中...");
        yield return new WaitForSeconds(cooldownDuration);
        bulletsFired = 0;
        isCooldown = false;
        Debug.Log("再装填完了！");
        if (remainingText != null)
        {
            remainingText.text =$"{ maxBullets}/{ maxBullets }";
        }
    }
}
