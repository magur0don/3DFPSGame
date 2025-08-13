using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class NetworkScoreboard : NetworkBehaviour
{
    public static NetworkScoreboard Instance { get; private set; }

    /// <summary>
    /// NetworkList はフィールドにして Awake で new（Spawn 前に初期化）
    /// </summary>
    public NetworkList<ScoreEntry> Scores;

    /// <summary>
    /// Scene上の残りターゲット
    /// </summary>
    public NetworkVariable<int> RemainingTargets = new(
        0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    /// <summary>
    /// Gameのステータス
    /// </summary>
    public NetworkVariable<GameState> State = new(
        GameState.Playing, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    void Awake()
    {
        Instance = this;
        if (Scores == null)
        {
            Scores = new NetworkList<ScoreEntry>(
               readPerm: NetworkVariableReadPermission.Everyone,
               writePerm: NetworkVariableWritePermission.Server);
        }
    }

    /// <summary>
    /// ネットワークにつながった時の処理
    /// </summary>
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // 既存接続者を初期登録
            Scores.Clear();
            foreach (var c in NetworkManager.ConnectedClientsList)
            {
                AddPlayerIfMissing(c.ClientId);
            }
            NetworkManager.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.OnClientDisconnectCallback += OnClientDisconnected;
        }
    }
    /// <summary>
    /// ネットワークが切れた時の後処理
    /// </summary>
    public override void OnNetworkDespawn()
    {
        if (IsServer && NetworkManager != null)
        {
            NetworkManager.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.OnClientDisconnectCallback -= OnClientDisconnected;
        }
        if (Instance == this)
        {
            Instance = null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientId"></param>
    void OnClientConnected(ulong clientId)
    {
        if (!IsServer)
        {
            return;
        }
        AddPlayerIfMissing(clientId);
    }

    /// <summary>
    /// プレイヤーの接続が切れた時の処理
    /// </summary>
    /// <param name="clientId"></param>
    void OnClientDisconnected(ulong clientId)
    {
        if (!IsServer) return;
        int idx = IndexOf(clientId);
        if (idx >= 0)
        {
            Scores.RemoveAt(idx);
        }
    }

    void AddPlayerIfMissing(ulong id)
    {
        if (IndexOf(id) >= 0)
        {
            return;
        }
        Scores.Add(new ScoreEntry(id, 0));
    }

    int IndexOf(ulong id)
    {
        for (int i = 0; i < Scores.Count; i++)
        {
            if (Scores[i].ClientId == id)
            {
                return i;
            }
        }
        return -1;
    }

    // —— スコア加点（サーバ） ——
    [ServerRpc(RequireOwnership = false)]
    public void AddScoreServerRpc(ulong scorerClientId, int amount = 1)
    {
        if (!IsServer || State.Value == GameState.GameOver)
        {
            return;
        }
        int i = IndexOf(scorerClientId);
        if (i < 0)
        {
            Scores.Add(new ScoreEntry(scorerClientId, amount));
        }
        else
        {
            // struct はコピー→変更→再代入が必須
            var e = Scores[i];
            e.Score += amount;
            Scores[i] = e;
        }
    }

    public void TargetDestroyedServerOnly()
    {
        if (!IsServer || State.Value == GameState.GameOver)
        {
            return;
        }
        // 0以下にならないように調整
        RemainingTargets.Value = Mathf.Max(0, RemainingTargets.Value - 1);
        if (RemainingTargets.Value == 0)
        {
            EndGame();
        }
    }

    /// <summary>
    /// —— 終了処理（サーバ確定→全員UI表示） ——
    /// </summary>
    void EndGame()
    {
        if (!IsServer) return;
        State.Value = GameState.GameOver;
        ShowResultClientRpc();
    }

    [ClientRpc]
    void ShowResultClientRpc()
    {
        var ui = FindAnyObjectByType<LeaderboardUI>();
        if (ui != null)
        {
            ui.ShowResult(); // UI側で Scores を読んで描画
        }
    }
}
