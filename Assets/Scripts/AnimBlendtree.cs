using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBlendtree : MonoBehaviour
{
    public GameObject character;
    public Animator anim;

    void Start()
    {

        anim = GetComponent<Animator>();
        character.SetActive(true);
    }

    void Update()
    {
        anim.SetFloat("Speed", PlayerLocomotionHandler.playerVelocity);
    }
}
