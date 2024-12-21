using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTopDownMovement : MonoBehaviour
{
  [SerializeField]
  float _speed = 1f;

  [SerializeField]
  bool _useEasing = false;

  // less is slower, around 2f like sliding on ice
  [SerializeField]
  float _easingSpeed = 10f;

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
      EasingMovement();
    else
      BaseMovement();
  }

  /**
   * Added easing function
   */
  void EasingMovement()
  {
    Vector2 currentVelocity = _rigidBody2D.linearVelocity;
    Vector2 move = _playerInput.actions["Move"].ReadValue<Vector2>();
    Vector2 fullVelocity = move * _speed;

    // Interpolate between currentVelocity and fullVelocity
    Vector2 desiredVelocity = Vector2.Lerp(
      currentVelocity,
      fullVelocity,
      Time.deltaTime * _easingSpeed // 0f to 1f
    );

    _rigidBody2D.linearVelocity = desiredVelocity;
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
