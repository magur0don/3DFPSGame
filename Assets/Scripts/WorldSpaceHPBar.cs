using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceHPBar : MonoBehaviour
{
    [SerializeField] private Image hpFill;
    private OnlinePlayerHealth target;

    public void Init(OnlinePlayerHealth oph)
    {
        target = oph;
        target.CurrentHP.OnValueChanged += OnHPChanged;
        OnHPChanged(target.CurrentHP.Value, target.CurrentHP.Value);
    }

    private void OnHPChanged(int prev, int curr)
    {
        int max = target.GetComponent<Health>().GetMaxHealthPoint;
        hpFill.fillAmount = (float)curr / max;
    }

    void OnDestroy()
    {
        if (target != null)
            target.CurrentHP.OnValueChanged -= OnHPChanged;
    }
}
