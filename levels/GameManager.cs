using Godot;
using System;

public partial class GameManager : Node
{
    [Export] Movement player;
    public Movement.MovementState GetMovementState()
    {
        if (player == null)
        {
            return Movement.MovementState.SIDE;
        }
        return player.GetMovementState();
    }
}
