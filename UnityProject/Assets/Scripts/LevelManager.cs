using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("是否一開始顯示隨機技能")]
    public bool showRandomSkill;
    [Header("是否自動開門")]
    public bool autoOpenDoor;
    [Header("隨機技能介面")]
    public GameObject randomSkill;
    [Header("是否為魔王關")]
    public bool isBoss;

    private Animator door;  // 門
    private Image cross;    // 轉場畫面
    private CanvasGroup panelRevival; //復活畫面
    private Text textCountRevival; //復活畫面
    private GameObject panelResult;
    private AddsManager addsManager;

    private void Start()
    {
        door = GameObject.Find("門").GetComponent<Animator>();
        cross = GameObject.Find("轉場畫面").GetComponent<Image>();
        addsManager = GameObject.FindObjectOfType<AddsManager>();

        panelRevival = GameObject.Find("復活畫面").GetComponent<CanvasGroup>();
        panelRevival.transform.Find("復活按鈕").GetComponent<Button>().onClick.AddListener(addsManager.ShowAdd);
        textCountRevival = panelRevival.transform.Find("倒數秒數").GetComponent<Text>();

        panelResult = GameObject.Find("結算畫面");
        panelResult.GetComponent<Button>().onClick.AddListener(BackToMenu);
        

        if (autoOpenDoor) Invoke("OpenDoor", 6);    // 延遲調用("方法名稱"，延遲時間)
        if (showRandomSkill) ShowRandomSkill();
    }

    /// <summary>
    /// 返回選單方法
    /// </summary>
    private void BackToMenu()
    {
        SceneManager.LoadScene("選單畫面");
    }

    /// <summary>
    /// 顯示隨機技能介面
    /// </summary>
    private void ShowRandomSkill()
    {
        randomSkill.SetActive(true);
    }

    /// <summary>
    /// 播放開門動畫
    /// </summary>
    private void OpenDoor()
    {
        door.SetTrigger("開門");  // 動畫控制器.設定觸發("參數名稱")
    }

    /// <summary>
    /// 載入關卡
    /// </summary>
    private IEnumerator LoadLevel()
    {
        

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;      // 建立並取得場景索引值
        AsyncOperation ao = SceneManager.LoadSceneAsync(++sceneIndex);  // 載入場景
        ao.allowSceneActivation = false;                                // 載入場景資訊.是否允許切換 = 否

        while (!ao.isDone)                                              // 當(載入場景資訊.是否完成 為 否)
        {
            print(ao.progress);
            cross.color = new Color(1, 1, 1, ao.progress);              // 轉場畫面.顏色 = 新 顏色(1，1，1，透明度) // ao.progress 載入進度 0 - 0.9
            yield return new WaitForSeconds(0.01f);

            if (ao.progress >= 0.9f) ao.allowSceneActivation = true;    // 當 載入進度 >= 0.9 允許切換
        }
    }

    /// <summary>
    /// 復活畫面倒數
    /// </summary>
    /// <returns></returns>
    public IEnumerator CountDownRevival()
    {
        panelRevival.alpha = 1;   //顯示畫面
        panelRevival.interactable = true;   //設定為可互動
        panelRevival.blocksRaycasts = true;   //阻擋射線

        for (int i = 3; i > 0; i--)   //倒數計時
        {
            textCountRevival.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        panelRevival.alpha = 0;   //倒數完畢將畫面關閉
        panelRevival.interactable = false;   //設定為不可互動
        panelRevival.blocksRaycasts = false;   //阻擋玩家操作

        if (!AddsManager.lookAdds)  //如果沒有看廣告
        {
            SceneManager.LoadScene("選單畫面");  //倒數完回到選單畫面
        }

        AddsManager.lookAdds = false;
    }

    /// <summary>
    /// 隱藏復活畫面
    /// </summary>
    public void CloseRevival()
    {
        StopCoroutine(CountDownRevival());   //停止協程

        panelRevival.alpha = 0;   //倒數完畢將畫面關閉
        panelRevival.interactable = false;   //設定為不可互動
        panelRevival.blocksRaycasts = false;   //阻擋玩家操作
    }

    /// <summary>
    /// 過關方法
    /// </summary>
    public void PassLevel()
    {
        OpenDoor();

        Item[] items = FindObjectsOfType<Item>();

        for (int i = 0; i < items.Length; i++)
        {
            items[i].pass = true;
        }
    }
    
    /// <summary>
    ///顯示結算畫面 
    /// </summary>
    public void ShowResult()
    {
        panelResult.GetComponent<CanvasGroup>().alpha = 0.75f;
        panelResult.GetComponent<CanvasGroup>().interactable = true;
        panelResult.GetComponent<CanvasGroup>().blocksRaycasts = true;
        panelResult.GetComponent<Animator>().SetTrigger("結算畫面觸發");
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        panelResult.transform.Find("關卡名稱").GetComponent<Text>().text = "Lv." + currentLevel.ToString();
    }
}
