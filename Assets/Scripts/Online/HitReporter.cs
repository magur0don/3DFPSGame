using Unity.Netcode;
using UnityEngine;

/// <summary>
/// ローカル弾の命中をサーバへ報告する中継役。
/// （所有プレイヤーの NetworkObject に付ける）
/// </summary>
public class HitReporter : NetworkBehaviour
{
    public static HitReporter LocalOwned; // 自分が所有する HitReporter の参照

    [SerializeField] private LayerMask hittableMask = ~0; // ターゲットが属するレイヤーを設定しておく

    public override void OnNetworkSpawn()
    {
        if (IsOwner) LocalOwned = this;
    }
    /// <summary>
    /// クライアント→サーバ：命中した相手(HealthNetwork)にダメージ適用を依頼
    /// </summary>
    [ServerRpc]
    public void ReportDamageServerRpc(NetworkObjectReference victimRef, int damage, Vector3 hitPoint)
    {
        if (!IsServer) return;

        if (victimRef.TryGet(out NetworkObject no) && no != null)
        {
            if (no.TryGetComponent<NetworkScoreTarget>(out var hn))
            {
                hn.ApplyDamageServer(damage, OwnerClientId, hitPoint);
                return;
            }
        }
    }
    // ★ フォールバック：座標から近接の “Spawn済み” ターゲットを見つけてダメージ
    [ServerRpc]
    public void ReportDamageByHitPointServerRpc(Vector3 hitPoint, int damage, float radius = 0.3f)
    {
        if (!IsServer) return;

        var cols = Physics.OverlapSphere(hitPoint, radius, hittableMask, QueryTriggerInteraction.Ignore);
        foreach (var c in cols)
        {
            var no = c.GetComponentInParent<NetworkObject>();
            if (no != null && no.IsSpawned && no.TryGetComponent<IServerDamageable>(out var dmg))
            {
                dmg.ApplyDamageServer(damage, OwnerClientId, hitPoint);
                break; // 最初に見つかった1体へ
            }
        }
    }
}