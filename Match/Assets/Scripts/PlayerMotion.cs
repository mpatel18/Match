using UnityEngine;

public class PlayerMotion : MonoBehaviour {

    // Player Variables
    private Vector3 current_velocity;
    private Vector3 current_rotation;
    private float camera_rotationX;
    private float current_camera_rotationX;
    private Vector3 current_applied_force;

    [SerializeField]
    private float camera_limit; 
    
    // Player Components 
    private Rigidbody rb;

    // Player Children
    [SerializeField]
    private Camera player_camera; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Moving();
        Rotation(); 
    }

    public void Move(Vector3 velocity){ current_velocity = velocity; }
    public void Rotate(Vector3 rotation) { current_rotation = rotation; }
    public void CameraRotate(float camera_rotation) { camera_rotationX = camera_rotation;  }
    public void Force(Vector3 applied_force) { current_applied_force = applied_force;  }

    private void Moving()
    {
        if (current_velocity != Vector3.zero)
        {
            Vector3 new_position = transform.position + (current_velocity * Time.fixedDeltaTime);
            rb.MovePosition(new_position);
        }

        if(current_applied_force != Vector3.zero)
        {
            rb.AddForce(current_applied_force * Time.fixedDeltaTime, ForceMode.Acceleration); 
        }
    }

    private void Rotation()
    {
        Quaternion new_rotation = transform.rotation * Quaternion.Euler(current_rotation); 
        rb.MoveRotation(new_rotation);
        
        if(player_camera != null)
        {
            current_camera_rotationX -= camera_rotationX;
            current_camera_rotationX = Mathf.Clamp(current_camera_rotationX, -camera_limit, camera_limit);

            player_camera.transform.localEulerAngles = new Vector3(current_camera_rotationX, 0f, 0f); 
        }
    }
}
