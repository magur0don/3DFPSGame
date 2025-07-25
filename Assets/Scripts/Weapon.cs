using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData weaponData;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private GameObject bulletPrefabOverride;

    /// <summary>
    /// 現在の残弾数
    /// </summary>
    private int currentAmmo;

    /// <summary>
    /// 最後の発射された時間
    /// </summary>
    private float lastFireTime;

    /// <summary>
    /// ローディング中ですか
    /// </summary>
    private bool isReloading = false;

    public int GetCurrentAmmo
    {
        get { return currentAmmo; }
    }

    public int GetMaxAmmo
    {
        get { return weaponData.MaxAmmo; }
    }

    /// <summary>
    /// 総所持弾数
    /// </summary>
    private int totalAmmo;

    public int GetTotalAmmo
    {
        get { return totalAmmo; }
    }

    /// <summary>
    /// gameObjectがActiveになったときに発火します
    /// </summary>
    private void OnEnable()
    {
        currentAmmo = weaponData.MaxAmmo;
        // 総所持弾数を初期化
        totalAmmo = weaponData.MaxTotalAmmo;
    }

    public void Fire()
    {
        if (isReloading)
        {
            return;
        }
        if (Time.time - lastFireTime < 1f / weaponData.FireRate)
        {
            return;
        }
        if (currentAmmo < 0)
        {
            return;
        }
        lastFireTime = Time.time;
        // 現在の残弾数をデクリメント(-1)します
        currentAmmo--;
        // 弾丸を生成します
        GameObject bullet = Instantiate(
            bulletPrefabOverride != null ?
            bulletPrefabOverride : weaponData.BulletPrefab,
            shootPoint.position, shootPoint.rotation
            );
        bullet.GetComponent<Rigidbody>().linearVelocity
            = shootPoint.forward * 30f;
    }

    public void Reload()
    {
        // ローディング中 or 残弾数が最大だった場合
        if (isReloading || currentAmmo == weaponData.MaxAmmo)
        {
            return;
        }
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(weaponData.ReloadTime);

        var needed = weaponData.MaxAmmo - currentAmmo;
        var taken = Mathf.Min(needed, totalAmmo);
        currentAmmo += taken;
        totalAmmo -= taken;
        isReloading = false;
    }

    /// <summary>
    /// 外部から弾数を補充する
    /// </summary>
    /// <param name="ammo">補充される弾数</param>
    public void AddTotalAmmo(int ammo)
    {
        // Mathf.Minは二つ以上の値から最小値を返してくれます
        totalAmmo = Mathf.Min(totalAmmo+ammo,weaponData.MaxTotalAmmo);
    }



}
