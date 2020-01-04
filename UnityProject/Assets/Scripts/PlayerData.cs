using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料",menuName ="KID/玩家資料")]
public class PlayerData : ScriptableObject
{
    [Header("血量")]
    public float hp = 200;
    [Header("最大血量 : 不會改變")]
    public float hpMax = 200;
}
