using UnityEngine;

public class PlayerAnimatorControl : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Rigidbody playerRigidbody;

    private float speed =0f;

    /// <summary>
    /// Aiming�̃A�j���[�V�����̃t���O
    /// </summary>
    public bool IsAiming = false;

    private void FixedUpdate()
    {
        playerAnimator.SetBool("Aiming", IsAiming);

        // �x�N�g���̑傫�����X�s�[�h�Ƃ��ďo�͂���
        speed = new Vector3(
            playerRigidbody.linearVelocity.x
            ,0
            ,playerRigidbody.linearVelocity.z).magnitude;

        playerAnimator.SetFloat("Speed",speed);
    }

}
