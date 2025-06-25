using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefeated : Unity.Services.Analytics.Event
{
    public EnemyDefeated() : base("EnemyDefeated")
    {
    }

    public string enemyName { set { SetParameter("enemyName", value); } }
    public int number { set { SetParameter("number", value); } }
    public float timeToDefeat { set { SetParameter("timeToDefeat", value); } }
}
