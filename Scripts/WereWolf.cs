using Godot;
using System;

public partial class WereWolf : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public AnimatedSprite2D sprite;


	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		get_joy_info(device: 0);
		
		Vector2 direction = get_joy_axis(device: 0,)
		
		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.




		//mr. lemke's example
		var animation = sprite.Animation;
		


		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			sprite.Play("Forward Walk");
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			sprite.Play("Forward Walk");
		}

		if (direction != Vector2.Zero)
		{
			velocity.Y = direction.Y * Speed;
			sprite.Play("Forward Walk");
		}
		else
		{
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			sprite.Play("Forward Walk");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
