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

    public void GameOver()
    {
          // カーソルのモードを変更し、カーソル自体を表示します
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // 一時停止
    }

    public void Retry()
    {

        Time.timeScale = 1f;
        // BuildScene内のIndexでシーンをロードする
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
