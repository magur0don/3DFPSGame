using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    /// <summary>
    /// Weapon�̃f�[�^
    /// </summary>
    [SerializeField]
    private WeaponData[] weapons;

    /// <summary>
    /// Weapon�̐e�I�u�W�F�N�g��Transform(�ʒu)
    /// </summary>
    [SerializeField]
    private Transform weaponHolder;

    /// <summary>
    /// Game��Ő�������镐��̃Q�[���I�u�W�F�N�g
    /// </summary>
    private GameObject[] weaponInstances;

    private int currentIndex = 0;

    void Start()
    {
        // ����f�[�^�̐������AGameObject�̔z����쐬���đ������
        weaponInstances = new GameObject[weapons.Length];

        for (int i = 0; i < weapons.Length; i++)
        {
            // �����prefab��Scene��ɐ�������
            GameObject weaponObj = Instantiate(
                weapons[i].WeaponPrefab, weaponHolder);
            // ������\���ɂ��܂�
            weaponObj.SetActive(false);

            weaponInstances[i] = weaponObj;
        }
        weaponInstances[currentIndex].SetActive(true);
    }

    public void Switch(int direction)
    {
        weaponInstances[currentIndex].SetActive(false);
        // %�͗]����v�Z���Ă���܂�
        // �Ⴆ�Ε��킪2��ނ�2�Ԗڂ̕�����g���Ă����ꍇ�B
        // (1 + 1 + 2) % 2 = 0�i�ŏ��ɖ߂�j
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
