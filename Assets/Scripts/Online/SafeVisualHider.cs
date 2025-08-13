using Unity.Netcode;
using UnityEngine;

public class SafeVisualHider : NetworkBehaviour
{
    public override void OnNetworkDespawn()
    {
        // �ǂ�ȊK�w�ł��m���Ɍ����ڂƓ�������~�߂�
        foreach (var r in GetComponentsInChildren<Renderer>(true)) r.enabled = false;
        foreach (var c in GetComponentsInChildren<Collider>(true)) c.enabled = false;

        // Scene���u���� destroy:false �̏ꍇ�ł������Ȃ��悤�ŏI���b�N
        gameObject.SetActive(false);
    }
}
