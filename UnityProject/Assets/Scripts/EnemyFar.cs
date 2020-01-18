using UnityEngine;
using System.Collections;

public class EnemyFar : Enemy
{
    [Header("攻擊物件")]
    public GameObject AttackObject;

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(CreateAttack());
    }

    private IEnumerator CreateAttack()
    {
        yield return new WaitForSeconds(data.attackDelay);   //等待攻擊CD
        Vector3 pos = new Vector3(0, data.attackOffset.y, data.attackOffset.z);
        Instantiate(AttackObject, transform.position + pos + transform.forward * data.attackOffset.z, Quaternion.identity);  //生成物件
    }
}
