using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
public class OnlinePlayerLook : NetworkBehaviour
{
    /// <summary>
    /// �㉺��]�p��CameraHolder
    /// </summary>
    [SerializeField]
    private Transform pitchTarget;

    /// <summary>
    /// �}�E�X�̊��x
    /// </summary>
    [SerializeField]
    private float mouseSensitivity = 100f;

    /// <summary>
    /// pitch�l��S�N���C�A���g�ɋ��L���邽�߂̕ϐ�
    /// new NetworkVariable<float>(�f�t�H���g�̒l�A�N�Ɍ����āA�N�����M���邩)
    /// </summary>
    private NetworkVariable<float> networkedPitch = new NetworkVariable<float>(
        0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    private float pitch = 0f;

    private void Start()
    {
        if (!IsOwner)
        {
            pitchTarget.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // �I�[�i�[�ł͂Ȃ��v���C���[�́A�������ꂽpitch���g���ĉ�]������
        if (!IsOwner)
        {
            pitchTarget.localRotation = Quaternion.Euler(networkedPitch.Value, 0, 0);
        }
    }

    public void OnLook(InputValue value)
    {
        if (!IsOwner)
        {
            return;
        }

        // �v���C���[�̎��_����(x:��,y:�c)
        Vector2 look = value.Get<Vector2>();

        // x�������Ɋ��x���|����1�t���[��������̍��E��]�̊p�x���Z�o
        float yaw = look.x * mouseSensitivity * Time.deltaTime;
        // �㉺��]�̍X�V
        float pitchDelta = -look.y * mouseSensitivity * Time.deltaTime;
        // pitch�p�����͈�(-80~80)��Clamp(�������܂�)
        pitch = Mathf.Clamp(pitch + pitchDelta, -80f, 80f);

        // pitch�̒l���㉺��]�p�̃I�u�W�F�N�g�ɓK�p
        pitchTarget.localRotation = Quaternion.Euler(pitch, 0, 0);
        // yaw�̒l��Player�{�̂�Y����]�Ƃ��ēK�p����
        this.transform.Rotate(Vector3.up * yaw);

        // ���v���C���[�̂��߂�pitch���l�b�g���[�N�ϐ��ɔ��f
        networkedPitch.Value = pitch;
    }

}
