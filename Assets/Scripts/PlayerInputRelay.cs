using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputRelay : MonoBehaviour
{
    [SerializeField]
    private WeaponSwitcher weaponSwitcher;
   
    [SerializeField]
    private PlayerAnimatorControl playerAnimatorControl;

    public void OnAiming(InputValue value)
    {
        if (value.isPressed)
        {
            playerAnimatorControl.IsAiming = true;
        }
    }
    public void OnAimEnd(InputValue value)
    {
        if (!value.isPressed)
        {
            playerAnimatorControl.IsAiming = false;
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            weaponSwitcher.GetCurrentWeapon.Fire();
        }
    }

    public void OnReload(InputValue value)
    {
        // inputActionsに設定されたReload（Rキーなど）が押されたら
        if (value.isPressed)
        {
            weaponSwitcher.GetCurrentWeapon.Reload();
            Debug.Log("Reload");
        }
    }

    public void OnWeaponSwitch(InputValue value)
    {
        var inputValue = value.Get<Vector2>();
        if (inputValue.y > 0)
        {
            weaponSwitcher.Switch(1);
        }
        else if (inputValue.y < 0)
        {
            weaponSwitcher.Switch(-1);
        }
    }

}
