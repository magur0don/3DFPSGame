
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    /// <summary>
    /// 武器のprefab
    /// </summary>
    public GameObject WeaponPrefab;

    /// <summary>
    /// 弾丸のprefab
    /// </summary>
    public GameObject BulletPrefab;

    public float FireRate = 1f;
    /// <summary>
    /// 武器の弾の最大値
    /// </summary>
    public int MaxAmmo = 10;

    /// <summary>
    /// リロードの時間
    /// </summary>
    public float ReloadTime = 1.5f;

    /// <summary>
    /// 所持できる弾数の最大値
    /// </summary>
    public int MaxTotalAmmo = 30;
}
