
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    /// <summary>
    /// �����prefab
    /// </summary>
    public GameObject WeaponPrefab;

    /// <summary>
    /// �e�ۂ�prefab
    /// </summary>
    public GameObject BulletPrefab;

    public float FireRate = 1f;
    /// <summary>
    /// ����̒e�̍ő�l
    /// </summary>
    public int MaxAmmo = 10;

    /// <summary>
    /// �����[�h�̎���
    /// </summary>
    public float ReloadTime = 1.5f;

    /// <summary>
    /// �����ł���e���̍ő�l
    /// </summary>
    public int MaxTotalAmmo = 30;
}
