using UnityEngine;

public class PlayerHealth : Health
{
    /// <summary>
    /// override: virtual�C���q���������\�b�h���㏑������
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage)
    {
        // �e�N���X��TakeDame�̏������܂��s��
        base.TakeDamage(damage);

        if (IsDead())
        {
            Debug.Log("�v���C���[�̎��S");
            // Scene�ォ��FPSGameManager��������GameOver���\�b�h�𔭉΂�����
            FindAnyObjectByType<FPSGameManager>().GameOver();
        }
    }
}
