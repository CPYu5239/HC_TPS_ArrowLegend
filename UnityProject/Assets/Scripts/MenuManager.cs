using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerData data;

    /// <summary>
    /// 載入關卡
    /// </summary>
    public void LoadLevel()
    {
        data.hp = data.hpMax;
        SceneManager.LoadScene("關卡1");
    }

    /// <summary>
    /// 內購血量
    /// </summary>
    public void BuyHp_500()
    {
        data.hpMax += 500;
        data.hp = data.hpMax;
    }

    /// <summary>
    /// 內購攻擊力
    /// </summary>
    public void BuyAtk_50()
    {
        data.attack += 50;
    }
}
