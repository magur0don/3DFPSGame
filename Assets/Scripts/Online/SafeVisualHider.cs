using Unity.Netcode;
using UnityEngine;

public class SafeVisualHider : NetworkBehaviour
{
    public override void OnNetworkDespawn()
    {
        // どんな階層でも確実に見た目と当たりを止める
        foreach (var r in GetComponentsInChildren<Renderer>(true)) r.enabled = false;
        foreach (var c in GetComponentsInChildren<Collider>(true)) c.enabled = false;

        // Scene直置きで destroy:false の場合でも見えないよう最終ロック
        gameObject.SetActive(false);
    }
}
