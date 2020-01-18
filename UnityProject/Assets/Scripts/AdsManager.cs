using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    private string googleID = "3436900";   //Google專案ID
    private string placementRevival = "revivals";   //廣告名稱
    private bool testMode = true;     //測試模式 : 是否允許在Unity內測試

    private void Start()
    {
        Advertisement.Initialize(googleID, testMode);     //初始化廣告
    }
}
