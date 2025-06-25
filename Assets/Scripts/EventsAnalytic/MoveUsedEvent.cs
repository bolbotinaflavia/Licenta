using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUsedEvent : Unity.Services.Analytics.Event
{
    public MoveUsedEvent() : base("MoveUsed")
    {
    }
    public string moveName { set { SetParameter("MoveName", value); } }
    public int number { set { SetParameter("number", value); } }
}
