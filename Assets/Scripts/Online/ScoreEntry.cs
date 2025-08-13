using Unity.Netcode;
using UnityEngine;
using System;

public enum GameState { Playing, GameOver } // ゲーム全体の状態（プレイ中 or 終了）

[Serializable]
public struct ScoreEntry : INetworkSerializable, IEquatable<ScoreEntry>
{
    public ulong ClientId;  // プレイヤー（クライアント）の一意なID（Netcodeが割り当てる）
    public int Score;       // このプレイヤーのスコア（破壊数など）

    // コンストラクタ：IDとスコアをセット
    public ScoreEntry(ulong id, int score) { ClientId = id; Score = score; }

    // Netcode用のシリアライズ処理（サーバ⇄クライアントで同期する値をここで読み書き）
    public void NetworkSerialize<T>(BufferSerializer<T> s) where T : IReaderWriter
    {
        s.SerializeValue(ref ClientId);
        s.SerializeValue(ref Score);
    }

    // IEquatable<ScoreEntry> の実装：
    // 「同じプレイヤーか？」を判定する等値比較。ここでは ClientId が同じなら同一とみなす。
    // ※ スコア値は比較に含めない点に注意（同じ人の行を探す用途に向く）
    public bool Equals(ScoreEntry other) => ClientId == other.ClientId;
}