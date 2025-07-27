using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using Unity.Netcode;
using Unity.Netcode.Components;

[RequireComponent(typeof(Rigidbody))]
public class OnlinePlayerMovement : NetworkBehaviour
{
    private Vector2 moveInput;

    public float moveSpeed = 5f;

    private NetworkRigidbody playerRigidbody;

    /// <summary>
    /// �v���C���[�������Ă�������̊
    /// ���_�����ʃI�u�W�F�N�g�ɔC���Ă���ꍇ�͂���Transdform���w��
    /// </summary>
    [SerializeField]
    private Transform lookTransform;

    [SerializeField]
    private RotationConstraint rotationConstraint;

    [SerializeField]
    private PlayerAnimatorControl playerAnimatorControl;

    private void Awake()
    {
        playerRigidbody = GetComponent<NetworkRigidbody>();

        // �J�[�\���̃��[�h��ύX���A�J�[�\�����̂���\���ɂ��܂�
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // �G�C������������
        if (playerAnimatorControl.IsAiming)
        {
            rotationConstraint.rotationOffset = new Vector3(0, 60, 0);
        }
        else
        {
            if (rotationConstraint.rotationOffset != Vector3.zero)
            {
                rotationConstraint.rotationOffset = Vector3.Lerp(
              rotationConstraint.rotationOffset,
              Vector3.zero,
              Time.deltaTime * 5f // 5f�͕�ԑ��x�i�����\�j
          );
            }
        }


        // ���_�̑O�����̃x�N�g���ƉE�����̃x�N�g�����擾
        Vector3 forward = lookTransform.forward;
        Vector3 right = lookTransform.right;

        //�㉺�������������A�n�ʂɂ������ړ��x�N�g���ɂ���
        forward.y = 0f;
        right.y = 0f;

        // ���K�����ĕ����̃x�N�g���Ƃ��Ĉ���(���x�𓝈�)
        forward.Normalize();
        right.Normalize();

        // �O��A���E�̈ړ����͂ɕ����x�N�g�����|���Ĉړ��ʂ�����
        Vector3 move = forward * moveInput.y + right * moveInput.x;
        playerRigidbody.SetLinearVelocity(move * moveSpeed);
    }

    public void OnMove(InputValue movementValue)
    {
        if (!IsOwner)
        {
            return;
        }
        moveInput = movementValue.Get<Vector2>();
    }
}
