using Godot;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
	[Export] public int ControllerId = 0;
	[Export] public float Deadzone = 0.35f;
	public const float Speed = 300f;

	[Export] public float FlashlightAngleOffsetDeg = 0f;
	[Export] public bool SmoothFlashlight = false;
	[Export] public float SmoothFactor = 0.25f;

	private PointLight2D flashlight;
	private AnimatedSprite2D anim;

	public override void _Ready()
	{
		flashlight = GetNode<PointLight2D>("flashlight");
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		AddToGroup("Player");
		
		switch (ControllerId)
		{
			case 0:
				flashlight.Color = new Color(1f, 0f, 0.2f);
				break;
			case 1:
				flashlight.Color = new Color(0f, 0f, 1f);
				break;
			case 2:
				flashlight.Color = new Color(0.5f, 0.5f, 0.2f);
				break;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = Vector2.Zero;
		MoveAndSlide();

		float rawX = Input.GetJoyAxis(ControllerId, JoyAxis.LeftX);
		float rawY = Input.GetJoyAxis(ControllerId, JoyAxis.LeftY);

		Vector2 direction = new Vector2(
			Mathf.Abs(rawX) > Deadzone ? rawX : 0f,
			Mathf.Abs(rawY) > Deadzone ? rawY : 0f
		);

		Vector2 velocity = direction != Vector2.Zero ? direction * Speed : Vector2.Zero;

		Velocity = velocity;
		MoveAndSlide();

		Visible = Velocity.Length() > 0.1f;

		FlashlightAngleOffsetDeg = -90;
		flashlight.Rotation = Velocity.Angle() + Mathf.DegToRad(FlashlightAngleOffsetDeg);

		if (Mathf.Abs(Velocity.X) > Mathf.Abs(Velocity.Y))
		{
			anim.Play("SideWalk");
			anim.FlipH = Velocity.X < 0;
		}
		else
		{
			if (Velocity.Y < 0)
				anim.Play("UpWalk");
			else
				anim.Play("DownWalk");

			anim.FlipH = false;
		}
	}

	private void BodyEntered(Node2D body)
	{
		body.Visible = true;
	}

	public void Die(Node2D body)
	{
		if (body is Player player)
			body.QueueFree();              
	}
}
