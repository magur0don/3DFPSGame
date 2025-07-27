using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{
    // ① Imageの変数を作成。Unityから設定。
    [SerializeField]
    private Image playerHPImage;
    // ②PlayerHealthの変数を作成。これもUnityから設定。
    [SerializeField]
    private PlayerHealth playerHealth;

    // ③Update内で、ImageのfillAmountをPlayerHealthの値
    // で代入(playerHealth.CurrentHP/ playerHealth.MaxHp )
    private void Update()
    {
        if (playerHealth == null)
        {
            return;
        }
        // (変更したい型)変更される変数
        // のような書き方を"キャスト"と言って型を変更できます。
        playerHPImage.fillAmount =
            (float)playerHealth.GetCurrentHealthPoint / playerHealth.GetMaxHealthPoint;
    }
}