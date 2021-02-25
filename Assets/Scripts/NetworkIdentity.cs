using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkIdentity : MonoBehaviour
{
    [SerializeField]
    private string id;
    [SerializeField]
    private bool isControlling = false;

    private SocketIOComponent socket;

    // Start is called before the first frame update
    void Start()
    {
        //isControlling = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setControllerID(string ID)
    {
        id = ID;

        //print("setControllerID: id = " + ID + " e clientID = " + NetworkClient.ClientID + " - isControlling = " + isControlling);

        //if(isControlling)
        //{
        //    return;
        //}

        //print("NetworkClient.ClientID == ID --> " + (NetworkClient.ClientID == ID));

        isControlling = (NetworkClient.ClientID == ID) ? true : false;
        //print(ID + " IsControling = " + isControlling);
    }

    public void SetSocketReference(SocketIOComponent Socket)
    {
        socket = Socket;
    }

    public string getID()
    {
        return id;
    }

    public bool isControllingThis()
    {
        //print(id + " return IsControling = " + isControlling);
        return isControlling;
    }

    public SocketIOComponent getSocket()
    {
        return socket;
    }
}
