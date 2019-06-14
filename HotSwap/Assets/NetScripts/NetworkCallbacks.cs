using UnityEngine;



[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
	public bool ImHost = false;
	public override void SceneLoadLocalDone(string scene){
		var spawnPosition1 = new Vector2(-10,0);
        var spawnPosition2 = new Vector2(2,0);
        var spawnPosition3 = new Vector2(0,0);
       
        // GameObject boss = null;

        if (BoltNetwork.isServer)
        {
	        BoltNetwork.Instantiate(BoltPrefabs.Hero1,spawnPosition1,Quaternion.identity);
	    	ImHost = true;
	    	GameObject.Find("GameManager").GetComponent<GameManager>().LoadPlayer1();
        }
        else{
        	BoltNetwork.Instantiate(BoltPrefabs.Boss,spawnPosition3,Quaternion.identity);
        	BoltNetwork.Instantiate(BoltPrefabs.Hero2,spawnPosition2,Quaternion.identity);
        	GameObject.Find("GameManager").GetComponent<GameManager>().LoadPlayer2();
        	GameObject.Find("GameManager").GetComponent<GameManager>().SetupGame();
        }
       	


  	//   	BoltNetwork.Instantiate(BoltPrefabs.Hero1,spawnPosition1,Quaternion.identity);
  	//   	BoltNetwork.Instantiate(BoltPrefabs.Hero2,spawnPosition2,Quaternion.identity);
	// BoltNetwork.Instantiate(BoltPrefabs.Boss,spawnPosition3,Quaternion.identity);
	}
}
