using UnityEngine;

public interface IServerDamageable
{
    // �T�[�o�ł̂�HP���Z���m�肷��
    void ApplyDamageServer(int damage, ulong attackerClientId, Vector3 hitPoint);
}
