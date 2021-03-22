using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int startingMoney;
    public static int money;

    private void Start()
    {
        money = startingMoney;
    }
}
