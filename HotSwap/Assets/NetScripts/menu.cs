﻿using System;
using UdpKit;
using UnityEngine;

public class menu : Bolt.GlobalEventListener
{
	public void StartServer(){
		BoltLauncher.StartServer();
	}
	public void StartClient(){
		BoltLauncher.StartClient();
	}
	public override void BoltStartDone(){
		if (BoltNetwork.isServer){
			string matchName = "Match Start";
			BoltNetwork.SetServerInfo(matchName,null);
			BoltNetwork.LoadScene("SampleScene");
		}
	}
	public override void SessionListUpdated(Map<Guid, UdpSession> sessionList){
		foreach (var session in sessionList){
			UdpSession photonSession = session.Value as UdpSession;
			if (photonSession.Source == UdpSessionSource.Photon){
				BoltNetwork.Connect(photonSession);
			}
		}
	}
}
