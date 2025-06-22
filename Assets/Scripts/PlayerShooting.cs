using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

using TMPro;

public class PlayerShooting : Shooting
{

    [SerializeField]
    private TextMeshProUGUI bulletText;

    /// <summary>
    /// メインカメラ
    /// </summary>
    private Camera mainCamera;

    private void Start()
    {
        bulletText.text = $"{maxBullets - bulletsFired}/{maxBullets}";
        mainCamera = Camera.main;
    }

    public void OnAttack(InputValue value)
    {
        // 押されていなかったり、クールダウン中だったら何もしない
        if (!value.isPressed || isCooldown)
        {
            return;
        }

        // 見えない光線をプレイヤーの画面中央から発射する
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        // 撃ちたいターゲットに何があるかを事前に取得する
        if (Physics.Raycast(ray, out RaycastHit hit, 20f))
        {
            Debug.Log($"hit:{hit.collider.name}");
        }
        Shoot(mainCamera.transform.forward, bulletText);
        bulletText.text = $"{maxBullets - bulletsFired}/{maxBullets}";
    }
}
