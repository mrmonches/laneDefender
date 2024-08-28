using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private int PlayerHighScore;

    public int PlayerHighScore1 { get => PlayerHighScore; set => PlayerHighScore = value; }
}
