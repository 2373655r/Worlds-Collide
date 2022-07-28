using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

    public GameObject right_body;
    public Transform left_location;
    public GameObject right_player;
    public GameObject left_player;

    public Camera leftcam;
    public Camera rightcam;
    public GameObject merge_effect;

    public float merge_gravity = 0.5f;
    public float merge_time = 20f;
    Vector2 middle;
    bool merged = false;
    bool unmerging = false;
    GameObject clone;
    public float distance_between_LR = 20f;


    private void Start()
    {
        merged = true;
        rightcam.enabled = false;
        leftcam.rect = new Rect(0, 0, 1, 1);
        leftcam.orthographicSize = 8;
        leftcam.transform.position = leftcam.transform.position + new Vector3(0, -0.5f, 0);
        clone = Instantiate(right_body, left_location);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Merge"))
        {
            if (merged && merging == false && unmerging == false)
            {
                Unmerge();
                merged = false;
            } else if (!merged && merging == false && unmerging == false)
            {

                StartCoroutine(Merge());
                merged = true;
            }
        }
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}

    // Merge right level onto left
    IEnumerator Merge() {
        clone = Instantiate(right_body, left_location);
        //Put right player on left world
        right_player.transform.position = right_player.transform.position - new Vector3(distance_between_LR, 0, 0);
        //right_player.GetComponent(BoxCollider2D).enabled = false;

        //lower gravity off
        Rigidbody2D rb = right_player.GetComponent<Rigidbody2D>();
        float previous_gravity = rb.gravityScale;
        rb.gravityScale = merge_gravity;
        Rigidbody2D rbL = left_player.GetComponent<Rigidbody2D>();
        rbL.gravityScale = merge_gravity;

        //turn right player colliders off
        BoxCollider2D bc = right_player.GetComponent<BoxCollider2D>();
        bc.isTrigger = true;
        CircleCollider2D cc = right_player.GetComponent<CircleCollider2D>();
        cc.enabled = false;


        //switch to left cam
        rightcam.enabled = false;
        leftcam.rect = new Rect(0, 0, 1, 1);
        leftcam.orthographicSize = 8;
        leftcam.transform.position = leftcam.transform.position +  new Vector3(0, -0.5f, 0);

        //Animation
        merging = true;
        curTime = 0f;

        //Sound effect
        FindObjectOfType<AudioManager>().Play("merge");


        while (curTime < merge_time && merging)
        {
            float perc = curTime / merge_time;
            curTime += Time.deltaTime;
            middle = (left_player.transform.position + right_player.transform.position) / 2;
            left_player.transform.position = Vector2.Lerp(left_player.transform.position, middle, perc);
            right_player.transform.position = Vector2.Lerp(right_player.transform.position, middle, perc);
            yield return null;
        }
        //play effect
        Instantiate(merge_effect,left_player.transform);

        //turn gravity on
        rb.gravityScale = previous_gravity;
        rbL.gravityScale = previous_gravity;

        //Disable right player
        
        right_player.SetActive(false);

        //but turn his colliders and scripts on

        bc.isTrigger = false;
        cc.enabled = true;

        //put player inbetween the two
        //left_player.transform.position = ((right_player.transform.position + new Vector3(-distance_between_LR,0,0)) + left_player.transform.position) / 2;

        merging = false;
        
    }

    bool merging;
    float curTime;

    // Remove left elements from right
    void Unmerge()
    {
        unmerging = true;

        //Sound effect
        FindObjectOfType<AudioManager>().Play("unmerge");

        Destroy(clone);
        rightcam.enabled = true;
        right_player.transform.position = left_player.transform.position + new Vector3(distance_between_LR,0,0);
        right_player.SetActive(true);
        
        leftcam.rect = new Rect(0, 0, 0.5f, 1);
        leftcam.orthographicSize = 11;
        leftcam.transform.position = leftcam.transform.position + new Vector3(0, +0.5f, 0);
        unmerging = false;
    }
}
