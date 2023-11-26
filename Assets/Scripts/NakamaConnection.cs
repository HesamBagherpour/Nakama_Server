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
    
    public IClient _client;
    public ISession _session;


    private const string SessionPrefabName = "Nakama.session";

    private const string DeviceUniqueIdentifierPreName = "Nakakama.deviceUniqueIdentifier";
    // async void Start()
    // {
    //     _cient = new Client(scheme, host, plort, serverKey, UnityWebRequestAdapter.Instance);
    //     _session = await _client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier);
    //     Debug.Log("_client" + _client);
    //     Debug.Log("_session :" + _session );
    // }


    public async Task Connect()
    {
        _client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);
    

    }
  
}
 