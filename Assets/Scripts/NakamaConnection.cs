using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using UnityEngine;

[CreateAssetMenu(fileName = "NakamaConection", menuName = "server/Nakama")]
public class NakamaConnection : ScriptableObject   // from mono to scripb;e 
{
    
    public string scheme = "http";
    public string host = "localhost";
    public int port = 7350;
    public string serverKey = "defaultkey";
    public int timeout = 10;
    
    private IClient _client;
    private ISession _session;
    private ISocket _socket;
    private const string SessionPrefabName = "Nakama.session";
    private const string DeviceUniqueIdentifierPreName = "Nakakama.deviceUniqueIdentifier";

    
    
    public async Task ConnectToSever ()
    {
        
        
        
        // Nakama Device Authentication uses the physical device’s unique identifier to easily authenticate a user and create an account if one does not exist.
        //
        //     When using only device authentication, you don’t need a login UI as the player can automatically authenticate when the game launches.
        _client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance,true);

  
        //Each request to Nakama from the client must complete in a certain period of time before
        //it is considered to have timed out.
        //You can configure how long this period is (in seconds)
        //by setting the Timeout value on the client:
        _client.Timeout = timeout;
    }
    public async Task AuthenticateDevice()
    {
        
        // _session = await _client.AuthenticateEmailAsync("hesam.bagherpour@gmail.com", "123", "hesam", true);
        // _session = await _client.AuthenticateGoogleAsync("token", "hesam", true);
        // _session = await _client.AuthenticateSteamAsync("token", "hesam", true);
        
        //Nakama Facebook Authentication is an easy to use authentication method which lets you
        //optionally import the player’s Facebook friends and add them to their Nakama Friends list.
        
        // _session = await _client.AuthenticateFacebookAsync("token", "hesam", true);
        
        // Authenticate with the Nakama server using Device Authentication.
        //_session = await _client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier, "hesam", true);
        
        try
        {
            // Authenticate the device
            _session = await _client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier);
            // Log the user ID and session token
            Debug.Log($"User ID: {_session.UserId}, Session Token: {_session.AuthToken}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error authenticating device: {e.Message}");
        }
        

        
        
        // The Unity client library includes a feature where sessions close to expiration are automatically refreshed.
        //
        //     This is enabled by default but can be configured when first creating the Nakama client using the following parameters:
        //
        // AutoRefreshSession - Boolean value indicating if this feature is enabled, true by default
        // DefaultExpiredTimespan - The time prior to session expiry when auto-refresh will occur, set to 5 minutes be default
        
        
        
        // // Check whether a session has expired or is close to expiry.
        // if (session.IsExpired || session.HasExpired(DateTime.UtcNow.AddDays(1))) {
        //     try {
        //         // Attempt to refresh the existing session.
        //         session = await client.SessionRefreshAsync(session);
        //     } catch (ApiResponseException) {
        //         // Couldn't refresh the session so reauthenticate.
        //         session = await client.AuthenticateDeviceAsync(deviceId);
        //         PlayerPrefs.SetString("nakama.refreshToken", session.RefreshToken);
        //     }
        //
        //     PlayerPrefs.SetString("nakama.authToken", session.AuthToken);
        // }
    }
    private async Task ConnectToSocket()
    {
        try
        {
            // Connect to Nakama socket
            _socket = _client.NewSocket();
            await _socket.ConnectAsync(_session, true);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error connecting to socket: {e.Message}");
        }
    }
  
}
 