using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("敵人資料")]
    public EnemyData data;           // 敵人資料
    [Header("敵人資料")]
    public float hp;
    protected NavMeshAgent agent;    // 導覽代理器
    protected Transform player;      // 玩家變形
    protected Animator ani;
    protected float timer;

    private HpBarControl hpControl;  //血條控制

    private void Start()
    {
        // 取得元件
        ani = GetComponent<Animator>();             
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        player = GameObject.Find("玩家").transform;
        agent.SetDestination(player.position);
        hp = data.hpMax;
        hpControl = transform.Find("血條顯示系統").GetComponent<HpBarControl>();   // 透過名稱尋找子物件
        hpControl.BarControl(data.hpMax, hp);      //更新血條
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

    public void Hit(float damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, 10000);
        hpControl.BarControl(data.hpMax, hp);   //更新血條顯示
        StartCoroutine(hpControl.ShowDamage(damage));
        if (hp == 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        if (ani.GetBool("死亡動畫"))  //如果已死亡就不叫用復活畫面
        {
            return;
        }
        ani.SetBool("死亡動畫", true);  // 播放死亡動畫 SetBool("參數名稱", 布林值)
        agent.isStopped = true;  //代理器停止讓敵人不會動
        this.enabled = false;    //將腳本關閉(this可以省略不寫)
        Destroy(gameObject, 1.5f);
    }

}
