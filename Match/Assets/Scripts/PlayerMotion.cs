using UnityEngine;

public class PlayerMotion : MonoBehaviour {

    // Player Variables
    private Vector3 current_velocity;
    private Vector3 current_rotation;
    private Vector3 current_camera_rotation;
    
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
    public void CameraRotate(Vector3 camera_rotation) { current_camera_rotation = camera_rotation;  }

    private void Moving()
    {
        if (current_velocity != Vector3.zero)
        {
            Vector3 new_position = transform.position + (current_velocity * Time.fixedDeltaTime);
            rb.MovePosition(new_position);
        }
    }

    private void Rotation()
    {
        Quaternion new_rotation = transform.rotation * Quaternion.Euler(current_rotation); 
        rb.MoveRotation(new_rotation);

        player_camera.transform.Rotate(current_camera_rotation); 
    }
}
