using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    public LayerMask solidObjectLayers;
    public LayerMask interactableLayers;

    void Awake(){
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(Game.state == Game.State.Dialog || Game.state == Game.State.OpenUI){
            return;
        }
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //Only walking in 1 direction
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
                
            }
        }

        animator.SetBool("isWalking", isMoving);
    }

    IEnumerator Move(Vector3 targerPos)
    {
        isMoving = true;

        while ((targerPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            //Time.deltaTime: ensure the moveSpeed is constant for different frame rate
            transform.position = Vector3.MoveTowards(transform.position, targerPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targerPos;

        isMoving = false;
    }

    public void Interact(){
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayers);
        if(collider != null){
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    private bool IsWalkable(Vector3 targetPos){
        if(Physics2D.OverlapCircle(targetPos, 0.05f, solidObjectLayers | interactableLayers) != null){
            return false;
        }
        return true;
    }

}
