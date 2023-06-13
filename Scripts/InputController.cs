using UnityEngine;

public class InputController : MonoBehaviour
{
    private PlayerInput _input;
    private IControllable _controllable;

    private void Awake()
    {
        _controllable = GetComponent<IControllable>();
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Gameplay.StrafeLeft.performed += OnStrafeLeftPerformed;
        _input.Gameplay.StrafeRight.performed += OnStrafeRightPerformed;
        _input.Gameplay.Jump.performed += OnJumpPerformed;
        _input.Gameplay.Slide.performed += OnSlidePerfomed;
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Gameplay.StrafeLeft.performed -= OnStrafeLeftPerformed;
        _input.Gameplay.StrafeRight.performed -= OnStrafeRightPerformed;
        _input.Gameplay.Jump.performed -= OnJumpPerformed;
        _input.Gameplay.Slide.performed -= OnSlidePerfomed;
    }

    private void OnSlidePerfomed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _controllable.Slide();
    }

    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _controllable.Jump();
    }

    private void OnStrafeRightPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _controllable.Strafe(Direction.Right);
    }

    private void OnStrafeLeftPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _controllable.Strafe(Direction.Left);
    }
}
