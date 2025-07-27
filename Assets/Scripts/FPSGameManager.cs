using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using UnityEngine.InputSystem;

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

    [SerializeField]
    private ScoreUI scoreUI;
    // �@�X�R�A�p��int�^�̕ϐ���p�ӂ��Ă��������B
    [SerializeField]
    private int gameScore = 0;

    [SerializeField]
    private NetworkManager networkManager;

    // �A�X�R�A�ǉ��p�̃��\�b�h���쐬���Ă��������B�����̒l���X�R�A�p�̕ϐ��ɒǉ�
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
        // C�L�[�ŃN���C�A���g���[�h�Őڑ�����
        if (Keyboard.current.cKey.isPressed)
        {
            networkManager.StartClient();
        }
    }


    public void GameOver()
    {
        // �J�[�\���̃��[�h��ύX���A�J�[�\�����̂�\�����܂�
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
        scoreUI.SetScoreUI(gameScore);
        Time.timeScale = 0f; // �ꎞ��~
    }

    public void Retry()
    {
        // BuildScene����Index�ŃV�[�������[�h����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
