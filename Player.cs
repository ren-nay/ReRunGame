using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidBodyPlayer;
    public int speed;
    public Vector2 jump;
    public bool isOnGround;
    public bool doubleJump;
    public BoxCollider2D box;
    public Transform clone;
    public Vector3 home;
    public int cloneCount;
    public int cloneLimit;
    public bool isCloning;
    public int netWorth;
    public float horizontal;
    public List<Transform> clones = new List<Transform>();
    public Animator playerAnimator;
    public SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start()
    {
        rigidBodyPlayer = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        home = new Vector3(0, -1, 0);
        cloneCount = 0;
        cloneLimit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Move left and right

        horizontal = Input.GetAxis("Horizontal");
        Vector2 vector = new Vector2(horizontal, 0);
        rigidBodyPlayer.AddForce(speed * vector);
        playerAnimator.SetFloat("movingDirection", Mathf.Abs(horizontal));

        //stop cloning animation
        if (rigidBodyPlayer.velocity.y > 0 || rigidBodyPlayer.velocity.x != 0)
        {
            playerAnimator.SetBool("isCloning", false);
        }

        //flip sprite
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }


        //Are you on the ground/platform?
        //Middle raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, box.size.y / 2);
        if (hit == true && isOnGround == false)
        {
            if ((hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.name == "frozen(Clone)") && rigidBodyPlayer.velocity.y <= 0)
            {
                isOnGround = true;
                doubleJump = true;
                playerAnimator.SetBool("isJumping", false);
                print("Middle Raycast");
            }
        }
        //Right raycast
        hit = Physics2D.Raycast(transform.position + new Vector3(box.size.x / 2, 0, 0), Vector2.down, box.size.y / 2);
        if (hit == true && isOnGround == false)
        {
            if ((hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.name == "frozen(Clone)") && rigidBodyPlayer.velocity.y <= 0)
            {
                isOnGround = true;
                doubleJump = true;
                playerAnimator.SetBool("isJumping", false);
                print("Right Raycast");
            }
        }
        //Left raycast
        hit = Physics2D.Raycast(transform.position - new Vector3(box.size.x / 2, 0, 0), Vector2.down, box.size.y / 2);
        if (hit == true && isOnGround == false)
        {
            if ((hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.name == "frozen(Clone)") && rigidBodyPlayer.velocity.y <= 0)
            {
                isOnGround = true;
                doubleJump = true;
                playerAnimator.SetBool("isJumping", false);
                print("Left Raycast");
            }
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround == true || doubleJump == true))
        {
            rigidBodyPlayer.AddForce(jump);
            if (isOnGround == false)
            {
                doubleJump = false;
            }
            isOnGround = false;
            playerAnimator.SetBool("isJumping", true);
        }

        if (rigidBodyPlayer.velocity.y < 0)
        {
            playerAnimator.SetBool("isFalling", true);

        }
        else
        {
            playerAnimator.SetBool("isFalling", false);
        }

        //CLONING

        if (Input.GetKeyUp(KeyCode.C) && cloneCount < cloneLimit)
        {
            //Instantiate(clone, rigidBodyPlayer.transform.position, Quaternion.identity);
            clones.Add(Instantiate(clone, rigidBodyPlayer.transform.position, Quaternion.identity));
            cloneCount++;
            //GameObject.FindGameObjectWithTag("cloneLabel").GetComponent<Text>().text = "Clones Left: " + (cloneLimit - cloneCount);
            //CheckPointanimator.SetBool("cPressed", true);
            isCloning = true;
            print("cloning");
            Restart();
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            int last = (clones.Count) - 1;
            Destroy(clones[last].gameObject);
            clones.RemoveAt(last);
            cloneCount--;
            GameObject.FindGameObjectWithTag("deployedLabel").GetComponent<Text>().text = "CLONES DEPLOYED: " + cloneCount + " / " + cloneLimit;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spike")
        {
            Restart();
        }
    }

    private void Restart()
    {
        rigidBodyPlayer.transform.SetPositionAndRotation(home, Quaternion.identity);
        playerAnimator.SetBool("isCloning", true);
        rigidBodyPlayer.velocity = new Vector2(0, 0);
        //CheckPointanimator.SetBool("cPressed", false);
        GameObject.FindGameObjectWithTag("deployedLabel").GetComponent<Text>().text = "CLONES DEPLOYED: " + cloneCount + " / " + cloneLimit;
        isCloning = false;
        print("Clones Count" + clones.Count);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EndGame")
        {
            SceneManager.LoadScene("Main menu");
        }
    }
}