using Godot;
using System;

public partial class ExamplePlayer : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;

	private AnimatedSprite2D sprite;
	private PointLight2D flashlight;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		flashlight = GetNode<PointLight2D>("PointLight2D");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;

		var direction = Input.GetVector("p1_left", "p1_right", "p1_up", "p1_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
			sprite.FlipH = velocity.X < 0;
		}
		else
		{
			velocity = Vector2.Zero;
		}

		Velocity = velocity;
		MoveAndSlide();
		Rotation = Velocity.Angle() + Mathf.DegToRad(0);

		if (Velocity == Vector2.Zero) 
		{
			flashlight.Visible = false;
		}
		else 
		{
			flashlight.Visible = true;
		}
	}
	
	public void MakeVisible() {
		
	}
}
