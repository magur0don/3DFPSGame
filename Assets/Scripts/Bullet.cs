using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// �q�b�g���̃G�t�F�N�g(�ΉԂƂ���)
    /// </summary>
    [SerializeField]
    private GameObject hitEffectPrefab;
    /// <summary>
    /// �e�̎���
    /// </summary>
    [SerializeField]
    private float lifeTime = 5f;

    /// <summary>
    /// �e�������������̉����t�@�C��
    /// </summary>
    [SerializeField]
    private AudioClip hitAudioClip;

    /// <summary>
    /// �e�ۂ̑��x
    /// </summary>
    [SerializeField]
    private float bulletSpeed = 1f;

    /// <summary>
    /// �O������e�̑��x���擾����v���p�e�B
    /// </summary>
    public float GetBulletSpeed
    {
        get { return bulletSpeed; }
    }

    private void Start()
    {
        // ��莞�Ԍ�Ɏ����폜
        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �Փˈʒu�ƕ\�ʂ̖@���x�N�g��
        ContactPoint contact = collision.contacts[0];
        // �q�b�g�G�t�F�N�g�𐶐�
        if (hitEffectPrefab != null)
        {
            // hitEffectPrefab���Փˈʒu�̕\�ʂɁA�\�ʂ̖@���x�N�g�������Ɍ������Đ���
            Instantiate(hitEffectPrefab,
                contact.point,
                Quaternion.LookRotation(contact.normal)
                );
        }
        // �����t�@�C�����Đ��A�����ō폜�����
        AudioSource.PlayClipAtPoint(hitAudioClip, contact.point);

        // �����������肩��Health�N���X���擾���ɍs���āA�����out health�ɕԂ�
        if (collision.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
