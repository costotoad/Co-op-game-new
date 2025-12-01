using Godot;

public partial class Player : CharacterBody2D
{
	[Export] public int ControllerId = 0;
	[Export] public float Deadzone = 0.35f;
	public const float Speed = 300f;

	private PointLight2D flashlight;

	public override void _Ready()
	{
		flashlight = GetNode<PointLight2D>("flashlight");

		switch (ControllerId)
		{
			case 0:
				flashlight.Color = new Color(1f, 0f, 0.2f);   // Bright red
				break;
			case 1:
				flashlight.Color = new Color(0f, 0f, 1f);   // Bright blue
				break;
			case 2:
				flashlight.Color = new Color(0.5f, 0.5f, 0.2f);       // Yellow
				break;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		float rawX = Input.GetJoyAxis(ControllerId, JoyAxis.LeftX);
		float rawY = Input.GetJoyAxis(ControllerId, JoyAxis.LeftY);

		Vector2 direction = new Vector2(
			Mathf.Abs(rawX) > Deadzone ? rawX : 0f,
			Mathf.Abs(rawY) > Deadzone ? rawY : 0f
		);

		Vector2 velocity = direction != Vector2.Zero
			? direction * Speed
			: Vector2.Zero;

		Velocity = velocity;
		MoveAndSlide();

		Visible = Velocity.Length() > 0.1f;
	}
}
