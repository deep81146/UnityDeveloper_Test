using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    bool CanChangeDirection;
    public float speed = 6f;
    public float rotationSpeed = 700f;
    public float jumpForce = 5f;

    private bool isGrounded;

    public Rigidbody rb;
    public Animator animator;
    public Transform hologramParent;
    public GameObject[] Holograms;

    private void Start()
    {
        CanChangeDirection = false;
    }

    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            PlayerControls();
            DisplayHologram();
        }
    }

    public void DisplayHologram()
    {
        hologramParent.transform.position = transform.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            CanChangeDirection = true;
            GravityController.instance.direction = GravityController.Direction.Up;
            for (int i = 0; i < Holograms.Length; i++)
            {
                Holograms[i].SetActive(false);
            }

            Holograms[0].SetActive(true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            CanChangeDirection = true;
            GravityController.instance.direction = GravityController.Direction.Down;
            for (int i = 0; i < Holograms.Length; i++)
            {
                Holograms[i].SetActive(false);
            }

            Holograms[1].SetActive(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            CanChangeDirection = true;
            GravityController.instance.direction = GravityController.Direction.Left;
            for (int i = 0; i < Holograms.Length; i++)
            {
                Holograms[i].SetActive(false);
            }

            Holograms[2].SetActive(true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            CanChangeDirection = true;
            GravityController.instance.direction = GravityController.Direction.Right;
            for (int i = 0; i < Holograms.Length; i++)
            {
                Holograms[i].SetActive(false);
            }

            Holograms[3].SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.V) && CanChangeDirection)
        {
            DisableAllHolograms();
            GravityController.instance.ChangeGravityDirection();
        }
    }

    void DisableAllHolograms()
    {
        for (int i = 0; i < Holograms.Length; i++)
        {
            Holograms[i].SetActive(false);
        }
    }

    public void PlayerControls()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;

        if (move.magnitude >= 0.1f)
        {
            animator.SetBool("IsMoving", true);
            transform.Translate(move * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * rotationSpeed);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        animator.SetBool("IsGrounded", isGrounded);
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Targets")
        {
            Destroy(other.gameObject);
            GameManager.instance.CheckGameIsWin();
        }

        if(other.tag == "DeadLine")
        {
            GameManager.instance.GameOver();
        }
    }
}
