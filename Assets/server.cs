using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class server : MonoBehaviourPunCallbacks , IOnEventCallback
{
	private GameObject camFollow;
	private GameObject MainCamera;
	public bool host;
	// Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        int isHost = PlayerPrefs.GetInt("host", 2);
        if (isHost==0)
        {
	        host = false;
	    }
        else if(isHost==1)
        {
	        host = true;
	    }
    }

    public void attackevent()
    {
	    RaiseEventOptions evt= new RaiseEventOptions{Receivers = ReceiverGroup.All};
	    if (host)
	    {
		    PhotonNetwork.RaiseEvent(1, 1, evt, SendOptions.SendReliable);
	    }
	    else
	    {	PhotonNetwork.RaiseEvent(0, 0, evt, SendOptions.SendReliable);
	    }
    }
    
    
    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
	PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
	    Debug.Log("bbbbbb");
	    if (host==false)
	    {
		    var pl=GameObject.FindGameObjectWithTag("p2");
		    pl.GetComponent<ownershittransfer>().takecontrol();
	    }
	    
    }

    
    public override void OnJoinedLobby()
    {
	    if (host)
	    {
		    //PhotonNetwork.CreateRoom("test");
		    PhotonNetwork.CreateRoom("");

		    
	    }
	    else
	    {
		    //PhotonNetwork.JoinRoom("test");
		    PhotonNetwork.JoinRandomRoom();
		    var camFollow = GameObject.FindGameObjectWithTag("CamFollow");
			var pl=GameObject.FindGameObjectWithTag("p2");
        	camFollow.GetComponent<cameraFollow>().player=pl;
        	camFollow.transform.GetChild(0).gameObject.GetComponent<cameraFollow>().player = pl;
			//pl.GetComponent<ownershittransfer>().takecontrol();
	    }
    }

    public void OnEvent(EventData photonEvent)
    {
	    byte eventcode = photonEvent.Code;
	    if (eventcode==1 && !host)
	    {
		    var pl=GameObject.FindGameObjectWithTag("Player");
		    pl.GetComponent<Player2>().StartCoroutine(pl.GetComponent<Player2>().Melee());
	    }
	    if (eventcode==0 && host)
	    {
		    var pl=GameObject.FindGameObjectWithTag("p2");
		    pl.GetComponent<Player2join>().StartCoroutine(pl.GetComponent<Player2join>().Melee());
	    }
	}
}

