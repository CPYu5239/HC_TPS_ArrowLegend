using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料",menuName ="KID/玩家資料")]
public class PlayerData : ScriptableObject
{
    [Header("血量")]
    public float hp = 200;
    [Header("最大血量 : 不會改變")]
    public float hpMax = 200;
    [Header("攻擊力"), Range(0, 100)]
    public float attack = 30;
    [Header("攻擊CD"), Range(0, 5)]
    public float cd = 2.5f;
    [Header("武器飛行速度"),Range(0,3000)]
    public float power = 1500;
}
