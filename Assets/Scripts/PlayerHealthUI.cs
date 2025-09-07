using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{
    // ① Imageの変数を作成。Unityから設定。
    [SerializeField]
    private Image playerHPImage;
    // ②PlayerHealthの変数を作成。これもUnityから設定。
    [SerializeField]
    private Health playerHealth;

    private OnlinePlayerHealth target;
    public void Bind(OnlinePlayerHealth oph)
    {
        target = oph;
        UpdateBar(target.CurrentHP.Value, target.GetComponent<Health>().GetMaxHealthPoint);

        // ★ HPが変わったら呼ばれる
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

    // ③Update内で、ImageのfillAmountをPlayerHealthの値
    // で代入(playerHealth.CurrentHP/ playerHealth.MaxHp )
    private void Update()
    {
        // Online用のHealthが登録されていた場合は早期return
        if (target) {

            return;
        }
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