using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    // �@回復する弾数のint型の変数
    [SerializeField]
    private int addAmmo = 10;
    // �AOnTriggerEnterでPlayerが当たってきたら
    // Playerの階層下にあるWeaponSwitcherを取得して、
    // weapon.AddTotalAmmo(�@で作った変数)で最大弾数を追加
    // このAmmoBoxコンポーネントが追加されているGameObjectを削除
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var weaponSwitcher = 
                other.GetComponentInChildren<WeaponSwitcher>();
            weaponSwitcher.GetCurrentWeapon.AddTotalAmmo(addAmmo);
            Destroy(this.gameObject);
        }
    }


}
