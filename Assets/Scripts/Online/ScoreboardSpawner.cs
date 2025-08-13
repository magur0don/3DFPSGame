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

        // すでにサーバが動いていれば即スポーン
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

        // 既に存在していれば二重生成しない
        if (NetworkScoreboard.Instance != null &&
            NetworkScoreboard.Instance.NetworkObject.IsSpawned)
        {
            spawned = true; return;
        }

        var no = Instantiate(scoreboardPrefab);
        no.Spawn(true); // Prefabは NetworkPrefabs に登録必須
        spawned = true;
        Debug.Log("[ScoreboardSpawner] Spawned NetworkScoreboard");
    }

    IEnumerator WaitForNetworkManager()
    {
        while (NetworkManager.Singleton == null) yield return null;
        TryHookOrSpawn();
    }
}
