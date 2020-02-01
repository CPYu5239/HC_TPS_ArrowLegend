using System.Collections;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    public float damage;   //外部傳入的傷害值

    private void OnTriggerEnter(Collider other)
    {
        if (this.tag == "EnemyBullet" && other.tag == "Player")      //如果物件為敵人的並碰到玩家
        { 
            other.GetComponent<Player>().Hit(damage);   //呼叫受傷方法
        }
        else if (this.tag == "PlayerBullet" && other.tag == "Enemy") //如果物件為玩家的並碰到敵人
        {
            other.GetComponent<Enemy>().Hit(damage);   //呼叫受傷方法
        }
    }
}
