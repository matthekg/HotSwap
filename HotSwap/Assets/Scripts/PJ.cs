using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ : Bolt.EntityBehaviour<IPracticeState>
{
    public override void Attached()
    {
    	var evnt = PlayerJoined.Create();
    	evnt.Message = "Hello!";
    	evnt.Send();
    }
}
