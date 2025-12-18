using Godot;
using System;

public partial class wolf_body : Area2D
{
		public void OnBodyEntered(Node2D body)
	{
		GD.Print("in the zone");
		if (body is Player)
		{
			((Player) body).QueueFree();
		}
	}

}
