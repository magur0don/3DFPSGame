using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
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

    private float pitch = 0f;

    public void OnLook(InputValue value)
    {
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
    }

}
