using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class NetworkTransform : MonoBehaviour
{
    [SerializeField]
    private Vector3 oldPosition;

    private NetworkIdentity networkIdentity;
    private Player player;

    //private bool habilitado = true;

    private float stillCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

        networkIdentity = this.GetComponent<NetworkIdentity>();
        oldPosition = this.transform.position;
        player = new Player();
        player.position = new Position();
        player.position.x = 0.0f;
        player.position.y = 0.0f;
        player.position.z = 0.0f;
        //player.position.x = "0";
        //player.position.y = "0";
        //player.position.z = "0";

        if (!networkIdentity.isControllingThis())
        {
            enabled = false;
            //habilitado = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(networkIdentity.isControllingThis());

        if (networkIdentity.isControllingThis())
        //if (habilitado)
        {
            print("OLD -> x:" + oldPosition.x + " - y: " + oldPosition.y + " - z: " + oldPosition.z);
            print("x:" + this.transform.position.x + " - y: " + this.transform.position.y + " - z: " + this.transform.position.z);

            if (oldPosition != this.transform.position)
            {
                oldPosition = this.transform.position;
                stillCounter = 0;
                sendData();
            }
            else
            {
                stillCounter += Time.deltaTime;

                if (stillCounter >= 1)
                {
                    stillCounter = 0;
                    sendData();
                }
            }
        }

        //stillCounter += Time.deltaTime;
    }

    private void sendData()
    {

        //print("OLD -> x:" + player.position.x + " - y: " + player.position.y + " - z: " + player.position.z);

        player.position.x = Mathf.Round(this.transform.position.x * 1000.0f) / 1000.0f;
        player.position.y = Mathf.Round(this.transform.position.y * 1000.0f) / 1000.0f;
        player.position.z = Mathf.Round(this.transform.position.z * 1000.0f) / 1000.0f;

        //player.position.x = this.transform.position.x;
        //player.position.y = this.transform.position.y;
        //player.position.z = this.transform.position.z;

        //print("x:" + player.position.x + " - y: " + player.position.y + " - z: " + player.position.z);

        networkIdentity.getSocket().Emit("updatePosition", new JSONObject(JsonUtility.ToJson(player)));

        //player.position.x = FloatToString(transform.position.x);
        //player.position.y = FloatToString(transform.position.y);
        //player.position.z = FloatToString(transform.position.z);

        //JSONObject data = new JSONObject();
        //data.AddField("posX", player.position.x);
        //data.AddField("posY", player.position.y);
        //data.AddField("posZ", player.position.z);

        //networkIdentity.getSocket().Emit("updatePosition", data);
    }

    //public string FloatToString(float value)
    //{
    //    return value.ToString("0.0000");
    //}

    //public float JSONToFloat(JSONObject jsonObject)
    //{
    //    return float.Parse(jsonObject.ToString().Replace("\"", ""));
    //}
}
