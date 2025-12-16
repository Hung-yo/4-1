using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player player;
    public GameManager gameManager;
    public PlayerCamera playerCamera;
    public KeyCode jumpKey = KeyCode.Space;
    [Header("MouseMovement")]
    public float mouseSensitivity = 500f;
    private float yRotation;
    private float xRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.gameManager;
        player = gameManager.player;
        xRotation = 0f;
        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.player == null || GameManager.isPaused || !GameManager.isGameStarted || GameManager.godSceneActive)
        {
            return;
        }
        else
        {
            // WASD Movement
            if (Input.GetKey(KeyCode.W))
            {
                player.MoveForward(player.moveSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                player.MoveForward(-player.moveSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                player.MoveRight(player.moveSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                player.MoveRight(-player.moveSpeed);
            }

            // Rotate on mouse movement
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            player.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            
            if (Input.GetKeyDown(jumpKey))
            {
                player.Jump();
            }
        }
    }
}
