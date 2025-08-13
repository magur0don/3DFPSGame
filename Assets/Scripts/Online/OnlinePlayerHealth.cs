using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject)), RequireComponent(typeof(Health))]
public class OnlinePlayerHealth : NetworkBehaviour, IServerDamageable
{
    private Health health;

    public NetworkVariable<int> CurrentHp = new(0,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> MaxHp = new(0,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        health = GetComponent<Health>();
        if (IsServer)
        {
            MaxHp.Value = health.GetMaxHealthPoint;
            CurrentHp.Value = health.GetCurrentHealthPoint;
        }
        CurrentHp.OnValueChanged += (_, curr) =>
        {
            // ここで自分のHP UIを更新（IsOwnerなら自分のHUDなど）
        };
    }

    public void ApplyDamageServer(int damage, ulong attackerClientId, Vector3 hitPoint)
    {
        if (!IsServer) return;

        health.TakeDamage(damage);
        CurrentHp.Value = Mathf.Clamp(health.GetCurrentHealthPoint, 0, MaxHp.Value);

        if (health.IsDead())
        {
            GetComponent<NetworkObject>()?.Despawn();
        }
    }
}