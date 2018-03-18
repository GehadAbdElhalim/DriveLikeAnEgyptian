using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public Vector3[] Lane;
    public int i;
    //static Animator anim;
    //public float speed;

	// Use this for initialization
	void Start () {
        i = 0;
        //anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (i < Lane.Length)
        {
            Vector3 direction = Lane[i] - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude > 0.5)
            {
                this.transform.Translate(0, 0, 0.1f);
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
