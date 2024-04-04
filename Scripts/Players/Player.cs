using Godot;

public partial class Player : CharacterBody2D
{
    public float Speed {get; protected set;} = 300.0f;
    public float GravityMultiplier {get; protected set;} = 1f;
    public float PlayerSpeedMultiplier {get; protected set;} = .75f;
    public float JumpSpeed {get; protected set;} = -500f;
    public long Coins {get; protected set;} = 0;

        public void AddCoins(long amount){
        Coins += amount;
        
    }
}