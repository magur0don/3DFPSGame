using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{
    // �@ Image�̕ϐ����쐬�BUnity����ݒ�B
    [SerializeField]
    private Image playerHPImage;
    // �APlayerHealth�̕ϐ����쐬�B�����Unity����ݒ�B
    [SerializeField]
    private Health playerHealth;

    private OnlinePlayerHealth target;
    public void Bind(OnlinePlayerHealth oph)
    {
        target = oph;
        UpdateBar(target.CurrentHP.Value, target.GetComponent<Health>().GetMaxHealthPoint);

        // �� HP���ς������Ă΂��
        target.CurrentHP.OnValueChanged += OnHPChanged;
    }
    private void OnHPChanged(int prev, int curr)
    {
        int max = target.GetComponent<Health>().GetMaxHealthPoint;
        UpdateBar(curr, max);
    }

    private void UpdateBar(int current, int max)
    {
        playerHPImage.fillAmount = (max > 0) ? (float)current / max : 0f;
    }
    void OnDestroy()
    {
        if (target != null)
            target.CurrentHP.OnValueChanged -= OnHPChanged;
    }


    public void SetPlayerHealth(Health playerHealth)
    {
        this.playerHealth = playerHealth;
    }

    // �BUpdate���ŁAImage��fillAmount��PlayerHealth�̒l
    // �ő��(playerHealth.CurrentHP/ playerHealth.MaxHp )
    private void Update()
    {
        // Online�p��Health���o�^����Ă����ꍇ�͑���return
        if (target) {

            return;
        }
        if (playerHealth == null)
        {
            return;
        }
        // (�ύX�������^)�ύX�����ϐ�
        // �̂悤�ȏ�������"�L���X�g"�ƌ����Č^��ύX�ł��܂��B
        playerHPImage.fillAmount =
            (float)playerHealth.GetCurrentHealthPoint / playerHealth.GetMaxHealthPoint;
    }
}