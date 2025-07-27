using UnityEngine;

public class HeartPath : MonoBehaviour
{
    public float speed = 1f;             // t �̐i�ޑ���
    public float scale = 0.1f;           // �n�[�g�^�̃T�C�Y����
    public Vector3 center = Vector3.zero; // �n�[�g�̒��S�ʒu

    private float t = 0f;

    void Update()
    {
        t += speed * Time.deltaTime;

        // �n�[�g�̔}��ϐ��it: ���W�A���j
        float x = 16f * Mathf.Pow(Mathf.Sin(t), 3f);
        float y = 13f * Mathf.Cos(t) - 5f * Mathf.Cos(2f * t)
                - 2f * Mathf.Cos(3f * t) - Mathf.Cos(4f * t);

        // �X�P�[�����O���Ĉʒu�ɔ��f
        Vector3 pos = new Vector3(x, 0, y) * scale + center;
        transform.position = pos;
    }
}
