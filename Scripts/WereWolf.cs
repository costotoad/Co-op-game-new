using Godot;

public partial class WereWolf : CharacterBody2D
{
	public const float Speed = 300f;
	[Export] public int ControllerId = -1; // 3 = player-controlled

	private AnimatedSprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		// Connect DeathArea signal
		var deathArea = GetNode<Area2D>("DeathArea");
		deathArea.BodyEntered += OnDeathAreaEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Vector2.Zero;

		// Player control only if ControllerId == 3
		if (ControllerId == 3)
		{
			float rawX = Input.GetJoyAxis(ControllerId, JoyAxis.LeftX);
			float rawY = Input.GetJoyAxis(ControllerId, JoyAxis.LeftY);

			direction = new Vector2(
				Mathf.Abs(rawX) > 0.35f ? rawX : 0f,
				Mathf.Abs(rawY) > 0.35f ? rawY : 0f
			);
		}

		Velocity = direction * Speed;
		MoveAndSlide();

		if (direction != Vector2.Zero)
			sprite.Play("Forward Walk");
	}

	private void OnDeathAreaEntered(Node body)
	{
		if (body is Player player) {
			player.Die(player);
			GD.Print("Player died!");
			}
	}
}
