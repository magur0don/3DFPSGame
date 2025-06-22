using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// �ˌ��̃x�[�X�N���X
/// </summary>
public class Shooting : MonoBehaviour
{
    /// <summary>
    /// �e�ۂ̃v���n�u
    /// </summary>
    public GameObject BulletPrefab;

    /// <summary>
    /// �e�ۂ̔��ˈʒu
    /// </summary>
    [SerializeField]
    protected Transform shootPoint;

    /// <summary>
    /// �������e�ې�
    /// </summary>
    protected int bulletsFired = 0;
    /// <summary>
    /// �ő�e��
    /// </summary>
    protected int maxBullets = 10;

    /// <summary>
    /// �ł��؂����ꍇ�̐���
    /// </summary>
    protected float cooldownDuration = 10f;

    /// <summary>
    /// �N�[���_�E���Ɏg������
    /// </summary>
    protected bool isCooldown = false;

    /// <summary>
    /// �ˌ�����
    /// </summary>
    /// <param name="direction">������</param>
    /// <param name="remainingText">�c�e��\������K�v������Ύw��:null�ő��</param>
    protected virtual void Shoot(Vector3 direction,TextMeshProUGUI remainingText = null)
    {
        if (bulletsFired >= maxBullets)
        {
            StartCoroutine(StartCooldown(remainingText));
            return;
        }
        GameObject bullet = Instantiate(BulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        float speed = bullet.GetComponent<Bullet>().GetBulletSpeed;
        bulletRb.AddForce(direction * speed, ForceMode.Impulse);
        bulletsFired++;
    }

    protected IEnumerator StartCooldown(TextMeshProUGUI remainingText = null)
    {
        isCooldown = true;
        Debug.Log("�e�؂�I�N�[���_�E����...");
        yield return new WaitForSeconds(cooldownDuration);
        bulletsFired = 0;
        isCooldown = false;
        Debug.Log("�đ��U�����I");
        if (remainingText != null)
        {
            remainingText.text =$"{ maxBullets}/{ maxBullets }";
        }
    }
}
