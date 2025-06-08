using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

using TMPro;

public class PlayerShooting : MonoBehaviour
{
    /// <summary>
    /// 弾丸のプレハブ
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// 弾丸の速度
    /// </summary>
    private float bulletSpeed = 20f;
    /// <summary>
    /// 弾丸の発射位置
    /// </summary>
    [SerializeField]
    private Transform shootPoint;

    /// <summary>
    /// 撃った弾丸数
    /// </summary>
    private int bulletsFired = 0;
    /// <summary>
    /// 最大弾数
    /// </summary>
    private int maxBullets = 10;
    /// <summary>
    /// 打ち切った場合の制限
    /// </summary>
    private float cooldownDuration = 10f;

    /// <summary>
    /// クールダウンに使う判定
    /// </summary>
    private bool isCooldown = false;

    [SerializeField]
    private TextMeshProUGUI bulletText;

    private void Start()
    {
        bulletText.text = $"{maxBullets}/{maxBullets}";
    }

    public void OnAttack(InputValue value)
    {
        // 押されていなかったり、クールダウン中だったら何もしない
        if (!value.isPressed || isCooldown)
        {
            return;
        }

        // 撃った弾丸数が最大数より増えていると
        if (bulletsFired >= maxBullets)
        {
            StartCoroutine(StartCooldown());
            return;
        }

        GameObject bullet = Instantiate(
            bulletPrefab,           // 生成するGameOpbect
            shootPoint.position, // 生成する位置
            shootPoint.rotation // 生成する角度
            );

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        // 力を加える
        bulletRigidbody.AddForce(
            shootPoint.forward * bulletSpeed,
            ForceMode.Impulse);
        // 撃った弾丸数を増やす
        bulletsFired++;
        bulletText.text = $"{maxBullets-bulletsFired}/{maxBullets}";
    }


    private IEnumerator StartCooldown()
    {
        isCooldown = true;
        Debug.Log("弾切れ！クールダウン中...");
        yield return new WaitForSeconds(cooldownDuration);
        bulletsFired = 0;
        isCooldown = false;
        Debug.Log("再装填完了！");
        bulletText.text = $"{maxBullets}/{maxBullets}";
    }

}
