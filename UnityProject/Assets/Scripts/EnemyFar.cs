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

    /// <summary>
    /// 生成遠程攻擊物件
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateAttack()
    {
        yield return new WaitForSeconds(data.attackDelay);   //等待攻擊CD
        Vector3 pos = new Vector3(0, data.attackOffset.y, data.attackOffset.z);
        GameObject tempAttackObject = Instantiate(AttackObject, transform.position + pos + transform.forward * data.attackOffset.z, Quaternion.identity);  //生成物件

        tempAttackObject.GetComponent<Rigidbody>().AddForce(transform.forward * data.bulletSpeed);  //讓攻擊物件飛向玩家

        tempAttackObject.AddComponent<AttackObject>();   //將攻擊物件添加腳本
        tempAttackObject.GetComponent<AttackObject>().damage = data.attack;   //將參數傳入
    }
}
