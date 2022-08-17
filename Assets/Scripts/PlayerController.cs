using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public int health = 5;
    public float speed = 1.0f;
    public Vector3 movement;
    public Rigidbody rb;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            health = 5;
            score = 0;
            SceneManager.LoadScene("maze");
        }

        if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector3.left);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector3.right);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(Vector3.forward);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(Vector3.back);

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            score++;
            Debug.Log($"Score: {score}");
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            Debug.Log($"Health: {health}");
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
    }
}
