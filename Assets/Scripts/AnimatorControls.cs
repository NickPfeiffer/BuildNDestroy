using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControls : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float f = Input.GetAxis("Vertical");
        animator.SetFloat("Forward", f);

        float t = Input.GetAxis("Horizontal");
        animator.SetFloat("Turn", t);
    }
}
