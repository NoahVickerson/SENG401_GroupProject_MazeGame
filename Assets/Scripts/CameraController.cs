using UnityEngine;

public class CameraController : MonoBehaviour
{
  public void SetCameraPosition(Vector2 position){
    Camera.main.transform.position = new Vector3(position.x, position.y, -100.0f);
  }
  
  public void SetCameraSize(float size){
    Camera.main.orthographicSize = size;
  }
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
