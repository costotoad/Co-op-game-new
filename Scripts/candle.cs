using Godot;
using System;

public partial class candle : Area2D{
	private AnimatedSprite2D player_sprite;
	override public void _Ready()
{
	player_sprite = GetNode<AnimatedSprite2D>("Candle7");
}
public void OnBodyEntered(Node2D body)
{
	GD.Print("in the zone");
	if (body is ExamplePlayer)
	{
		((ExamplePlayer)body).MakeVisible();
	}
}

public void OnBodyExited(Node2D body)
	{
		GD.Print("out of the zone");
		if (body is ExamplePlayer)
		{
			((ExamplePlayer) body).MakeInvisible();
		}
	}
}
