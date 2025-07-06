using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    /// <summary>
    /// Weaponのデータ
    /// </summary>
    [SerializeField]
    private WeaponData[] weapons;

    /// <summary>
    /// Weaponの親オブジェクトのTransform(位置)
    /// </summary>
    [SerializeField]
    private Transform weaponHolder;

    /// <summary>
    /// Game上で生成される武器のゲームオブジェクト
    /// </summary>
    private GameObject[] weaponInstances;

    private int currentIndex = 0;

    void Start()
    {
        // 武器データの数だけ、GameObjectの配列を作成して代入する
        weaponInstances = new GameObject[weapons.Length];

        for (int i = 0; i < weapons.Length; i++)
        {
            // 武器のprefabをScene上に生成する
            GameObject weaponObj = Instantiate(
                weapons[i].WeaponPrefab, weaponHolder);
            // 武器を非表示にします
            weaponObj.SetActive(false);

            weaponInstances[i] = weaponObj;
        }
        weaponInstances[currentIndex].SetActive(true);
    }

    public void Switch(int direction)
    {
        weaponInstances[currentIndex].SetActive(false);
        // %は余剰を計算してくれます
        // 例えば武器が2種類で2番目の武器を使っていた場合。
        // (1 + 1 + 2) % 2 = 0（最初に戻る）
        currentIndex =
            (currentIndex + direction + weaponInstances.Length)
            % weaponInstances.Length;
        weaponInstances[currentIndex].SetActive(true);
    }

    public Weapon GetCurrentWeapon
    {
        get
        {
            return 
                weaponInstances[currentIndex].GetComponent<Weapon>();
        }
    }

}
