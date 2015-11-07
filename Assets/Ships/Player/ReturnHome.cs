using UnityEngine;
using System.Collections;
using Game.Events;
using Game.Interfaces;
using System;

public class ReturnHome : MonoBehaviour, PlayerDamagedSubscriber
{

    [SerializeField]
    PlayerInfo playerInfo;

    float lastHitTime;
    [SerializeField]
    float warpTime;
    float remainingWarpTime;
    bool warping = false;

    // Use this for initialization
    void Start()
    {
        GameEventSystem.RegisterSubScriber(this);
        lastHitTime = -warpTime;
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
            PlayerHomeEventArgs args = new PlayerHomeEventArgs { playerId = playerInfo.playerId };
            GameEventSystem.PublishEvent(typeof(HomeSubscriber), args);
        }

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
