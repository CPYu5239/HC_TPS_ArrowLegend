using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料",menuName ="KID/玩家資料")]
public class PlayerData : ScriptableObject
{
    [Header("血量"), Range(100, 500)]
    public float hp = 500;
}
