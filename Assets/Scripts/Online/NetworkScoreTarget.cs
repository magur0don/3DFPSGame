using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
[RequireComponent(typeof(Health))]
public class NetworkScoreTarget : NetworkBehaviour, IServerDamageable
{
    [SerializeField] int score = 10;
    Health health;
    bool handled;

    void Awake() => health = GetComponent<Health>();

    // HitReporter��ServerRpc ����Ă΂��z��
    public void ApplyDamageServer(int damage, ulong attackerClientId, Vector3 hitPoint)
    {
        if (!IsServer || handled) return;

        int before = health.GetCurrentHealthPoint; // ���Ȃ��� Health �ɍ��킹�ĎQ��
        health.TakeDamage(damage);
        Debug.Log($"[SV]{name} HP {before} -> {health.GetCurrentHealthPoint}");

        if (!health.IsDead()) return;
        handled = true;

        // 1) ���_
        NetworkScoreboard.Instance?.AddScoreServerRpc(attackerClientId, score);
        // 2) �c�������炷�i0�Ȃ�T�[�o����GameOver�ցj
        NetworkScoreboard.Instance?.TargetDestroyedServerOnly();
        // 3) Despawn
        var no = GetComponent<NetworkObject>();
        if (no && no.IsSpawned) no.Despawn((bool)no.IsSceneObject ? false : true);
    }
}
