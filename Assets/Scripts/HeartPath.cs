using UnityEngine;

public class HeartPath : MonoBehaviour
{
    public float speed = 1f;             // t の進む速さ
    public float scale = 0.1f;           // ハート型のサイズ調整
    public Vector3 center = Vector3.zero; // ハートの中心位置

    private float t = 0f;

    void Update()
    {
        t += speed * Time.deltaTime;

        // ハートの媒介変数（t: ラジアン）
        float x = 16f * Mathf.Pow(Mathf.Sin(t), 3f);
        float y = 13f * Mathf.Cos(t) - 5f * Mathf.Cos(2f * t)
                - 2f * Mathf.Cos(3f * t) - Mathf.Cos(4f * t);

        // スケーリングして位置に反映
        Vector3 pos = new Vector3(x, 0, y) * scale + center;
        transform.position = pos;
    }
}
