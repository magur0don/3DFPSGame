using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

using TMPro;

public class PlayerShooting : Shooting
{

    [SerializeField]
    private TextMeshProUGUI bulletText;

    /// <summary>
    /// ���C���J����
    /// </summary>
    private Camera mainCamera;

    private void Start()
    {
        bulletText.text = $"{maxBullets - bulletsFired}/{maxBullets}";
        mainCamera = Camera.main;
    }

    public void OnAttack(InputValue value)
    {
        // ������Ă��Ȃ�������A�N�[���_�E�����������牽�����Ȃ�
        if (!value.isPressed || isCooldown)
        {
            return;
        }

        // �����Ȃ��������v���C���[�̉�ʒ������甭�˂���
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        // ���������^�[�Q�b�g�ɉ������邩�����O�Ɏ擾����
        if (Physics.Raycast(ray, out RaycastHit hit, 20f))
        {
            Debug.Log($"hit:{hit.collider.name}");
        }
        Shoot(mainCamera.transform.forward, bulletText);
        bulletText.text = $"{maxBullets - bulletsFired}/{maxBullets}";
    }
}
