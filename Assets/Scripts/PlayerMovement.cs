using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;

    public float moveSpeed = 5f;

    private Rigidbody playerRigidbody;

    /// <summary>
    /// �v���C���[�������Ă�������̊
    /// ���_�����ʃI�u�W�F�N�g�ɔC���Ă���ꍇ�͂���Transdform���w��
    /// </summary>
    [SerializeField]
    private Transform lookTransform;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        // �J�[�\���̃��[�h��ύX���A�J�[�\�����̂���\���ɂ��܂�
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
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
        playerRigidbody.linearVelocity = move * moveSpeed;
    }

    public void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
    }
}
