using System.Collections;
using UnityEngine;
/// <summary>
/// �G�̎ˌ�����
/// </summary>
public class EnemyShooting : Shooting
{
    /// <summary>
    /// �e�����Ԋu
    /// </summary>
    [SerializeField]
    private float shootInterval = 2f;

    /// <summary>
    /// �v���C���[�������Ă���z��
    /// </summary>
    private Transform target;

    /// <summary>
    /// �O��ˌ���������
    /// </summary>
    private float lastShootTime = -999f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // �^�[�Q�b�g���Q�[���J�n���ɂ��Ȃ������牽�����Ȃ�
        if (target == null)
        {
            return;
        }
        // �N�[���_�E�����������牽�����Ȃ�
        if (isCooldown)
        {
            return;
        }
        // �Ō�Ɏˌ����ꂽ���Ԃ��猻�ݎ��Ԃ��������l���ˌ��Ԋu��蒷��������
        if (Time.time - lastShootTime > shootInterval)
        {
            // �ˌ����s��
            base.Shoot(shootPoint.forward);
            lastShootTime = Time.time;
        }
    }


}
