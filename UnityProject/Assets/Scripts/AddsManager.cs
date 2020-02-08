using UnityEngine;
using UnityEngine.Advertisements;

public class AddsManager : MonoBehaviour, IUnityAdsListener
{
    private string googleID = "3436900";   //Google專案ID
    private string placementRevival = "revival";   //廣告名稱
    private bool testMode = true;     //測試模式 : 是否允許在Unity內測試
    private Player player;

    public static bool lookAdds;

    private void Start()
    {
        Advertisement.AddListener(this);  //新增一個監聽器監控廣告狀態
        Advertisement.Initialize(googleID, testMode);     //初始化廣告
        player = FindObjectOfType<Player>();
    }

    /// <summary>
    /// 顯示廣告
    /// </summary>
    public void ShowAdd()
    {
        if (Advertisement.IsReady(placementRevival))   //利用API來判斷廣告是否準備完成
        {
            lookAdds = true;
            Advertisement.Show(placementRevival);     //顯示廣告
        }
    }

    //廣告準備好時
    public void OnUnityAdsReady(string placementId)
    {
        
    }

    //廣告出錯時
    public void OnUnityAdsDidError(string message)
    {
        
    }

    //廣告開始時
    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    //看完廣告時
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == placementRevival)   //判斷是否為設定的廣告
        {
            switch (showResult)    //判斷廣告的結果
            {
                case ShowResult.Failed:    //失敗
                    print("廣告失敗");
                    break;
                case ShowResult.Skipped:   //略過
                    print("廣告略過");
                    break;
                case ShowResult.Finished:  //成功
                    print("廣告成功");
                    lookAdds = false;
                    player.Revival();      //呼叫玩家腳本內的復活方法
                    break;
            }
        }
    }
}
