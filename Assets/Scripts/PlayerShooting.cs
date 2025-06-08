using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShooting : MonoBehaviour
{
    /// <summary>
    /// 弾丸のプレハブ
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// 弾丸の速度
    /// </summary>
    private float bulletSpeed = 20f;
    /// <summary>
    /// 弾丸の発射位置
    /// </summary>
    [SerializeField]
    private Transform shootPoint;

    public void OnAttack(InputValue value)
    {
        // 押されていなかったら何もしない
        if (!value.isPressed)
        {
            return;
        }

        GameObject bullet = Instantiate(
            bulletPrefab,           // 生成するGameOpbect
            shootPoint.position, // 生成する位置
            shootPoint.rotation // 生成する角度
            );
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        // 力を加える
        bulletRigidbody.AddForce(
            shootPoint.forward * bulletSpeed,
            ForceMode.Impulse);
    }



}
