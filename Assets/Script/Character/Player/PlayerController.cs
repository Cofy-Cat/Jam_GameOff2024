using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float maxDashClickGap = 0.3f;
    [SerializeField] private float maxComboAttackGap = 0.2f;
    public AudioClip moveClip;

    private Vector2 _lastMoveInput = Vector2.zero;
    public Vector2 LastMoveInput => _lastMoveInput;
   
}