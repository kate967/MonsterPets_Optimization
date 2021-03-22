using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStats : MonoBehaviour
{
    [Header("Max Stats")]
    public float maxHappy;
    public float maxHunger;
    public float maxThirst;

    private float currHappy;
    private float currHunger;
    private float currThirst;
    
    [Header("Stat References")]
    public Image happyImage;
    public Image hungerImage;
    public Image thirstImage;

    public Animator deathScreen;
    public Animator monsterUI;

    void Start()
    {
        currHappy = maxHappy;
        currHunger = maxHunger;
        currThirst = maxThirst;
    }

    void Update()
    {
        DecrementStats();
        ClampStats();

        //Update UI
        happyImage.fillAmount = currHappy / maxHappy;
        hungerImage.fillAmount = currHunger / maxHunger;
        thirstImage.fillAmount = currThirst / maxThirst;
    }

    private void DecrementStats()
    {
        currHunger -= Time.deltaTime;
        currThirst -= Time.deltaTime;

        if (currHunger < maxHunger / 2 || currThirst < maxThirst / 2)
        {
            currHappy -= Time.deltaTime * 2;
        }
        else if(currHunger < maxHunger/4 || currThirst < maxThirst/4)
        {
            currHappy -= Time.deltaTime * 200;
        }
        else
        {
            currHappy -= Time.deltaTime;
        }

        if(currHappy <= 0 || currHunger <= 0 || currThirst <= 0) // death
        {
            deathScreen.SetTrigger("Open");
            monsterUI.gameObject.SetActive(true);
            monsterUI.SetTrigger("Open");
        }

    }

    private void ClampStats()
    {
        if (currHappy > maxHappy)
            currHappy = maxHappy;
        if (currHunger > maxHunger)
            currHunger = maxHunger;
        if (currThirst > maxThirst)
            currThirst = maxThirst;

        if (currHappy < 0)
            currHappy = 0;
        if (currHunger < 0)
            currHunger = 0;
        if (currThirst < 0)
            currThirst = 0;
    }

    public void FeedMonster(float foodAmount)
    {
        currHunger += foodAmount;
    }
    
    private void TriggerMonsterDeath()
    {

    }

    public void WaterMonster(float waterAmount)
    {
        currThirst += waterAmount;
    }

    public void MakeMonsterHappy(float happyAmount)
    {
        currHappy += happyAmount;
    }

    public void DecrementHunger(float amount)
    {
        currHunger -= amount;
    }

    public void DecrementThirst(float amount)
    {
        currThirst -= amount;
    }

    public float CheckHunger()
    {
        return currHunger;
    }

    public float CheckThirst()
    {
        return currThirst;
    }
}
