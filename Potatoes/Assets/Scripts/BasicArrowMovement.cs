using UnityEngine;
//using Pathfinding;
using System.Collections;

public class BasicArrowMovement : MonoBehaviour
{

    public float maxMoveSpeed;
    bool facingRight = true;
    //public Bounds myBounds;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    Animator anim;
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //var guo = new GraphUpdateObject(GetComponent<Collider2D>().bounds);
       // var guo = new GraphUpdateObject(myBounds);
        //guo.updatePhysics = true;
        //AstarPath.active.UpdateGraphs(guo);

        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        //anim.SetFloat("Speed", Mathf.Abs(moveHorizontal));


        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxisRaw("Vertical");
        //float moving = Mathf.Abs(rb2d.velocity.x);
        anim.SetFloat("Speed", rb2d.velocity.magnitude); // calculate speed of the unit in any direction and set animation Speed value to it
        
        if (rb2d.velocity.x > 0 && !facingRight) //if not looking right and walking right, look right
            Flip();
        else if (rb2d.velocity.x < 0 && facingRight) //if looking left and looking left, look left
            Flip();

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.velocity = new Vector2(moveHorizontal * maxMoveSpeed, moveVertical * maxMoveSpeed);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}