using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] GameObject resultPanel;
    [SerializeField] TMP_Text tableText;

    void OnEnable() { StartCoroutine(BindWhenReady()); }
    void OnDisable()
    {
        if (NetworkScoreboard.Instance != null && NetworkScoreboard.Instance.Scores != null)
            NetworkScoreboard.Instance.Scores.OnListChanged -= OnChanged;
    }

    System.Collections.IEnumerator BindWhenReady()
    {
        while (NetworkScoreboard.Instance == null) yield return null;
        var sb = NetworkScoreboard.Instance;
        sb.Scores.OnListChanged += OnChanged; // 変更のたびに再描画
        Refresh();
    }

    void OnChanged(Unity.Netcode.NetworkListEvent<ScoreEntry> _) => Refresh();

    void Refresh()
    {
        var sb = NetworkScoreboard.Instance;
        if (sb == null || sb.Scores == null) return;

        // NetworkList → Listにコピーして順位付け
        var list = new List<ScoreEntry>(sb.Scores.Count);
        for (int i = 0; i < sb.Scores.Count; i++) list.Add(sb.Scores[i]);
        list.Sort((a, b) => b.Score.CompareTo(a.Score));

        var s = new System.Text.StringBuilder();
        int rank = 1;
        foreach (var e in list) { s.AppendLine($"{rank}. Player {e.ClientId} : {e.Score}"); rank++; }
        tableText.text = s.ToString();
    }

    public void ShowResult() { Refresh(); resultPanel.SetActive(true); }
}
