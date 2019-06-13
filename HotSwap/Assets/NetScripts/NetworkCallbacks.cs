using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
	public override void SceneLoadLocalDone(string scene){
		var spawnPosition1 = new Vector2(-10,0);
        var spawnPosition2 = new Vector2(10,0);
         var spawnPosition3 = new Vector2(10,0);

		BoltNetwork.Instantiate(BoltPrefabs.Hero1,spawnPosition1,Quaternion.identity);
		BoltNetwork.Instantiate(BoltPrefabs.Hero2,spawnPosition2,Quaternion.identity);
		BoltNetwork.Instantiate(BoltPrefabs.Boss,spawnPosition3,Quaternion.identity);

		GameObject.Find("GameManager").GetComponent<GameManager>().SetupGame();
	}
}
