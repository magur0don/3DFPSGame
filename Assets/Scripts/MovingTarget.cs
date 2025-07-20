using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    /// <summary>
    /// �ړ�����͈�
    /// </summary>
    [SerializeField]
    private float range = 2f;

    /// <summary>
    /// �ړ����鑬�x
    /// </summary>
    [SerializeField]
    private float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var offset = Mathf.Sin(Time.time * speed) * range;
        transform.position = startPos + new Vector3(offset, 0, 0);
    }
}
