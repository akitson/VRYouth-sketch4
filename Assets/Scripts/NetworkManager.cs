using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start(){
        ConnectToServer();
    }

    void ConnectToServer(){
        Debug.Log("Trying to connect to server...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Connected to server.");
        base.OnConnectedToMaster();
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom($"Room {RoomSettings.RoomNumber}", roomOptions, TypedLobby.Default);

    }

    public override void OnJoinedRoom(){
        Debug.Log($"Joined a room. Room = {PhotonNetwork.CurrentRoom.Name}");
        base.OnJoinedRoom();
    }

    public override void  OnPlayerEnteredRoom(Player newPlayer){
        Debug.Log("A new player entered the room.");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
