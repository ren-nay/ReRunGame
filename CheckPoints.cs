using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoints : MonoBehaviour {

    public bool passed;
    public Animator CheckPointanimator;
    private bool isCloning;

    // Use this for initialization
    void Start () {
        passed = false;
    }
	
	// Update is called once per frame
	void Update () {
        //print("isCloning: " + GameObject.Find("Player").GetComponent<Player>().isCloning);
        //isCloning = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isCloning;
        //print("isCloning: " + isCloning);
        //CheckPointanimator.SetBool("CPressed", isCloning);
        //CheckPointanimator.SetFloat("playerMove", Mathf.Abs(GameObject.Find("Player").GetComponent<Player>().horizontal));
        //CheckPointanimator.SetBool("playerMoved", false);
        CheckPointanimator.SetBool("CPressed", (Input.GetKeyUp(KeyCode.C)));
       CheckPointanimator.SetFloat("playerMovement", (Input.GetAxis("Horizontal")));
       


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && passed == false)
        {
            collision.gameObject.GetComponent<Player>().cloneLimit = 0;
            collision.gameObject.GetComponent<Player>().cloneCount = 0;
            collision.gameObject.GetComponent<Player>().clones.Clear();
            collision.gameObject.GetComponent<Player>().home = this.transform.position;
            GameObject.FindGameObjectWithTag("cloneLabel").GetComponent<Text>().text = "CLONES COLLECTED: " + collision.gameObject.GetComponent<Player>().cloneLimit;
            GameObject.FindGameObjectWithTag("deployedLabel").GetComponent<Text>().text = "CLONES DEPLOYED: " + collision.gameObject.GetComponent<Player>().cloneCount + " / " + collision.gameObject.GetComponent<Player>().cloneLimit;
            passed = true;
        }
        if (collision.gameObject.tag == "Player" && passed == true)
        {
            collision.gameObject.GetComponent<Player>().home = this.transform.position;
            CheckPointanimator.SetBool("playerMoved", true);
            print("collided again");
        }
    }
}
