using UnityEngine;

/// <summary>
/// �̗͂̊Ǘ�
/// </summary>
public class Health : MonoBehaviour
{
    /// <summary>
    /// �̗͂̍ő�l
    /// </summary>
    [SerializeField]
    private int maxHealthPoint = 5;

    /// <summary>
    /// �̗͂̍ő�l���擾����v���p�e�B
    /// </summary>
    public int GetMaxHealthPoint
    {
        get { return maxHealthPoint; }
    }

    /// <summary>
    /// ���݂̗̑͒l
    /// </summary>
    private int currentHealthPoint;
    
    /// <summary>
    /// ���݂̗̑͒l���擾����v���p�e�B
    /// </summary>
    public int GetCurrentHealthPoint
    {
        get { return currentHealthPoint; }
    }

    /// <summary>
    /// protected:�������g�ƌp�������q�N���X����A�N�Z�X�ł���
    /// virtual:�q�N���X�Ń��\�b�h�̓��e���㏑���ł���
    /// </summary>
    protected virtual void Start()
    {
        currentHealthPoint = maxHealthPoint;
    }

    /// <summary>
    /// �_���[�W����
    /// </summary>
    /// <param name="damage">���݂̗̑͒l����������_���[�W</param>
    public virtual void TakeDamage(int damage)
    {
        currentHealthPoint -= damage;
        Debug.Log(currentHealthPoint);
    }
    /// <summary>
    /// ���S����
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return currentHealthPoint <= 0;
    }
}
