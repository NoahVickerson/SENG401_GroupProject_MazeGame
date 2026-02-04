using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  private GameObject player;

  [SerializeField]
  private CameraController cam;

  [SerializeField]
  private float timeslice;

  [SerializeField]
  private float maxVel;

  [SerializeField]
  private float acceleration;

  [SerializeField]
  private float velDecay;

    public enum Directions{
      UP,
      DOWN,
      LEFT,
      RIGHT
    }
    
    private Vector2  velocity;
    List<Directions> pressedDirections;

    private void updatePosition(){
      player.transform.position += new Vector3(velocity.x*timeslice,velocity.y*timeslice, 0.0f);

      cam.SetCameraPosition(new Vector2(
            player.transform.position.x,
            player.transform.position.y));
    }

    private void updateVelocity(List<Directions> pressedKeys){
      if(pressedKeys.Contains(Directions.DOWN))
        velocity.y -= acceleration*timeslice;
      else if(pressedKeys.Contains(Directions.UP))
        velocity.y += acceleration*timeslice;
      else
        velocity.y /= velDecay;

      if(pressedKeys.Contains(Directions.LEFT))
        velocity.x -= acceleration*timeslice;
      else if(pressedKeys.Contains(Directions.RIGHT))
        velocity.x += acceleration*timeslice;
      else
        velocity.x /= velDecay;

      if(velocity.magnitude > maxVel){
        velocity /= velocity.magnitude;
        velocity *= maxVel;
      }
    }

    private void updateVelocity(Vector2 mouseClickPosition){

    }
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      player = gameObject;
      Debug.Log(player.name);
        
      velocity = new Vector2(0,0);
      pressedDirections = new List<Directions>();

      cam.SetCameraPosition(new Vector2(0,0));
    }

    // Update is called once per frame
    void Update()
    {
      pressedDirections.Clear();

      if(Keyboard.current.dKey.isPressed){
        pressedDirections.Add(Directions.RIGHT);
      }
      else if(Keyboard.current.aKey.isPressed){
        pressedDirections.Add(Directions.LEFT);
      }
      if(Keyboard.current.wKey.isPressed){
        pressedDirections.Add(Directions.UP);
      }
      else if(Keyboard.current.sKey.isPressed){
        pressedDirections.Add(Directions.DOWN);
      }
        
      updateVelocity(pressedDirections);
      updatePosition();
    }

}
