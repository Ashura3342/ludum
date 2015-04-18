using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    private const string typeName = "Ludum";
    private string gameName = "Test Ludum Reseau";
	private int port = 25000;
	private const int listen = 5;

    private bool isRefreshingHostList = false;
    private HostData[] hostList;

    public GameObject playerPrefab;

    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
			GUI.Label(new Rect(100, 100, 100, 25), "Server : Name");
			gameName = GUI.TextField(new Rect(200, 100, 255, 25), gameName);
			GUI.Label(new Rect(475, 100, 100, 20), "Port");
			port = int.Parse(GUI.TextField(new Rect(500, 100, 100, 25), port.ToString()));
            if (GUI.Button(new Rect(600, 100, 100, 25), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(100, 125, 100, 25), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(100, 160 + (25 * i), 200, 25), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }

    private void StartServer()
    {
        Network.InitializeServer(listen, port, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }

    void OnServerInitialized()
    {
        SpawnPlayer();
    }


    void Update()
    {
        if (isRefreshingHostList && MasterServer.PollHostList().Length > 0)
        {
            isRefreshingHostList = false;
            hostList = MasterServer.PollHostList();
        }
    }

    private void RefreshHostList()
    {
        if (!isRefreshingHostList)
        {
            isRefreshingHostList = true;
            MasterServer.RequestHostList(typeName);
        }
    }


    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        SpawnPlayer();
    }


    private void SpawnPlayer()
    {
        Network.Instantiate(playerPrefab, transform.position + Vector3.up * 5, Quaternion.identity, 0);
    }
}
