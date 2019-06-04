using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
	public override void SceneLoadLocalDone(string scene){
		var spawnPosition = new Vector2(-10,0);
		BoltNetwork.Instantiate(BoltPrefabs.Square,spawnPosition,Quaternion.identity);
	}
}
