using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coins : MonoBehaviour {

    public int money;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().netWorth += money;
            collision.gameObject.GetComponent<Player>().cloneLimit += 1;
            GameObject.FindGameObjectWithTag("cloneLabel").GetComponent<Text>().text = "CLONES COLLECTED: " + collision.gameObject.GetComponent<Player>().cloneLimit;
            GameObject.FindGameObjectWithTag("deployedLabel").GetComponent<Text>().text = "CLONES DEPLOYED: " + collision.gameObject.GetComponent<Player>().cloneCount + " / " + collision.gameObject.GetComponent<Player>().cloneLimit;
            Destroy(this.gameObject);
        }
    }

}
