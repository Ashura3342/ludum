using UnityEngine;
using System.Collections;

public class NetManage: MonoBehaviour {

	public enum Type
	{
		CLIENT,
		SERVER
	};

	public Type TypePlayer;
	public GameObject PlayerSource;
	public string Address;
	public int Port;
	public int Listen;

	private int _playerCount = 0;

	void Start () {
		if (TypePlayer == Type.SERVER)
			Network.InitializeServer (Listen, Port, false);
		else if (TypePlayer == Type.CLIENT)
			Network.Connect (Address, Port); 
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
			GUI.Label (new Rect (100, 100, 100, 25), "Waitting");
		else if (Network.peerType == NetworkPeerType.Client)
			GUI.Label (new Rect (100, 100, 100, 25), "Client Connected");
		else if (Network.peerType == NetworkPeerType.Server)
			GUI.Label (new Rect (100, 100, 100, 25), "Server Started");
	}

	void OnDestroy()
	{
		Network.Disconnect (10);
	}

	void OnConnectedToServer()
	{
		Debug.Log ("Connected to Server");
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) {
		if (Network.isServer) {
			Debug.Log ("Local Server connection disconnected");
			_playerCount = 0;
		} else {
			if (info == NetworkDisconnection.LostConnection)
				Debug.Log ("Lost connection to the Server");
			else
				Debug.Log ("Disconnected from the Server");
		}
	}

	void OnFailedToConnect(NetworkConnectionError error)
	{
		Debug.Log ("Could not connect to server: " + error);
	}

	void OnNetworkInstantiate(NetworkMessageInfo info)
	{
		Debug.Log ("New Object instantiated by " + info.sender);
	}

	void OnPlayerConnected(NetworkPlayer player)
	{
		Debug.Log ("Player " + _playerCount + " connected from " + player.ipAddress + ":" + player.port);
		Network.Instantiate (PlayerSource, transform.position + Vector3.forward * 100, 
		                     	Quaternion.AngleAxis(0, Vector3.zero), 0);
		_playerCount++;
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
		_playerCount--;
	}

	void OnServerInitialized() {
		Debug.Log ("Server initialized and ready");
		_playerCount = 0;
	}
	
}