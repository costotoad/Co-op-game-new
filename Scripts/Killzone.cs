using Godot;
using System;

public partial class Killzone : Area2D
{
	private AnimatedSprite2D player_sprite;


	public void ready()
	{
		player_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
}
	public void OnBodyEntered(Node2D body)
	{
		GD.Print("in the zone");
		if (body is ExamplePlayer) {
			player_sprite.MakeVisible();
		}
	}
}
