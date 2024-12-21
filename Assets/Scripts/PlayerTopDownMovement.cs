using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTopDownMovement : MonoBehaviour
{
  [SerializeField]
  float _speed = 1f;

  [SerializeField]
  bool _useEasing = false;

  [SerializeField]
  float _easingSpeed = 2f;

  Rigidbody2D _rigidBody2D;
  PlayerInput _playerInput;

  /**
   * Awake is called once before the application starts
   */
  void Awake()
  {
    _rigidBody2D = GetComponent<Rigidbody2D>();
    _playerInput = GetComponent<PlayerInput>();
  }

  /**
   * Update is called once per frame
   */
  void Update()
  {
    if (_useEasing)
    {
      EasingMovement();
    }
    else
    {
      BaseMovement();
    }
  }

  /**
   * Added easing function
   */
  void EasingMovement()
  {
    Vector2 currentVelocity = _rigidBody2D.linearVelocity;
    Vector2 move = _playerInput.actions["Move"].ReadValue<Vector2>();

    float velocityX = Mathf.Lerp(currentVelocity.x, move.x * _speed, Time.deltaTime * _easingSpeed);
    float velocityY = Mathf.Lerp(currentVelocity.y, move.y * _speed, Time.deltaTime * _easingSpeed);

    _rigidBody2D.linearVelocity = new Vector2(velocityX, velocityY);
  }

  /**
   * Simple movement without any easing or acceleration
   */
  void BaseMovement()
  {
    Vector2 move = _playerInput.actions["Move"].ReadValue<Vector2>();
    _rigidBody2D.linearVelocity = move * _speed;
  }
}
