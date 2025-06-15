using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
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

    private float pitch = 0f;

    public void OnLook(InputValue value)
    {
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
    }

}
