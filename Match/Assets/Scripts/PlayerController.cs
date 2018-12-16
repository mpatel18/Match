using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float player_speed;
    [SerializeField]
    private float camera_sensitivity;
    [SerializeField]
    private float player_force; 


    private PlayerMotion movement;

    private void Start()
    {
        movement = GetComponent<PlayerMotion>(); 
    }

    private void Update()
    {
        PlayerMovement();
        CameraRotation();
        Jump();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
        }
    }

    // Move player 
    private void PlayerMovement()
    {
        // Player Movement based on World 
        float x_axis = Input.GetAxisRaw("Horizontal");
        float z_axis = Input.GetAxisRaw("Vertical");

        Vector3 horizontal_move = transform.right * x_axis;
        Vector3 vertical_move = transform.forward * z_axis;

        // Current player movement 
        Vector3 player_movement = (horizontal_move + vertical_move).normalized;
        Vector3 velocity = player_movement * player_speed;

        // Applyed player movement
        movement.Move(velocity);

        // Player Movement based on Camera (Turning player)
        float camera_x_axis = Input.GetAxisRaw("Mouse X");

        // Rotate object on the y_axis based on camera x_axis
        Vector3 rotation = new Vector3(0f, camera_x_axis, 0f) * camera_sensitivity;

        // Apply player rotation
        movement.Rotate(rotation);
    }

    // Rotate Camera
    private void CameraRotation()
    {
        // Player Movement based on Camera (Camera tilt)
        float camera_y_axis = Input.GetAxisRaw("Mouse Y");

        // Rotate object on the y_axis based on camera x_axis
        float camera_rotationX = camera_y_axis * camera_sensitivity;

        // Apply camera rotation
        movement.CameraRotate(camera_rotationX);
    }

    private void Jump()
    {
        Vector3 applied_force = Vector3.zero; 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            applied_force = Vector3.up * player_force; 
        }

        movement.Force(applied_force);
    }
}
