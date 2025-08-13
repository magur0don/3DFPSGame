using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class OnlinePlayerInputRelay : NetworkBehaviour
{
    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

    [SerializeField]
    private PlayerAnimatorControl playerAnimatorControl;

    [SerializeField]
    private PlayerHealth playerHealth;

    private void Start()
    {
        if (!IsOwner)
        {
            return;
        }
        var playerAmmoUI = FindAnyObjectByType<PlayerAmmoUI>();
        playerAmmoUI.SetWeaponSwitcher(weaponSwitcher);
        var playerHealthUI = FindAnyObjectByType<PlayerHealthUI>();
        playerHealthUI.SetPlayerHealth(playerHealth);
    }

    public void OnAiming(InputValue value)
    {
        if (!IsOwner)
        {
            return;
        }

        if (value.isPressed)
        {
            AimingServerRpc();
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
            AimEndServerRpc();
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
            ReloadServerRpc();
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
            WeaponSwitchServerRpc(1);
        }
        else if (inputValue.y < 0)
        {
            WeaponSwitchServerRpc(-1);
        }
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

    [ServerRpc]
    void AimingServerRpc()
    {
        AimingClientRpc();
    }

    [ClientRpc]
    void AimingClientRpc()
    {
        playerAnimatorControl.IsAiming = true;
    }

    [ServerRpc]
    void AimEndServerRpc()
    {
        AimEndClientRpc();
    }

    [ClientRpc]
    void AimEndClientRpc()
    {
        playerAnimatorControl.IsAiming = false;
    }

    [ServerRpc]
    void ReloadServerRpc()
    {
        ReloadClientRpc();
    }

    [ClientRpc]
    void ReloadClientRpc()
    {
        weaponSwitcher.GetCurrentWeapon.Reload();
        playerAnimatorControl.SetReload();
        Debug.Log("Reload");
    }

    [ServerRpc]
    void WeaponSwitchServerRpc(int value)
    {
        WeaponSwitchClientRpc(value);
    }

    [ClientRpc]
    void WeaponSwitchClientRpc(int value)
    {
        weaponSwitcher.Switch(value);
    }
}
