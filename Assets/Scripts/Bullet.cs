using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// ヒット時のエフェクト(火花とか煙)
    /// </summary>
    [SerializeField]
    private GameObject hitEffectPrefab;
    /// <summary>
    /// 弾の寿命
    /// </summary>
    [SerializeField]
    private float lifeTime = 5f;

    /// <summary>
    /// 弾が当たった時の音声ファイル
    /// </summary>
    [SerializeField]
    private AudioClip hitAudioClip;

    /// <summary>
    /// 弾丸の速度
    /// </summary>
    [SerializeField]
    private float bulletSpeed = 1f;

    /// <summary>
    /// 外部から弾の速度を取得するプロパティ
    /// </summary>
    public float GetBulletSpeed
    {
        get { return bulletSpeed; }
    }

    private void Start()
    {
        // 一定時間後に自動削除
        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突位置と表面の法線ベクトル
        ContactPoint contact = collision.contacts[0];
        // ヒットエフェクトを生成
        if (hitEffectPrefab != null)
        {
            // hitEffectPrefabを衝突位置の表面に、表面の法線ベクトル向きに向かって生成
            Instantiate(hitEffectPrefab,
                contact.point,
                Quaternion.LookRotation(contact.normal)
                );
        }
        // 音声ファイルを再生、自動で削除される
        AudioSource.PlayClipAtPoint(hitAudioClip, contact.point);

        Destroy(gameObject);
    }
}
