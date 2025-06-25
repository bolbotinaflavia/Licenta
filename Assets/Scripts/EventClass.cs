using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClass : Unity.Services.Analytics.Event
{
    public EventClass() : base("EventClass")
    {
    }

    public string FabulousString { set { SetParameter("fabulousString", value); } }
    public int SparklingInt { set { SetParameter("sparklingInt", value); } }
    public float SpectacularFloat { set { SetParameter("spectacularFloat", value); } }
    public bool PeculiarBool { set { SetParameter("peculiarBool", value); } }
}
