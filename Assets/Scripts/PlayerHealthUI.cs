using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{
    // �@ Imageの変数を作成。Unityから設定。
    [SerializeField]
    private Image playerHPImage;
    // �APlayerHealthの変数を作成。これもUnityから設定。
    [SerializeField]
    private PlayerHealth playerHealth;

    // �BUpdate内で、ImageのfillAmountをPlayerHealthの値
    // で代入(playerHealth.CurrentHP/ playerHealth.MaxHp )
    private void Update()
    {
        // (変更したい型)変更される変数
        // のような書き方を"キャスト"と言って型を変更できます。
        playerHPImage.fillAmount = 
            (float)playerHealth.GetCurrentHealthPoint / playerHealth.GetMaxHealthPoint; 
    }
}