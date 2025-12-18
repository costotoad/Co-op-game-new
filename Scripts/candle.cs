using Godot;
using System;

public partial class Candle : Area2D{
	private AnimatedSprite2D player_sprite;
	override public void _Ready()
{
	player_sprite = GetNode<AnimatedSprite2D>("Candle7");
}
public void OnBodyEntered(Node2D body)
{
	GD.Print("in the zone");
	if (body is Player)
	{
		((Player)body).Visible = true;
	}
}

public void OnBodyExited(Node2D body)
	{
		GD.Print("out of the zone");
		if (body is Player)
		{
			((Player) body).Visible = false;
		}
	}
}
