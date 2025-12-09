using Godot;
using System;

public partial class WereWolf : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
<<<<<<< HEAD
	public AnimatedSprite2D sprite;


	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

=======
>>>>>>> 8ef06fc7937d01d1123039b45510c7038127821b

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

<<<<<<< HEAD
		
		
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.




		//mr. lemke's example
		var animation = sprite.Animation;
		


		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			sprite.Play("Forward Walk");
=======
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
>>>>>>> 8ef06fc7937d01d1123039b45510c7038127821b
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
<<<<<<< HEAD
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
=======
>>>>>>> 8ef06fc7937d01d1123039b45510c7038127821b
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
