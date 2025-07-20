using UnityEngine;

public class SpiralMovement : MonoBehaviour
{
    public float initialRadius = 0f;
    public float radiusGrowthRate = 1f;
    public float speed = 1f;
    public float maxRadius = 10f;
    public Vector3 center = Vector3.zero;

    private float theta = 0f;
    private bool returning = false;
    private float currentRadius = 0f;

    void Update()
    {
        theta += speed * Time.deltaTime;

        if (!returning)
        {
            currentRadius = initialRadius + radiusGrowthRate * theta;

            // �߂胂�[�h�ɐ؂�ւ���
            if (currentRadius >= maxRadius)
            {
                returning = true;
                // �Ƃ͈ێ������܂܁Ar�̌������J�n
            }
        }
        else
        {
            currentRadius -= radiusGrowthRate * speed * Time.deltaTime;

            if (currentRadius <= 0f)
            {
                currentRadius = 0f;
            }
        }

        float x = currentRadius * Mathf.Cos(theta);
        float y = currentRadius * Mathf.Sin(theta);
        transform.position = center + new Vector3(x, 0, y);
        if (transform.position == center)
        {
            returning = false;
            theta = 0f;
        }
    }
}
