using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Netcode;

public class NetworkHandler : NetworkBehaviour
{
    // Start is called before the first frame update
   void Start() {
        NetworkManager.OnClientStarted += OnClientStarted;
        NetworkManager.OnServerStarted += OnServerStarted;
    }

    private void PrintMe() {
        if (IsServer) {
            NetworkHelper.Log($"I AM a Server! {NetworkManager.ServerClientId}");
        }
        if (IsHost) {
            NetworkHelper.Log($"I AM a Host! {NetworkManager.ServerClientId}/{NetworkManager.LocalClientId}");
        }
        if (IsClient) {
            NetworkHelper.Log($"I AM a Client! {NetworkManager.LocalClientId}");
        }
        if (!IsServer && !IsClient) {
            NetworkHelper.Log("I AM Nothing yet");
        }
    }

    private void OnClientStarted() {
        NetworkHelper.Log("!! Client Started !!");
        NetworkManager.OnClientConnectedCallback += ClientOnClientConnected;
        NetworkManager.OnClientDisconnectCallback += ClientOnClientDisconnected;
        NetworkManager.OnClientStopped += ClientOnClientStopped;
        PrintMe();
    }

    //Client Actions

    private void ClientOnClientConnected(ulong clientId) {
        NetworkHelper.Log("$I have connected {clientId}");

    }
    private void ClientOnClientDisconnected(ulong clientId) {
        NetworkHelper.Log($"I have disconnected {clientId}");
     }

    private void ClientOnClientStopped(bool indicator) {
        NetworkHelper.Log("!! Client Stopped !!");
        NetworkManager.OnClientConnectedCallback -= ClientOnClientConnected;
        NetworkManager.OnClientDisconnectCallback -= ClientOnClientDisconnected;
        NetworkManager.OnClientStopped -= ClientOnClientStopped;
        PrintMe();
    }

    // Server Actions
    private void OnServerStarted() {
        NetworkHelper.Log("!! Server Started!!");
        NetworkManager.OnClientConnectedCallback += ServerOnClientConnected;
        NetworkManager.OnClientDisconnectCallback += ServerOnClientDisconnected;
        NetworkManager.OnServerStopped += ServerOnServerStopped;
        PrintMe();
    }

    // CATCH UP ASSIGNMENT TIPS: USE NetworkManager.IsClient / .IsHost and the OnClientConnect / Disconnect callbacks 
    
    private void ServerOnClientConnected(ulong clientId) {
        NetworkHelper.Log($"Client {clientId} connected to the server");
    }

    private void ServerOnClientDisconnected(ulong clientId) {
        NetworkHelper.Log($"Client {clientId} disconnected to the server");
    }

    private void ServerOnServerStopped(bool indicator) {
        NetworkHelper.Log(" !!Server Stopped !!");
        NetworkManager.OnClientConnectedCallback -= ServerOnClientConnected;
        NetworkManager.OnClientDisconnectCallback -= ServerOnClientConnected;
        NetworkManager.OnServerStopped -= ServerOnServerStopped;
        PrintMe();
     }
    private void ServerSetup() { }
}
