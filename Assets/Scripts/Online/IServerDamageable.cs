using UnityEngine;

public interface IServerDamageable
{
    // サーバでのみHP減算を確定する
    void ApplyDamageServer(int damage, ulong attackerClientId, Vector3 hitPoint);
}
