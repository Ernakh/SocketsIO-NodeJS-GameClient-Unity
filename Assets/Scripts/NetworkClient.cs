using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;
using System.Globalization;

public class NetworkClient : SocketIOComponent
{
    [Header("NetworkClient")]
    [SerializeField]
    private Transform networkContainer;
    [SerializeField]
    private GameObject playerPrefab;

    public static string ClientID { get; private set; }

    private Dictionary<string, NetworkIdentity> objetosServidor;

    // Start is called before the first frame update
    public override void Start()
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

        base.Start();
        Initialize();
        configurarEventos();
    }

    private void Initialize()
    {
        objetosServidor = new Dictionary<string, NetworkIdentity>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    private void configurarEventos()
    {
        On("open", (E) =>
        {
            Debug.Log("Conectado ao servidor!");
        });

        On("register", (E) =>
        {
            ClientID = E.data["id"].ToString().Replace("\"", "");
            Debug.Log("Meu ID é " + ClientID);
        });

        On("spawn", (E) =>
        {
            string id = E.data["id"].ToString().Replace("\"", "");

            GameObject go = Instantiate(playerPrefab, networkContainer);
            //GameObject go = Instantiate(playerPrefab, networkContainer);
            //GameObject go = new GameObject("ID: " + id);
            //go.transform.SetParent(networkContainer);
            go.name = "Player (" + id + ")";

            print("id = " + id + " clientID = " + ClientID + " adicionado na lista");

            NetworkIdentity ni = go.GetComponent<NetworkIdentity>();
            ni.setControllerID(id);
            ni.SetSocketReference(this);

            objetosServidor.Add(id, ni);

            print(objetosServidor.Count);
        });

        On("disconnected", (E) =>
        {
            string id = E.data["id"].ToString().Replace("\"", "");


            GameObject go = objetosServidor[id].gameObject;
            Destroy(go);
            objetosServidor.Remove(id);
        });

        On("updatePosition", (E) =>
        {
            print("updatePosition");
            string id = E.data["id"].ToString().Replace("\"", "");

            float x = float.Parse(E.data["position"]["x"].str);
            float y = float.Parse(E.data["position"]["y"].str);
            float z = float.Parse(E.data["position"]["z"].str);

            //float x = float.Parse(E.data["posX"].str);
            //float y = float.Parse(E.data["posY"].str);
            //float z = float.Parse(E.data["posZ"].str);

            NetworkIdentity ni = objetosServidor[id];
            ni.transform.position = new Vector3(x, y, z);
        });

        On("updateRotation", (E) =>
        {
            print("updateRotation");
            string id = E.data["id"].ToString().Replace("\"", "");

            float x = float.Parse(E.data["rotation"]["x"].str);
            float y = float.Parse(E.data["rotation"]["y"].str);
            float z = float.Parse(E.data["rotation"]["z"].str);

            //float x = float.Parse(E.data["rotX"].str);
            //float y = float.Parse(E.data["rotY"].str);
            //float z = float.Parse(E.data["rotZ"].str);

            NetworkIdentity ni = objetosServidor[id];
            ni.transform.rotation = new Quaternion(x, y, z, 0);
        });
    }
}