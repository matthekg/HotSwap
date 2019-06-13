using System.Collections;
using System.Collections.Generic;
using System;
using UdpKit;
using UnityEngine;

public class menu : Bolt.GlobalEventListener
{
	public bool Host = false;
	public void StartServer(){
		BoltLauncher.StartServer();
		Host = true;
	}
	public void StartClient(){
		BoltLauncher.StartClient();
	}
	public override void BoltStartDone(){
		if (BoltNetwork.isServer){
			string matchName = "Match Start";
			BoltNetwork.SetServerInfo(matchName,null);
			BoltNetwork.LoadScene("LevelScene");
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
