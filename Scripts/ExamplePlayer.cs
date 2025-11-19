using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int ID = 0;

	private AnimatedSprite2D sprite;
	private PointLight2D flashlight;

	public const float Speed = 300f;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		flashlight = GetNode<PointLight2D>("PointLight2D");

		Modulate = GetRandomColor();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;

		float leftX = Input.GetJoyAxis(ID, JoyAxis.LeftX);
		float leftY = Input.GetJoyAxis(ID, JoyAxis.LeftY);

		if (Mathf.Abs(leftX) < 0.2f) leftX = 0;
		if (Mathf.Abs(leftY) < 0.2f) leftY = 0;

		Vector2 direction = new Vector2(leftX, leftY);

		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
			sprite.FlipH = velocity.X < 0;
		}

		Velocity = velocity;
		MoveAndSlide();

		if (velocity != Vector2.Zero)
		{
			Rotation = velocity.Angle();
			flashlight.Visible = true;
		}
		else
		{
			flashlight.Visible = false;
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.Device != ID)
			return;

		if (@event.IsActionPressed("ui_accept"))
		{
			GD.Print($"Player {ID} pressed accept.");
		}
	}

	private Color GetRandomColor()
	{
		return new Color(
			GD.Randf(),
			GD.Randf(),
			GD.Randf(),
			1f
		);
	}
}
