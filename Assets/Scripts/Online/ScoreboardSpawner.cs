using Unity.Netcode;
using UnityEngine;
using System.Collections;

public class ScoreboardSpawner : MonoBehaviour
{
    [SerializeField] private NetworkObject scoreboardPrefab;
    private bool spawned;

    void OnEnable()
    {
        TryHookOrSpawn();
    }

    void OnDisable()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
        }
    }

    void TryHookOrSpawn()
    {
        var nm = NetworkManager.Singleton;
        if (nm == null) { StartCoroutine(WaitForNetworkManager()); return; }

        // ���łɃT�[�o�������Ă���Α��X�|�[��
        if (nm.IsListening && nm.IsServer)
            SpawnIfServer();
        else
            nm.OnServerStarted += OnServerStarted;
    }

    void OnServerStarted()
    {
        SpawnIfServer();
        NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
    }

    void SpawnIfServer()
    {
        if (spawned) return;
        var nm = NetworkManager.Singleton;
        if (nm == null || !nm.IsServer) return;

        // ���ɑ��݂��Ă���Γ�d�������Ȃ�
        if (NetworkScoreboard.Instance != null &&
            NetworkScoreboard.Instance.NetworkObject.IsSpawned)
        {
            spawned = true; return;
        }

        var no = Instantiate(scoreboardPrefab);
        no.Spawn(true); // Prefab�� NetworkPrefabs �ɓo�^�K�{
        spawned = true;
        Debug.Log("[ScoreboardSpawner] Spawned NetworkScoreboard");
    }

    IEnumerator WaitForNetworkManager()
    {
        while (NetworkManager.Singleton == null) yield return null;
        TryHookOrSpawn();
    }
}
