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

	private bool _canMove = true;
	private bool _isDead = false; // Track death state

	public override void _Ready()
	{
		flashlight = GetNode<PointLight2D>("flashlight");
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

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
		if (!_canMove || _isDead)
		{
			Velocity = Vector2.Zero;
			MoveAndSlide();
			return;
		}

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

	private async void FreezeFor3Seconds()
	{
		if (!_canMove) return;

		_canMove = false;

		await ToSignal(GetTree().CreateTimer(3.0), "timeout");

		_canMove = true;
	}

	// ===== Die Method =====
	public async void Die()
	{
		if (_isDead) return;
		_isDead = true;

		_canMove = false;             // Stop movement
		var collision = GetNode<CollisionShape2D>("CollisionShape2D");
		if (collision != null)
			collision.Disabled = true;     // Disable collisions

		// Fade out over 1 second
		for (float t = 1f; t >= 0; t -= 0.05f)
		{
			Modulate = new Color(1, 1, 1, t);
			await ToSignal(GetTree().CreateTimer(0.05f), "timeout");
		}

		QueueFree();                  // Finally remove the player
	}
}
