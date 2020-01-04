using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpBarControl : MonoBehaviour
{
    private Image img;
    private Text hpText;
    private Text damageText;

    private void Start()
    {
        img = transform.GetChild(1).GetComponent<Image>();  //透過排序尋找子物件
        hpText = transform.GetChild(2).GetComponent<Text>();
        damageText = transform.GetChild(3).GetComponent<Text>();
        damageText.text = "";
    }

    private void Update()
    {
        AngleControl();
    }

    /// <summary>
    /// 角度控制,讓血調保持原本的角度
    /// </summary>
    private void AngleControl()
    {
        //transform.eulerAngles 為世界座標   (transform.localEulerAngles為區域作標)
        transform.eulerAngles = new Vector3(35, -180, 0);   //以世界座標為準
    }
    
    /// <summary>
    /// 更新血條顯示與文字
    /// </summary>
    /// <param name="maxHp">最大血量</param>
    /// <param name="currentHp">目前血量</param>
    public void BarControl(float maxHp, float currentHp)
    {
        img.fillAmount = currentHp / maxHp;    //血量調的顯示
        hpText.text = currentHp.ToString();    //文字改變
    }

    /// <summary>
    /// 顯示傷害值
    /// </summary>
    /// <param name="damageNum">受到傷害值</param>
    public IEnumerator ShowDamage(float damageNum)
    {
        Vector3 posOriginal = damageText.transform.position;
        damageText.text = "-" + damageNum;
        for (int i = 0; i < 20; i++)
        {
            damageText.transform.position += new Vector3(0, 0.05f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        damageText.text = "";
        damageText.transform.position = posOriginal;  //回到原本坐標

        /*利用程式碼來變動Transform.position的話Unity內顯示會跑掉出現亂碼,
        利用RectTransform.anchoredPosition給值才能給Unity裡我們看到的值*/
        //damageText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,185);  
    }
}