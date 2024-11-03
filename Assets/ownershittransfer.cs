using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class ownershittransfer : MonoBehaviour , IPunOwnershipCallbacks
{
    public void Start()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void takecontrol()
    {
        PhotonNetwork.AddCallbackTarget(this);
        base.GetComponent<PhotonView>().RequestOwnership();
    }

    public void OnOwnershipRequest(PhotonView targetView,  Photon.Realtime.Player requestingPlayer)
    {
        Debug.Log("transfer?");
        base.GetComponent<PhotonView>().TransferOwnership(requestingPlayer);
    }
    
    public void OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner)
    {
        Debug.Log("transfered");
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Photon.Realtime.Player senderOfFailedRequest)
    {
        throw new System.NotImplementedException();
    }
}


