using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "com.pranjh.colorblind";
	private const string gameName = "MasterRoom";

	public MainPanelScript mainPanelScript;
	public Manager manager;

	// Use this for initialization
	void Start () {
//		this.StartServer ();
		this.RefreshHostList ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
	}

	private HostData[] hostList;
	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if(msEvent == MasterServerEvent.RegistrationFailedNoServer ||
		   msEvent == MasterServerEvent.RegistrationFailedGameName ||
		   msEvent == MasterServerEvent.RegistrationFailedGameType){
			Debug.Log("Registration Failed");
			return;
		}

		if (msEvent == MasterServerEvent.HostListReceived){
			hostList = MasterServer.PollHostList();

			if (hostList != null && hostList.Length > 0) {
				this.JoinServer(hostList[0]);
			}else
				this.StartServer();
		}
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
	}

	[RPC]
	public void StartGame(int difficultylevel)
	{
		int diffLevelInt = 0;

		if (!GetComponent<NetworkView>().isMine){
			switch (difficultylevel){
			case 0:
				manager.SetDifficultyLevel(ToggleValue.ToggleValues.Easy);
				break;
			case 1:
				manager.SetDifficultyLevel(ToggleValue.ToggleValues.Medium);
				break;
			case 2:
				manager.SetDifficultyLevel(ToggleValue.ToggleValues.Hard);
				break;
			}
		}else{
			switch (manager.difficultyLevel){
			case ToggleValue.ToggleValues.Easy:
				diffLevelInt = 0;
				break;
			case ToggleValue.ToggleValues.Medium:
				diffLevelInt = 1;
				break;
			case ToggleValue.ToggleValues.Hard:
				diffLevelInt = 2;
				break;
			}
		}

		mainPanelScript.SwitchToGame ();

		if (GetComponent<NetworkView>().isMine){
			GetComponent<NetworkView>().RPC("StartGame", RPCMode.OthersBuffered, diffLevelInt);
		}
	}
}
