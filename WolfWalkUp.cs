using Godot;
using System;

public partial class WolfMovement : CharacterBody2D
{
	[Export] public float MoveSpeed = 200f;

	private AnimatedSprite2D anim;  // Reference to the animation node

	public override void _Ready()
	{
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;

		// --- INPUT (WASD / Arrow Keys) ---
		if (Input.IsActionPressed("ui_right"))
			velocity.X += 1;
		if (Input.IsActionPressed("ui_left"))
			velocity.X -= 1;
		if (Input.IsActionPressed("ui_down"))
			velocity.Y += 1;
		if (Input.IsActionPressed("ui_up"))
			velocity.Y -= 1;

		velocity = velocity.Normalized() * MoveSpeed;

		// --- MOVEMENT ---
		Velocity = velocity;
		MoveAndSlide();

		// --- ANIMATION SELECTION ---
		if (velocity == Vector2.Zero)
		{
			anim.Stop();
		}
		else
		{
			// Determine dominant axis (horizontal vs vertical)
			if (Math.Abs(velocity.X) > Math.Abs(velocity.Y))
			{
				if (velocity.X > 0)
					anim.Play("wolf-walk-right");
				else
					anim.Play("wolf-walk-left");
			}
			else
			{
				if (velocity.Y > 0)
					anim.Play("wolf-walk-down");
				else
					anim.Play("wolf-walk-up");
			}
		}
	}
}
