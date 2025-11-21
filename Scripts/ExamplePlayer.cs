using Godot;
using System;
using System.Collections.Generic;

public partial class ExamplePlayer : CharacterBody2D
{
	private static HashSet<long> UsedControllers = new HashSet<long>();
	private long JoypadID = -1;

	[Export] public float Speed = 300.0f;

	private AnimatedSprite2D sprite;
	private PointLight2D flashlight;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		flashlight = GetNode<PointLight2D>("PointLight2D");
		flashlight.Color = Colors.White;
		flashlight.Visible = false;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (JoypadID == -1)
		{
			CheckForJoin();
			return;
		}

		int joyID = (int)JoypadID;

		float x = Input.GetJoyAxis(joyID, JoyAxis.LeftX);
		float y = Input.GetJoyAxis(joyID, JoyAxis.LeftY);

		if (Mathf.Abs(x) < 0.2f) x = 0;
		if (Mathf.Abs(y) < 0.2f) y = 0;

		Vector2 direction = new Vector2(x, y);
		Vector2 velocity = direction * Speed;

		Velocity = velocity;
		MoveAndSlide();
		Rotation = Velocity.Angle();

		sprite.FlipH = velocity.X < 0;
		flashlight.Visible = velocity != Vector2.Zero;
	}

	private void CheckForJoin()
	{
		var pads = Input.GetConnectedJoypads();
		foreach (long pad in pads)
		{
			if (UsedControllers.Contains(pad))
				continue;

			int joyID = (int)pad;
			for (int button = 0; button <= (int)JoyButton.Max; button++)
			{
				if (Input.IsJoyButtonPressed(joyID, (JoyButton)button))
				{
					AssignPad(pad);
					return;
				}
			}
		}
	}

	private void AssignPad(long pad)
	{
		JoypadID = pad;
		UsedControllers.Add(pad);

		flashlight.Color = GetColorFromID((int)pad);
		flashlight.Visible = true;

		// Adjust PointLight2D energy per hue
		float hue = ((int)pad * 0.25f) % 1f;
		float baseEnergy = 2.0f;

		if (hue < 0.1f || hue > 0.9f)      // red hues
			flashlight.Energy = baseEnergy * 1.5f; // boost red
		else if (hue > 0.12f && hue < 0.25f) // yellow hues
			flashlight.Energy = baseEnergy * 0.85f; // slightly dim yellow
		else if (hue > 0.6f && hue < 0.8f) // purple/blue hues
			flashlight.Energy = baseEnergy * 1.15f; // slight boost
		else
			flashlight.Energy = baseEnergy;        // green default

		GD.Print($"Player joined with controller {pad}");
	}

	private Color GetColorFromID(int id)
	{
		float hue = (id * 0.25f) % 1f;
		float saturation = 1f;
		float value = 1f;

		// Red boost
		if (hue < 0.1f || hue > 0.9f)
			value = 5f;

		// Yellow duller
		else if (hue > 0.12f && hue < 0.25f)
		{
			value = 0.7f;
			saturation = 0.85f;
		}

		Color color = Color.FromHsv(hue, saturation, value);

		// Perceptual luminance correction for other hues
		float luminance = 0.2126f * color.R + 0.7152f * color.G + 0.0722f * color.B;
		float targetBrightness = 0.8f;

		if (hue > 0.6f && hue < 0.8f) // purple/blue
			targetBrightness *= 1.1f;

		if (luminance > 0 && !(hue < 0.1f || hue > 0.9f || (hue > 0.12f && hue < 0.25f)))
		{
			float scale = targetBrightness / luminance;
			color.R *= scale;
			color.G *= scale;
			color.B *= scale;
		}

		color.A = 0.9f;
		return color;
	}
}
