using UnityEngine;
using TMPro;

public class PlayerAmmoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI bulletText;

    [SerializeField]
    private WeaponSwitcher weaponSwitcher;


   void Update()
    {
        bulletText.text = $"{weaponSwitcher.GetCurrentWeapon.GetCurrentAmmo}/{weaponSwitcher.GetCurrentWeapon.GetMaxAmmo}";
    }
}
