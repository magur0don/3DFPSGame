using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{
    // �@ Image�̕ϐ����쐬�BUnity����ݒ�B
    [SerializeField]
    private Image playerHPImage;
    // �APlayerHealth�̕ϐ����쐬�B�����Unity����ݒ�B
    [SerializeField]
    private PlayerHealth playerHealth;

    // �BUpdate���ŁAImage��fillAmount��PlayerHealth�̒l
    // �ő��(playerHealth.CurrentHP/ playerHealth.MaxHp )
    private void Update()
    {
        // (�ύX�������^)�ύX�����ϐ�
        // �̂悤�ȏ�������"�L���X�g"�ƌ����Č^��ύX�ł��܂��B
        playerHPImage.fillAmount = 
            (float)playerHealth.GetCurrentHealthPoint / playerHealth.GetMaxHealthPoint; 
    }
}