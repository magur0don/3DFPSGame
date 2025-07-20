using UnityEngine;

public class ScoreTargetHealth : Health
{
    // �X�R�A�^�[�Q�b�g�̃X�R�A
    private int score = 10;

    private FPSGameManager fPSGameManager = null;

    protected override void Start()
    {
        base.Start();
        // �Q�[�����Ƀ����_���ɐ��܂��\��������̂ŁAScene�ɓo�ꂵ�Ă���
        // �擾����
        fPSGameManager = FindAnyObjectByType<FPSGameManager>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (IsDead())
        {
            fPSGameManager.AddScore(score);
            // ����������
            Destroy(this.gameObject);
        }
    }
}
