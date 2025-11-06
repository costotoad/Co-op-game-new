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

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
			sprite.FlipH = velocity.X < 0;
			
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		Velocity = velocity;
		MoveAndSlide();
		Rotation = Velocity.Angle() + Mathf.DegToRad(0);
		if (Velocity == Vector2.Zero) {
			flashlight.Visible = false;
			}
		else {
			flashlight.Visible = true;
			}
	}
}
