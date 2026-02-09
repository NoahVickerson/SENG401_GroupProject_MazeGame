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

  private Vector2 targetPosition;
  private float targetDistance;

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
      float curDist = Vector2.Distance(mouseClickPosition, (Vector2)player.transform.position);
      if(curDist < 1E-3){
        targetPosition.x = -1;
        targetPosition.y = -1;
        return;
      }
      float velocityScalar = Math.Abs((float)(-1*Math.Pow(curDist - targetDistance/2, 2) + maxVel));
      velocityScalar = 0.2f;
      velocity = velocityScalar*(mouseClickPosition - (Vector2)player.transform.position)/curDist;
    }
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      player = gameObject;
        
      velocity = new Vector2(0,0);
      targetPosition = new Vector2(-1, -1);
      pressedDirections = new List<Directions>();

      cam.SetCameraPosition(new Vector2(0,0));
    }

    // Update is called once per frame
    void Update()
    {
      pressedDirections.Clear();

      if(Mouse.current.leftButton.wasPressedThisFrame){
        // read the mouse click point
        Vector3 mousePos = Mouse.current.position.ReadValue();

        // convert that to a point on the screen
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        targetPosition.x = mousePos.x;
        targetPosition.y = mousePos.y;
        targetDistance = Vector2.Distance(targetPosition, (Vector2)player.transform.position);
      }else{

        if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed){
          pressedDirections.Add(Directions.RIGHT);
        }
        else if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed){
          pressedDirections.Add(Directions.LEFT);
        }
        if(Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed){
          pressedDirections.Add(Directions.UP);
        }
        else if(Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed){
          pressedDirections.Add(Directions.DOWN);
        }
      }

      if(pressedDirections.Count > 0){
        updateVelocity(pressedDirections);
        targetPosition.x = -1;
        targetPosition.y = -1;
      }
      else if(targetPosition.x > 0 && targetPosition.y > 0)
        updateVelocity(targetPosition);
      else
        updateVelocity(pressedDirections);
      updatePosition();
    }

}
