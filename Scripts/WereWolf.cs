using Godot;
using System;

public partial class WereWolf : CharacterBody2D
{
	public const float BaseSpeed = 300.0f;
	public const float JumpVelocity = -400.0f;

	private float currentSpeed = BaseSpeed;
	private Godot.Timer countdown;

	public override void _Ready()
	{
		// Replace the path with your actual Timer node path
		countdown = GetNode<Godot.Timer>("/root/Level/Timer/Countdown");

		// Connect collision detection
		var area = GetNode<Area2D>("Area2D"); // make sure your WereWolf has an Area2D child
		area.BodyEntered += OnBodyEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Double speed when 30 seconds or less
		if (countdown != null)
			currentSpeed = countdown.TimeLeft <= 30 ? BaseSpeed * 2 : BaseSpeed;

		Vector2 velocity = Velocity;

		// Handle Jump
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
			velocity.X = direction.X * currentSpeed;
		else
			velocity.X = Mathf.MoveToward(Velocity.X, 0, currentSpeed);

		Velocity = velocity;
		MoveAndSlide();
	}

	private void OnBodyEntered(Node body)
	{
		if (body.IsInGroup("Player"))
		{
			// Call the Player's Die method to fade out and remove them
			(body as Player)?.Die();
		}
	}
}
