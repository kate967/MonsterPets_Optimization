using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject monster;
    private MonsterStats monsterStats;
    private MonsterMovement monsterMovement;

    public GameObject player;
    private PlayerManager playerManger;

    public Text playerMoneyText;

    public Animator interactionAnimator;

    void Start()
    {
        monsterStats = monster.GetComponent<MonsterStats>();
        monsterMovement = monster.GetComponent<MonsterMovement>();
        playerManger = player.GetComponent<PlayerManager>();
    }

    void Update()
    {
        playerMoneyText.text = "$" + PlayerManager.money.ToString();
    }

    public void Feed()
    {
        if(PlayerManager.money >= 10)
        {
            monsterStats.FeedMonster(10f);
            PlayerManager.money -= 10;
            interactionAnimator.SetTrigger("Eat");
        }
        else
        {
            return;
        }

    }

    public void Water()
    {
        if(PlayerManager.money >= 10)
        {
            monsterStats.WaterMonster(10f);
            PlayerManager.money -= 10;
            interactionAnimator.SetTrigger("Drink");
        }
        else
        {
            return;
        }
    }

    public void StartPlaying()
    {
        monsterMovement.ChangeSpeed(monsterMovement.speed);
        monsterMovement.ChangeIsPlaying(true);
    }
}
