using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void SetScoreUI(int score)
    {
        scoreText.text = $"Score:{score}";
    }
}
