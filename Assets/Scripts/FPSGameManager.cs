using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームの状態管理
/// </summary>
public class FPSGameManager : MonoBehaviour
{
    /// <summary>
    /// ゲームオーバーのUI
    /// </summary>
    [SerializeField]
    private GameObject gameOverPanel;
    
    [SerializeField]
    private ScoreUI scoreUI;
    // �@スコア用のint型の変数を用意してください。
    [SerializeField]
    private int gameScore = 0;

    // �Aスコア追加用のメソッドを作成してください。引数の値をスコア用の変数に追加
    public void AddScore(int addVal)
    {
        currentTargetNum--;
        gameScore += addVal;
    }

    private int currentTargetNum = 0;

    private void Start()
    {
        Time.timeScale = 1f;
        currentTargetNum =
            FindObjectsByType<ScoreTargetHealth>(FindObjectsSortMode.None).Length;
    }

    private void Update()
    {
        if (currentTargetNum <= 0)
        {
            GameOver();
        }
    }


    public void GameOver()
    {
        // カーソルのモードを変更し、カーソル自体を表示します
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
        scoreUI.SetScoreUI(gameScore);
        Time.timeScale = 0f; // 一時停止
    }

    public void Retry()
    {
        // BuildScene内のIndexでシーンをロードする
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
