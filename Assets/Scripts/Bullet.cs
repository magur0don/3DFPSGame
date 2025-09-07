using UnityEngine;
using Unity.Netcode;

/// <summary>
/// ���[�J�������̒e�ہB������/���̓��[�J���ōĐ����A
/// ���������� ServerRpc �ŃT�[�o�Ƀ_���[�W�m����˗�����B
/// �����̃q�b�g�G�t�F�N�g/�T�E���h/����/���x�v���p�e�B���ێ��B
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    // ===== �����t�B�[���h =====
    /// <summary>�q�b�g���̃G�t�F�N�g(�ΉԂƂ���)</summary>
    [SerializeField] private GameObject hitEffectPrefab;
    /// <summary>�e�̎����i�b�j</summary>
    [SerializeField] private float lifeTime = 5f;
    /// <summary>�q�b�g���ɖ炷��</summary>
    [SerializeField] private AudioClip hitAudioClip;
    /// <summary>�e�ۂ̑��x�i�O���Q�Ɨp�j</summary>
    [SerializeField] private float bulletSpeed = 1f;

    /// <summary>�O������e�̑��x���擾����v���p�e�B�i�����d�l���ێ��j</summary>
    public float GetBulletSpeed => bulletSpeed;

    // ===== �ǉ��t�B�[���h�i�I�����C���p�j=====
    /// <summary>���̒e���������v���C���[�� ClientId�i���ˎ��ɐݒ�j</summary>
    public ulong ownerClientId;
    /// <summary>��{�_���[�W</summary>
    public int baseDamage = 1;

    /// <summary>
    /// �I�����C�����ɑ��d�ɏ���������Ȃ��悤�ɂ���
    /// </summary>
    private bool hitProcessed = false;
    private void Start()
    {
        // ��莞�Ԍ�Ɏ����폜�i���[�J���j
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitProcessed)
        {
            return;   // �� ��x�ڈȍ~�͖���
        }
        hitProcessed = true;

        // --- �����ڂƉ��̓��[�J���ōĐ� ---
        var contact = collision.contacts.Length > 0 ? collision.contacts[0] : default;

        if (hitEffectPrefab != null && collision.contacts.Length > 0)
        {
            Instantiate(hitEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));
        }
        if (hitAudioClip != null && collision.contacts.Length > 0)
        {
            AudioSource.PlayClipAtPoint(hitAudioClip, contact.point);
        }

        // --- �_���[�W�̊m��̓T�[�o���Ђֈ˗� ---
        // �ENetcode�ڑ����F�������������e�̖����̂݃T�[�o�֕񍐁i���d�񍐖h�~�j
        // �E�I�t���C��/�X�^���h�A�������F�]���ǂ��� Health �ɒ��ڃ_���[�W
        bool isOnline = NetworkManager.Singleton != null && NetworkManager.Singleton.IsListening;

        if (isOnline)
        {
            // �e�������� NetworkObject�i�q�R���C�_�[�΍�j
            var no = collision.transform.GetComponentInParent<NetworkObject>();

            // ������ HitReporter�iOwner �� Player �ɕt���Ă���O��j
            var myPlayer = NetworkManager.Singleton.LocalClient?.PlayerObject;
            var reporter = myPlayer ? myPlayer.GetComponent<HitReporter>() : null;

            if (reporter != null)
            {
                // �� �������������e������������
                if (ownerClientId == NetworkManager.Singleton.LocalClientId)
                {
                    if (no != null && no.IsSpawned)
                    {
                        // �� Spawn�ς݂Ȃ���S�ɒ��Q��
                        reporter.ReportDamageServerRpc(no, baseDamage, this.transform.position);
                    }
                    else
                    {
                        // �� �f�X�|�[�����Ŗ�Spawn�Ȃ���W�t�H�[���o�b�N
                        reporter.ReportDamageByHitPointServerRpc(this.transform.position, baseDamage, 0.35f);
                    }
                }
            }
        }
        else
        {
            // �I�t���C���iNetcode���g�p�j�̏ꍇ�́A�]���ǂ��胍�[�J���Œ��ڃ_���[�W
            if (collision.gameObject.TryGetComponent<Health>(out var health))
            {
                Debug.Log("���������Ă�");
                health.TakeDamage(baseDamage);
            }
        }

        // �e�͏Փˌ�ɔj���i���[�J���j
        Destroy(gameObject);
    }
}
