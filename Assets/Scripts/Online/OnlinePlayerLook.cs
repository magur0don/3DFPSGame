using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
public class OnlinePlayerLook : NetworkBehaviour
{
    /// <summary>
    /// 上下回転用のCameraHolder
    /// </summary>
    [SerializeField]
    private Transform pitchTarget;

    /// <summary>
    /// マウスの感度
    /// </summary>
    [SerializeField]
    private float mouseSensitivity = 100f;

    /// <summary>
    /// pitch値を全クライアントに共有するための変数
    /// new NetworkVariable<float>(デフォルトの値、誰に向けて、誰が発信するか)
    /// </summary>
    private NetworkVariable<float> networkedPitch = new NetworkVariable<float>(
        0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    private float pitch = 0f;

    private void Start()
    {
        if (!IsOwner)
        {
            pitchTarget.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // オーナーではないプレイヤーは、同期されたpitchを使って回転させる
        if (!IsOwner)
        {
            pitchTarget.localRotation = Quaternion.Euler(networkedPitch.Value, 0, 0);
        }
    }

    public void OnLook(InputValue value)
    {
        if (!IsOwner)
        {
            return;
        }

        // プレイヤーの視点入力(x:横,y:縦)
        Vector2 look = value.Get<Vector2>();

        // x軸成分に感度を掛けて1フレーム当たりの左右回転の角度を算出
        float yaw = look.x * mouseSensitivity * Time.deltaTime;
        // 上下回転の更新
        float pitchDelta = -look.y * mouseSensitivity * Time.deltaTime;
        // pitch角を一定範囲(-80~80)でClamp(制限します)
        pitch = Mathf.Clamp(pitch + pitchDelta, -80f, 80f);

        // pitchの値を上下回転用のオブジェクトに適用
        pitchTarget.localRotation = Quaternion.Euler(pitch, 0, 0);
        // yawの値はPlayer本体のY軸回転として適用する
        this.transform.Rotate(Vector3.up * yaw);

        // 他プレイヤーのためにpitchをネットワーク変数に反映
        networkedPitch.Value = pitch;
    }

}
