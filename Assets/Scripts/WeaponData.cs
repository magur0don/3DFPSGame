
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    /// <summary>
    /// •Ší‚Ìprefab
    /// </summary>
    public GameObject WeaponPrefab;

    /// <summary>
    /// ’eŠÛ‚Ìprefab
    /// </summary>
    public GameObject BulletPrefab;

    public float FireRate = 1f;
    /// <summary>
    /// •Ší‚Ì’e‚ÌÅ‘å’l
    /// </summary>
    public int MaxAmmo = 10;

    /// <summary>
    /// ƒŠƒ[ƒh‚ÌŠÔ
    /// </summary>
    public float ReloadTime = 1.5f;

    /// <summary>
    /// Š‚Å‚«‚é’e”‚ÌÅ‘å’l
    /// </summary>
    public int MaxTotalAmmo = 30;
}
