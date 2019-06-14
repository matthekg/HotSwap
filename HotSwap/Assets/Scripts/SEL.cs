using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEL : Bolt.GlobalEventListener
{
	public override void OnEvent(PlayerJoined evnt){
		Debug.Log(evnt.Message);
	}
}
