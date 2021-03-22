using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed;
    private float currSpeed;

    public GameObject[] waypoints;
    private int waypointIndex;
    public GameObject playWaypoint;

    private Animator animator;
    public Animator interactionAnimator;
    private SpriteRenderer spr;
    private MonsterStats monsterStats;

    private float moveTimer;
    private bool isPlaying;

    void Start()
    {
        currSpeed = speed;
        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        monsterStats = GetComponent<MonsterStats>();

        animator.SetInteger("State", 0);
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;

        //Play Movement
        if(isPlaying)
        {
            MoveToPlaypoint();
        }
        if(isPlaying && Vector2.Distance(transform.position, playWaypoint.transform.position) < 0.01f)
        {
            animator.SetBool("isPlaying", true);
            spr.flipX = false;
            moveTimer = 10f;
            
            StartCoroutine(WaitForPlayTimeToEnd(1.1f)); //time of play animation
        }

        //Normal Movement
        if(!isPlaying && moveTimer <= 0)
        {
            Move();
        }
        if(!isPlaying && Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.01f)
        {
            animator.SetInteger("State", 0);
            SelectNewWaypoint();

            StartCoroutine(PauseMovement(2f));
        }

        //Flip Sprite
        if(!isPlaying)
        {
            FlipSprite(waypoints[waypointIndex].transform);
        }
        else
        {
            FlipSprite(playWaypoint.transform);
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, currSpeed);
        animator.SetInteger("State", 1);
    }

    private void SelectNewWaypoint()
    {
        waypointIndex = Random.Range(0, waypoints.Length);
        moveTimer = 10f;
        currSpeed = 0;

        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    private void MoveToPlaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, playWaypoint.transform.position, currSpeed);
        animator.SetInteger("State", 1);

    }

    private void FlipSprite(Transform target)
    {
        if (transform.position.x > target.position.x)
        {
            spr.flipX = true;
        }
        else
        {
            spr.flipX = false;
        }
    }

    IEnumerator PauseMovement(float time)
    {
        yield return new WaitForSeconds(time);
        currSpeed = speed;
    }

    IEnumerator WaitForPlayTimeToEnd(float time)
    {
        yield return new WaitForSeconds(time);
        
        //animation
        isPlaying = false;
        animator.SetBool("isPlaying", false);
        animator.SetInteger("State", 0);
        interactionAnimator.SetTrigger("Play");

        //update stats
        monsterStats.DecrementHunger(10f);
        monsterStats.DecrementThirst(10f);
        if(monsterStats.CheckHunger() > monsterStats.maxHunger/2 && monsterStats.CheckThirst() > monsterStats.maxThirst/2)
            monsterStats.MakeMonsterHappy(10f);
        
        PlayerManager.money += 25;

        StopAllCoroutines();
    }
    public void ChangeIsPlaying(bool value)
    {
        isPlaying = value;
    }

    public void ChangeSpeed(float newSpeed)
    {
        currSpeed = newSpeed;
    }
}
