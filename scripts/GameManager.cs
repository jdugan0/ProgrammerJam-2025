using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager instance;
    public Movement player;
    [Export] public int levelUnlocked = 1; 
    public override void _Ready()
    {
        instance = this;
    }

    public Movement.MovementState GetMovementState()
    {
        if (player == null)
        {
            return Movement.MovementState.SIDE;
        }
        return player.GetMovementState();
    }
}
