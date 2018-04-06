using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

    public Vector3[] Lane;
    public int i;
    //static Animator anim;
    //public float speed;

	// Use this for initialization
	void Start () {
        i = 0;
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		//agent.Warp (new Vector3 (0, 0, 0));
		agent.destination = Lane[0];
		agent.speed = 0.01f;
        //anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (i < Lane.Length)
        {
			NavMeshAgent agent = GetComponent<NavMeshAgent>();
            Vector3 direction = Lane[i] - this.transform.position;
			direction.y = 0;

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude > 5)
            {
				agent.destination = Lane[i];
                //anim.SetBool("IsRun", true);
                //print("hi");
            }
            else
            {
                i++;
                //anim.SetBool("IsRun", false);
            }
        }
	}
}
