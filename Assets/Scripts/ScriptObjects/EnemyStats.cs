using UnityEngine;

[CreateAssetMenu(menuName ="Enemy/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public string EnemyName;
    [TextArea]
    public string EnemyDescription;

    public float MaxHP;
    public float MoveSpeed;
}
