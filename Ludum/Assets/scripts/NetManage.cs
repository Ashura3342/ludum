using UnityEngine;
using System.Collections;

public class NetManage: MonoBehaviour {

	public enum Type
	{
		CLIENT,
		SERVER
	};

	public Type TypePlayer;
	public string Address;
	public int Port;
	public int Listen;
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
}
