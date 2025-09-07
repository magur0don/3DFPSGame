using UnityEngine;
using Unity.Netcode;

/// <summary>
/// ローカル生成の弾丸。見た目/音はローカルで再生し、
/// 命中時だけ ServerRpc でサーバにダメージ確定を依頼する。
/// 既存のヒットエフェクト/サウンド/寿命/速度プロパティを維持。
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    // ===== 既存フィールド =====
    /// <summary>ヒット時のエフェクト(火花とか煙)</summary>
    [SerializeField] private GameObject hitEffectPrefab;
    /// <summary>弾の寿命（秒）</summary>
    [SerializeField] private float lifeTime = 5f;
    /// <summary>ヒット時に鳴らす音</summary>
    [SerializeField] private AudioClip hitAudioClip;
    /// <summary>弾丸の速度（外部参照用）</summary>
    [SerializeField] private float bulletSpeed = 1f;

    /// <summary>外部から弾の速度を取得するプロパティ（既存仕様を維持）</summary>
    public float GetBulletSpeed => bulletSpeed;

    // ===== 追加フィールド（オンライン用）=====
    /// <summary>この弾を撃ったプレイヤーの ClientId（発射時に設定）</summary>
    public ulong ownerClientId;
    /// <summary>基本ダメージ</summary>
    public int baseDamage = 1;

    /// <summary>
    /// オンライン中に多重に処理が走らないようにする
    /// </summary>
    private bool hitProcessed = false;
    private void Start()
    {
        // 一定時間後に自動削除（ローカル）
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitProcessed)
        {
            return;   // ★ 二度目以降は無視
        }
        hitProcessed = true;

        // --- 見た目と音はローカルで再生 ---
        var contact = collision.contacts.Length > 0 ? collision.contacts[0] : default;

        if (hitEffectPrefab != null && collision.contacts.Length > 0)
        {
            Instantiate(hitEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));
        }
        if (hitAudioClip != null && collision.contacts.Length > 0)
        {
            AudioSource.PlayClipAtPoint(hitAudioClip, contact.point);
        }

        // --- ダメージの確定はサーバ権威へ依頼 ---
        // ・Netcode接続中：自分が撃った弾の命中のみサーバへ報告（多重報告防止）
        // ・オフライン/スタンドアロン時：従来どおり Health に直接ダメージ
        bool isOnline = NetworkManager.Singleton != null && NetworkManager.Singleton.IsListening;

        if (isOnline)
        {
            // 親方向から NetworkObject（子コライダー対策）
            var no = collision.transform.GetComponentInParent<NetworkObject>();

            // 自分の HitReporter（Owner の Player に付いている前提）
            var myPlayer = NetworkManager.Singleton.LocalClient?.PlayerObject;
            var reporter = myPlayer ? myPlayer.GetComponent<HitReporter>() : null;

            if (reporter != null)
            {
                // ★ 自分が撃った弾だけ処理する
                if (ownerClientId == NetworkManager.Singleton.LocalClientId)
                {
                    if (no != null && no.IsSpawned)
                    {
                        // ★ Spawn済みなら安全に直参照
                        reporter.ReportDamageServerRpc(no, baseDamage, this.transform.position);
                    }
                    else
                    {
                        // ★ デスポーン等で未Spawnなら座標フォールバック
                        reporter.ReportDamageByHitPointServerRpc(this.transform.position, baseDamage, 0.35f);
                    }
                }
            }
        }
        else
        {
            // オフライン（Netcode未使用）の場合は、従来どおりローカルで直接ダメージ
            if (collision.gameObject.TryGetComponent<Health>(out var health))
            {
                Debug.Log("ここがきてる");
                health.TakeDamage(baseDamage);
            }
        }

        // 弾は衝突後に破棄（ローカル）
        Destroy(gameObject);
    }
}
