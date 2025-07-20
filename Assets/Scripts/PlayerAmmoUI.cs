using UnityEngine;
using TMPro;

public class PlayerAmmoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI bulletText;
    [SerializeField]
    private TextMeshProUGUI totalMaxAmmoText;
    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

    void Update()
    {
        bulletText.text = $"{weaponSwitcher.GetCurrentWeapon.GetCurrentAmmo}/{weaponSwitcher.GetCurrentWeapon.GetMaxAmmo}";
        totalMaxAmmoText.text = $"{weaponSwitcher.GetCurrentWeapon.GetTotalAmmo}";
    }
}
