using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class OnlinePlayerInputRelay : NetworkBehaviour
{
    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

    [SerializeField]
    private PlayerAnimatorControl playerAnimatorControl;

    public void OnAiming(InputValue value)
    {
        if (!IsOwner)
        {
            return;
        }

        if (value.isPressed)
        {
            playerAnimatorControl.IsAiming = true;
        }
    }
    public void OnAimEnd(InputValue value)
    {
        if (!IsOwner)
        {
            return;
        }
        if (!value.isPressed)
        {
            playerAnimatorControl.IsAiming = false;
        }
    }

    public void OnAttack(InputValue value)
    {
        if (!IsOwner)
        {
            return;
        }
        if (value.isPressed)
        {
            weaponSwitcher.GetCurrentWeapon.Fire();
        }
    }

    public void OnReload(InputValue value)
    {
        if (!IsOwner)
        {
            return;
        }
        // inputActions�ɐݒ肳�ꂽReload�iR�L�[�Ȃǁj�������ꂽ��
        if (value.isPressed)
        {
            weaponSwitcher.GetCurrentWeapon.Reload();
            playerAnimatorControl.SetReload();
            Debug.Log("Reload");
        }
    }

    public void OnWeaponSwitch(InputValue value)
    {
        if (!IsOwner)
        {
            return;
        }
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
