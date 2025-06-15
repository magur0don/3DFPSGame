using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

using TMPro;

public class PlayerShooting : MonoBehaviour
{
    /// <summary>
    /// �e�ۂ̃v���n�u
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// �e�ۂ̑��x
    /// </summary>
    private float bulletSpeed = 1f;
    /// <summary>
    /// �e�ۂ̔��ˈʒu
    /// </summary>
    [SerializeField]
    private Transform shootPoint;

    /// <summary>
    /// �������e�ې�
    /// </summary>
    private int bulletsFired = 0;
    /// <summary>
    /// �ő�e��
    /// </summary>
    private int maxBullets = 10;
    /// <summary>
    /// �ł��؂����ꍇ�̐���
    /// </summary>
    private float cooldownDuration = 10f;

    /// <summary>
    /// �N�[���_�E���Ɏg������
    /// </summary>
    private bool isCooldown = false;

    [SerializeField]
    private TextMeshProUGUI bulletText;

    private void Start()
    {
        bulletText.text = $"{maxBullets}/{maxBullets}";
    }

    public void OnAttack(InputValue value)
    {
        // ������Ă��Ȃ�������A�N�[���_�E�����������牽�����Ȃ�
        if (!value.isPressed || isCooldown)
        {
            return;
        }

        // �������e�ې����ő吔��葝���Ă����
        if (bulletsFired >= maxBullets)
        {
            StartCoroutine(StartCooldown());
            return;
        }



        // �����Ȃ��������v���C���[�̉�ʒ������甭�˂���
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        // ���������^�[�Q�b�g�ɉ������邩�����O�Ɏ擾����
        if (Physics.Raycast(ray, out RaycastHit hit, 20f))
        {
            Debug.Log($"hit:{hit.collider.name}");
        }

        GameObject bullet = Instantiate(
      bulletPrefab,           // ��������GameOpbect
      shootPoint.position, // ��������ʒu
      shootPoint.rotation // ��������p�x
      );
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        // �͂�������
        bulletRigidbody.AddForce(
            Camera.main.transform.forward * bulletSpeed,
            ForceMode.Impulse);
        // �������e�ې��𑝂₷
        bulletsFired++;
        bulletText.text = $"{maxBullets - bulletsFired}/{maxBullets}";
    }


    private IEnumerator StartCooldown()
    {
        isCooldown = true;
        Debug.Log("�e�؂�I�N�[���_�E����...");
        yield return new WaitForSeconds(cooldownDuration);
        bulletsFired = 0;
        isCooldown = false;
        Debug.Log("�đ��U�����I");
        bulletText.text = $"{maxBullets}/{maxBullets}";
    }

}
