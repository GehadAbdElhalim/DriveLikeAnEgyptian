using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class walkTo : MonoBehaviour {
    public Transform goal;
    public Animator anim;
    public Vector3 player;
    //public Vector3 CarAI;

	// Use this for initialization
	void Start () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        //CarAI = GameObject.FindGameObjectsWithTag("CARAI")[0].transform.position;
        agent.speed = Random.Range(3, 5);
        //anim.SetBool("IsRun", true);
    }

    void Update(){
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        Vector3 distance = this.transform.position - goal.transform.position;

        /*Vector3 distance2 = transform.position - player;

        Vector3 distance3 = transform.position - CarAI;

        if(distance2.magnitude < 3)
        {
            Debug.Log("why");
            anim.SetBool("IsJump", true);
            agent.Stop();    
        }
        else
        {
            agent.destination = goal.position;
            anim.SetBool("IsRun", true);
        }

        if(distance3.magnitude < 0.5)
        {
            agent.destination = agent.transform.position;
            anim.SetBool("IsRun", false);
        }
        else
        {
            agent.destination = goal.position;
            anim.SetBool("IsRun", true);
        }*/

        if (distance.magnitude < 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            anim.SetBool("IsRun", true);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1.5)
            {
                Debug.Log("hello");
                anim.SetBool("IsRun", false);
                anim.SetBool("IsDead", true);
                this.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            }
        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
            Debug.Log("trigger");
        }
    }*/
}
