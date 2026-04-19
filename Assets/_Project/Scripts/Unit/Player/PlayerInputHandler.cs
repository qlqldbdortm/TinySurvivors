using System;
using TinySurvivors.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TinySurvivors.Unit.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        public event Action<Vector2> OnMove;
        private InputAction move;

        private void Awake()
        {
            playerInput ??= FindAnyObjectByType<PlayerInput>();

            var actions = playerInput.actions;

            move = actions["Move"];
        }

        private void OnEnable()
        {
            move.AddInputAction(Move, true, true, true);
            move.Enable();
        }

        private void OnDisable()
        {
            move.RemoveInputAction(Move, true, true, true);
            move.Disable();
        }

        private void Move(InputAction.CallbackContext ctx)
        {
            OnMove?.Invoke(ctx.ReadValue<Vector2>());
        }

# if UNITY_EDITOR
        private void Reset()
        {
            playerInput = FindAnyObjectByType<PlayerInput>();
        }
# endif
    }
}