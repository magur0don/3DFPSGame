using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShooting : MonoBehaviour
{
    /// <summary>
    /// �e�ۂ̃v���n�u
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// �e�ۂ̑��x
    /// </summary>
    private float bulletSpeed = 20f;
    /// <summary>
    /// �e�ۂ̔��ˈʒu
    /// </summary>
    [SerializeField]
    private Transform shootPoint;

    public void OnAttack(InputValue value)
    {
        // ������Ă��Ȃ������牽�����Ȃ�
        if (!value.isPressed)
        {
            return;
        }

        GameObject bullet = Instantiate(
            bulletPrefab,           // ��������GameOpbect
            shootPoint.position, // ��������ʒu
            shootPoint.rotation // ��������p�x
            );
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        // �͂�������
        bulletRigidbody.AddForce(
            shootPoint.forward * bulletSpeed,
            ForceMode.Impulse);
    }



}
