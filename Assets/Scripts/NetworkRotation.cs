using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class NetworkRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 oldRotation;

    private NetworkIdentity networkIdentity;
    private Player player;

    private bool habilitado = true;

    private float stillCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

        networkIdentity = this.GetComponent<NetworkIdentity>();
        oldRotation = new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z); 
        player = new Player();
        player.rotation = new Rotation();
        player.rotation.x = 0.0f;
        player.rotation.y = 0.0f;
        player.rotation.z = 0.0f;
        //player.rotation.x = "0";
        //player.rotation.y = "0";
        //player.rotation.z = "0";

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

        //if (networkIdentity.isControllingThis())
        if (habilitado)
        {
            if (oldRotation != new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z))
            {
                oldRotation = new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);
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
    }

    private void sendData()
    {
        player.rotation.x = Mathf.Round(this.transform.rotation.x * 1000.0f) / 1000.0f;
        player.rotation.y = Mathf.Round(this.transform.rotation.y * 1000.0f) / 1000.0f;
        player.rotation.z = Mathf.Round(this.transform.rotation.z * 1000.0f) / 1000.0f;

        networkIdentity.getSocket().Emit("updateRotation", new JSONObject(JsonUtility.ToJson(player)));

        //player.rotation.x = FloatToString(transform.rotation.x);
        //player.rotation.y = FloatToString(transform.rotation.y);
        //player.rotation.z = FloatToString(transform.rotation.z);

        //JSONObject data = new JSONObject();
        //data.AddField("rotX", player.rotation.x);
        //data.AddField("rotY", player.rotation.y);
        //data.AddField("rotZ", player.rotation.z);

        //networkIdentity.getSocket().Emit("updateRotation", data);
        //networkIdentity.getSocket().Emit("updateRotation", new JSONObject(JsonUtility.ToJson(player)));
    }

    public string FloatToString(float value)
    {
        return value.ToString("0.0000");
    }

    public float JSONToFloat(JSONObject jsonObject)
    {
        return float.Parse(jsonObject.ToString().Replace("\"", ""));
    }
}
