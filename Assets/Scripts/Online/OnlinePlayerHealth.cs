using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
[RequireComponent(typeof(Health))]
public class OnlinePlayerHealth : NetworkBehaviour, IServerDamageable
{

    private Health health;

    // ★ UI用にHPをネットワーク同期（Everyone読み／Server書き）
    public NetworkVariable<int> CurrentHP = new(
        0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    bool deathHandled = false;

    void Awake() => health = GetComponent<Health>();
    
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            health.ResetHealth();
            CurrentHP.Value = health.GetCurrentHealthPoint; // 初期値同期
        }
        GetComponentInChildren<WorldSpaceHPBar>().Init(this);

    }

    /// <summary>弾などから呼ばれるサーバ権威のダメージ入口</summary>
    public void ApplyDamageServer(int damage, ulong attackerClientId, Vector3 hitPoint)
    {
        if (!IsServer || deathHandled)
        { 
            return;
        }

        if (NetworkManager.Singleton.LocalClientId == attackerClientId && attackerClientId == OwnerClientId)
        {
            // 例：自傷を許さないならここで return;
        }

        int before = health.GetCurrentHealthPoint;
        health.TakeDamage(damage);
        CurrentHP.Value = health.GetCurrentHealthPoint;
        Debug.Log($"[SV] Player {OwnerClientId} HP {before}->{CurrentHP.Value}  by {attackerClientId}");

        if (CurrentHP.Value > 0)
        {
            return;
        }
        //死亡処理（サーバ）
        deathHandled = true;

        // 1) キル加点（加点しないならコメントアウト）
        NetworkScoreboard.Instance?.AddScoreServerRpc(attackerClientId, 1);

        // 2) ゲーム進行に通知（1人死んだら終了にする）
        NetworkScoreboard.Instance?.NotifyPlayerDiedServerRpc(OwnerClientId);
    }
}
