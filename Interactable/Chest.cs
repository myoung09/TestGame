using Godot;
using System;

public partial class Chest : Node2D
{
	private AnimatedSprite2D animationNode;
	private Area2D interactiveArea;
	private bool isOpen = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationNode = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		interactiveArea = GetNode<Area2D>("InteractiveBubble");
		interactiveArea.BodyEntered += InteractiveArea_BodyEntered;
	}

    private void InteractiveArea_BodyEntered(Node2D body)
    {
        if (body is Player player && !isOpen) {
			animationNode.Play("Chest_Open");
			player.AddCoins(5);
			isOpen = true;
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	
	}
	 
}
