using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("敵人資料")]
    public EnemyData data;         // 敵人資料

    protected NavMeshAgent agent;     // 導覽代理器
    protected Transform player;       // 玩家變形
    protected Animator ani;
    protected float timer;

    private void Start()
    {
        // 取得元件
        ani = GetComponent<Animator>();             
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        player = GameObject.Find("玩家").transform;
        agent.SetDestination(player.position);
    }

    private void Update()
    {
        Move();
    }

    protected virtual void Attack()
    {
        timer = 0;
        ani.SetTrigger("攻擊觸發");
    }

    //virtual 修飾詞可讓方法在子類別進行複寫
    protected virtual void Move()
    {
        agent.SetDestination(player.position);  // 代理器.設定目的地(玩家.座標)
        //ani.SetBool("跑步開關", true);           // 開啟跑步開關

        Vector3 posTarget = player.position;  //取得目標
        posTarget.y = transform.position.y;   //固定y軸
        transform.LookAt(posTarget); //看向目標

        //print(agent.remainingDistance);

        if (agent.remainingDistance <= data.stopDistance)   //判斷距離
        {
            Wait();
        }
        else
        {
            agent.isStopped = false;
            //ani.SetBool("跑步開關", true);           // 開啟跑步開關
        }
    }

    protected virtual void Wait()
    {
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

    private void Hit()
    {

    }

    private void Dead()
    {

    }
}
