using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    // �@�񕜂���e����int�^�̕ϐ�
    [SerializeField]
    private int addAmmo = 10;
    // �AOnTriggerEnter��Player���������Ă�����
    // Player�̊K�w���ɂ���WeaponSwitcher���擾���āA
    // weapon.AddTotalAmmo(�@�ō�����ϐ�)�ōő�e����ǉ�
    // ����AmmoBox�R���|�[�l���g���ǉ�����Ă���GameObject���폜
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
