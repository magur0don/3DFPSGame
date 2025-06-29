using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���̏�ԊǗ�
/// </summary>
public class FPSGameManager : MonoBehaviour
{
    /// <summary>
    /// �Q�[���I�[�o�[��UI
    /// </summary>
    [SerializeField]
    private GameObject gameOverPanel;

    public void GameOver()
    {
          // �J�[�\���̃��[�h��ύX���A�J�[�\�����̂�\�����܂�
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // �ꎞ��~
    }

    public void Retry()
    {

        Time.timeScale = 1f;
        // BuildScene����Index�ŃV�[�������[�h����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
