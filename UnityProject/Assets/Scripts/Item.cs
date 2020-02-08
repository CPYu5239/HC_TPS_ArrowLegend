using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool pass;
    Transform player;
    public AudioClip clip;
    AudioSource aud;

    private void Start()
    {
        player = GameObject.Find("玩家").transform;
        aud = gameObject.GetComponent<AudioSource>();

        HandleCollision();
    }

    private void Update()
    {
        GoToPlayer();
    }

    /// <summary>
    /// 管理碰撞
    /// </summary>
    private void HandleCollision()
    {
        Physics.IgnoreLayerCollision(10, 8);   //忽略碰撞
        Physics.IgnoreLayerCollision(10, 9);
    }

    /// <summary>
    /// 往玩家聚集
    /// </summary>
    private void GoToPlayer()
    {
        if (pass)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, 0.5f * 5 * Time.deltaTime);
            if (Vector3.Distance(transform.position,player.position) <= 1.5f)
            {
                aud.PlayOneShot(clip, 0.1f);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}