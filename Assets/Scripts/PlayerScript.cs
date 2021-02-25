using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    private NetworkIdentity ni;

    public float velocidadeFrente = 20;
    public float velocidadeRotacao = 80;

    private Rigidbody body;

    private GameObject camera;

    private bool chao = false;
    //public float vidaMax = 100f;
    public float vida = 100f;

    private Image imgVida;

    public GameObject _point;
    public GameObject _bullet;


    // Use this for initialization
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
        //ni = this.GetComponent<NetworkIdentity>();

        camera = GameObject.Find("Camera");

        //GameObject imageObject = GameObject.FindGameObjectWithTag("imgVida");
        //imgVida = imageObject.GetComponent<Image>();

        //vidaManager(vidaMax);
    }

    //public void vidaManager(float valor)
    //{
    //    vida += valor;

    //    imgVida.fillAmount = vida / 100f;
    //}

    // Update is called once per frame
    void Update()
    {
        //if (vida <= 0)
        //{
        //    print("Game Over - o jogador xxxx foi eliminado!");
        //    Destroy(this.gameObject);
        //}
        if (ni.isControllingThis())
        {
            print("PS -> x:" + this.transform.position.x + " - y: " + this.transform.position.y + " - z: " + this.transform.position.z);

            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(0, 0, (velocidadeFrente * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(0, 0, -(velocidadeFrente * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Rotate(0, -(velocidadeRotacao * Time.deltaTime), 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Rotate(0, (velocidadeRotacao * Time.deltaTime), 0);
            }

            //chao = Physics.Raycast(this.transform.position, -Vector3.up, 1f);//ultimo parametro é o raio para verificar a distancia do eprsonagem do chao

            //if (chao && Input.GetKeyDown(KeyCode.Space))
            //{
            //    body.velocity = new Vector3(0, Input.GetAxis("Jump") + 3, 0);
            //}

            //if (Input.GetKeyDown(KeyCode.Mouse0))
            //{
                //Instantiate(_bullet, _point.transform.position, _point.transform.rotation);//Utilizado para instanciar um objeto.
                //PhotonNetwork.Instantiate("Bullet", _point.transform.position, _point.transform.rotation, 0, null);
            //}

            //camera.transform.position = Vector3.Lerp(camera.transform.position,
            //    new Vector3(this.transform.position.x, (this.transform.position.y + 1),
            //    (this.transform.position.z)), 1f);
            //camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation,
            //    this.transform.rotation, Time.deltaTime * velocidadeRotacao);
            //}

            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    vidaManager(-10f);
            //}
        }
    }
}


[Serializable]
public class Player
{
    public string id;
    public Position position;
    public Rotation rotation;
}

//[Serializable]
//public class Position
//{
//    public string x;
//    public string y;
//    public string z;
//}

//[Serializable]
//public class Rotation
//{
//    public string x;
//    public string y;
//    public string z;
//}


[Serializable]
public class Position
{
    public float x;
    public float y;
    public float z;
}

[Serializable]
public class Rotation
{
    public float x;
    public float y;
    public float z;
}