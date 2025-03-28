using System;
using UnityEngine;

[Serializable]
public class PlayerAirborneData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseForceJump { get; private set; } = 10f;
    [field: SerializeField] public PlayerJumpData JumpData { get; private set; }
}
