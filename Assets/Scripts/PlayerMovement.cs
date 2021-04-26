using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed;
    public float runSpeed;
    public float gravity;

    public float jumpHeight = 3f;

    //ground check
    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;
    private bool isGrounded;
    
    private Vector3 velocity;

    public float health;
    public Image healthBar;
    
    // Update is called once per frame
    void Update()
    {
        //creates a sphere at the position of the empty groundCheck GameObject with the radius being groundDistance
        //and it's checking if it collides with any object that is in the groundMask
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //force player onto ground if he collides with ground
        }
        
        //moving
        //get move axis (W,S = Vertical, A,D = Horizontal)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        
        //running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * (runSpeed * Time.deltaTime));
        }
        else
        {
            controller.Move(move * (speed * Time.deltaTime));
        }
        

        //jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //falling
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); //multiply by Time twice, because that's how physics work
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / 100;

        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Main Menu");
        }
    }
}
