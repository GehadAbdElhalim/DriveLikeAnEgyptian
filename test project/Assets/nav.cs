using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nav : MonoBehaviour
{
    public Transform[] goals;
    //public Animator anim;

    // Use this for initialization
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goals[0].position;
        //anim = GetComponent<Animator>();
        //agent.speed = Random.Range(3, 5);
        //anim.SetBool("IsRun", true);
    }

    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        for (int i = 0; i < goals.Length - 1; i++)
        {
            Vector3 distance = this.transform.position - goals[i].transform.position;
            if (distance.magnitude <= 0.5)
            {
                agent.destination = goals[i + 1].position;
            }
        }
    }

    /*void OnCollisonEnter(Collision other)
    {
        if (other.collider.gameObject.CompareTag("Finish"))
        {
            other.collider.gameObject.SetActive(false);
            Debug.Log("collide");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (other.gameObject.CompareTag(goals[0].tag))
        {
            Debug.Log("heyyyyyyyyyyyy");
            agent.destination = goals[1].position;
        }
    }*/
}
