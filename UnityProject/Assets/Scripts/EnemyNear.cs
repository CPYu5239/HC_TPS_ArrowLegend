using UnityEngine;
using System.Collections;

public class EnemyNear : Enemy
{
    //override : 複寫父類別有 virtual 修飾詞的方法
    protected override void Attack()
    {
        base.Attack();  //使用父類別方法內容
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(data.attackDelay);

        RaycastHit hit;   //射線碰撞資訊

        //物理.射線碰撞(中心點,方向,射線碰撞資訊(加上ont修飾詞可以佔存資訊),長度) 傳回bool
        //利用物理的射線碰撞來判斷是否有打到玩家
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, data.attackRange))
        {
            print("打到" + hit.collider.gameObject.name + "了!!");
            //GameObject player = GameObject.Find(hit.collider.gameObject.name);
            //player.GetComponent<Player>().Hit(data.attack);
            hit.collider.gameObject.GetComponent<Player>().Hit(data.attack);    //取得碰撞的物件並呼叫Player內的Hit()方法
        }
    }

    //事件 : 繪製圖示  -->  僅限在場景內顯示,不會在遊戲中顯示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        //transform.forward,right,up 可以抓取物件的前方,右方跟上方(反向加-號)
        //繪製射線 (中心點,方向)
        Gizmos.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward * data.attackRange);
    }
}
