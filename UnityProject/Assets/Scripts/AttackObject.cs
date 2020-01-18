using System.Collections;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    public float damage;   //外部傳入的傷害值

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")      //如果碰到玩家
        { 
            other.GetComponent<Player>().Hit(damage);   //呼叫受傷方法
        }
    }
}
