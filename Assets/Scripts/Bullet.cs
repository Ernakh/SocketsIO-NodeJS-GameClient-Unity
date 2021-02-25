using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    void Update()
    {
        this.transform.Translate(0, 0, (3 * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "Player")
        {
            PlayerScript playScript = other.gameObject.GetComponent<PlayerScript>();

            playScript.vidaManager(-10);
        }*/
    }
}
