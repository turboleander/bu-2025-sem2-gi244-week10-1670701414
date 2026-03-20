using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float currentSpeed;
    public float normalSpeed = 10f;
    public float sprintSpeed = 20f;

    private float leftBound = -15;

    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * currentSpeed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (playerController.isSprint)
        {
            currentSpeed = sprintSpeed;
        }
        else if (!playerController.isSprint)
        {
            currentSpeed = normalSpeed;
        }
    }
    
}
