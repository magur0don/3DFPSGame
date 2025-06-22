using UnityEngine;

public class EnemyHealth : Health
{
    /// <summary>
    /// ���S���̃G�t�F�N�g
    /// </summary>
    public ParticleSystem DeathEffect;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (IsDead())
        {
            // ���S���̃G�t�F�N�g��null����Ȃ�������(�ݒ肳��Ă�����)
            if (DeathEffect != null)
            {
                // EnemyHealth���ǉ����ꂽ�G�̏ꏊ��
                // ���S���̃G�t�F�N�g���Y��
                Instantiate(DeathEffect.gameObject, this.transform);
            }
            // ����������
            Destroy(this.gameObject);
        }
    }


}
