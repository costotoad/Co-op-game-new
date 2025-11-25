using Godot;
using System;

public partial class ExamplePlayer : CharacterBody2D
{
	[Export] public int PlayerID = 0;
	[Export] public float Speed = 300.0f;

	private AnimatedSprite2D sprite;
	private PointLight2D flashlight;

	public partial class Player : CharacterBody2D
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		flashlight = GetNode<PointLight2D>("PointLight2D");
		flashlight.Color=GetRandomColor();
	}

	public override void _PhysicsProcess(double delta)
	{
		float x = Input.GetJoyAxis(PlayerID, JoyAxis.LeftX);
		float y = Input.GetJoyAxis(PlayerID, JoyAxis.LeftY);

		if (Mathf.Abs(x) < 0.2f) x = 0;
		if (Mathf.Abs(y) < 0.2f) y = 0;

		Vector2 direction = new Vector2(x, y);

		Vector2 velocity = Velocity;

		if (direction != Vector2.Zero)
		{
		public partial class Player : CharacterBody2D
		Velocity = velocity;
		MoveAndSlide();
		Rotation = Velocity.Angle();

		if (Velocity == Vector2.Zero)
			flashlight.Visible = false;
		else
			flashlight.Visible = true;
	}
	
	public Color GetRandomColor(){
		float r = GD.Randf();
		float g = GD.Randf();
		float b = GD.Randf();
		float a = 5;
		
		return new Color(r, g, b, a);
	}
}
