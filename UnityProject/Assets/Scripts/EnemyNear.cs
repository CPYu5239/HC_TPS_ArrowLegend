using UnityEngine;
using System.Collections;

public class EnemyNear : Enemy
{
    public override void Move()
    {
        
        agent.SetDestination(player.position);  // 代理器.設定目的地(玩家.座標)
        ani.SetBool("跑步開關", true);           // 開啟跑步開關

        //print(agent.remainingDistance);

        if (agent.remainingDistance <= data.stopDistance)   //判斷距離
        {
            Wait();
        }
        else
        {
            agent.isStopped = false;
            ani.SetBool("跑步開關", true);           // 開啟跑步開關
        }
    }

    //override : 複寫父類別有 virtual 修飾詞的方法
    public override void Wait()
    {
        //base.Wait(); //使用父類別方法內容

        agent.isStopped = true;   //代理器停止
        agent.velocity = Vector3.zero;   //向量 = 0
        ani.SetBool("跑步開關", false);   // 關閉跑步開關

        if (timer <= data.cd)
        {
            timer += Time.deltaTime;
            //print("時間 : " + timer);
        }
        else
        {
            Attack();
        }
    }

    public override void Attack()
    {
        timer = 0;
        ani.SetTrigger("攻擊觸發");
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
