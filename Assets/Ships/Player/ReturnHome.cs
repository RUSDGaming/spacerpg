using UnityEngine;
using System.Collections;
using Game.Events;
using Game.Interfaces;
using System;

public class ReturnHome : MonoBehaviour, PlayerDamagedSubscriber
{

    [SerializeField]    PlayerInfo playerInfo;
    [SerializeField]    float warpTime;
    ControlSwitcher switcher;
    float lastHitTime;

    float remainingWarpTime;
    bool warping = false;

    // Use this for initialization
    void Start()
    {
        switcher = gameObject.GetComponent<ControlSwitcher>();
        iTween.Init(switcher.mainShip);

        GameEventSystem.RegisterSubScriber(this);
        lastHitTime = -warpTime;
    }

    public void OnDestroy()
    {
        GameEventSystem.UnRegisterSubscriber(this);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Home))
        {
            if (Time.time - lastHitTime > warpTime)
            {
                Debug.Log("Player Starting Warp");
                warping = true;
                remainingWarpTime = warpTime;
               // StartCoroutine(MorphStart());
            }
        }
    }

    public void FixedUpdate()
    {
        CountDownWarpTime();
    }

    void CountDownWarpTime()
    {
        if (!warping)
            return;

        remainingWarpTime -= Time.fixedDeltaTime;

        if (remainingWarpTime <= 0) {
            warping = false;
          //  StartCoroutine(MorphStart());
            PlayerHomeEventArgs args = new PlayerHomeEventArgs { playerId = playerInfo.playerId };
            GameEventSystem.PublishEvent(typeof(HomeSubscriber), args);
            Debug.Log("Ending Warp");
         //   StartCoroutine(MorphEnd());

        }

    }

    IEnumerator MorphStart()
    {

        float warp = .2f;
        iTween.ScaleTo(switcher.mainShip,
            iTween.Hash("name","skinny"+switcher.mainShip.ToString(),"y", 4,
            "x", .1,
            "easeType", iTween.EaseType.easeOutQuad,
            "loopType", iTween.LoopType.none, "delay", 0,
            "time", warpTime));

        yield return new WaitForSeconds(warpTime);

        //StartCoroutine(MorphEnd());

    }

    IEnumerator MorphEnd()
    {

        float warp = .2f;
        iTween.ScaleTo(switcher.mainShip,
            iTween.Hash("name", "normal" + switcher.mainShip.ToString(), "y", 1,
            "x", 1,
            "easeType", iTween.EaseType.easeOutQuad,
            "loopType", iTween.LoopType.none, "delay", 0,
            "time", warp));
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    ///  if the player is hit he can not warp home 
    /// </summary>
    /// <param name="args"></param>
    public void HandleEvent(GameEventArgs args)
    {
        PlayerHitEventArgs hitArgs = (PlayerHitEventArgs)args;

        if (hitArgs.playerId == playerInfo.playerId)
        {
            lastHitTime = Time.time;
            warping = false;
        }
    }
}
