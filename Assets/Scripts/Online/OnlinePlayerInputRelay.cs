using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class OnlinePlayerInputRelay : NetworkBehaviour
{
    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

    [SerializeField]
    private PlayerAnimatorControl playerAnimatorControl;

    private void Start()
    {
        var playerAmmoUI = FindAnyObjectByType<PlayerAmmoUI>();
        playerAmmoUI.SetWeaponSwitcher(weaponSwitcher);
    }


    public void OnAiming(InputValue value)
    {
        if (!IsOwner)
        {
            return;
        }

        if (value.isPressed)
        {
            IsAimingServerRpc();
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
            IsAimEndServerRpc();
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
            FireBulletServerRpc();
        }
    }

    public void OnReload(InputValue value)
    {
        if (!IsOwner)
        {
            return;
        }
        // inputActionsに設定されたReload（Rキーなど）が押されたら
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

    [ServerRpc]
    void IsAimingServerRpc()
    {
        IsAimingClientRpc();
    }

    [ClientRpc]
    void IsAimingClientRpc()
    {
        playerAnimatorControl.IsAiming = true;
    }

    [ServerRpc]
    void IsAimEndServerRpc()
    {
        IsAimEndClientRpc();
    }

    [ClientRpc]
    void IsAimEndClientRpc()
    {
        playerAnimatorControl.IsAiming = false;
    }
    [ServerRpc]
    void FireBulletServerRpc()
    {
        FireBulletClientRpc();
    }

    [ClientRpc]
    void FireBulletClientRpc()
    {
        weaponSwitcher.GetCurrentWeapon.Fire();
    }
}
