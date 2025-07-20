using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    // ‡@‰ñ•œ‚·‚é’e”‚ÌintŒ^‚Ì•Ï”
    [SerializeField]
    private int addAmmo = 10;
    // ‡AOnTriggerEnter‚ÅPlayer‚ª“–‚½‚Á‚Ä‚«‚½‚ç
    // Player‚ÌŠK‘w‰º‚É‚ ‚éWeaponSwitcher‚ğæ“¾‚µ‚ÄA
    // weapon.AddTotalAmmo(‡@‚Åì‚Á‚½•Ï”)‚ÅÅ‘å’e”‚ğ’Ç‰Á
    // ‚±‚ÌAmmoBoxƒRƒ“ƒ|[ƒlƒ“ƒg‚ª’Ç‰Á‚³‚ê‚Ä‚¢‚éGameObject‚ğíœ
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
