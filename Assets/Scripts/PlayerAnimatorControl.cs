using UnityEngine;

public class PlayerAnimatorControl : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Rigidbody playerRigidbody;

    private float speed =0f;

    private void FixedUpdate()
    {
        // �x�N�g���̑傫�����X�s�[�h�Ƃ��ďo�͂���
        speed = new Vector3(
            playerRigidbody.linearVelocity.x
            ,0
            ,playerRigidbody.linearVelocity.z).magnitude;

        playerAnimator.SetFloat("Speed",speed);
    }

}
