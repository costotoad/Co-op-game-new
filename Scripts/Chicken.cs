using Godot;
using System;

public partial class Chicken : Area2D
{
	public override void _Ready()
	{
		// start invisible, mark as food
		Visible = false;
		AddToGroup("food");

		// ensure we are monitoring so GetOverlapping* works if you rely on code checks
		Monitoring = true;
		Monitorable = true;
	}

	public override void _PhysicsProcess(double delta)
	{
		// If an enemy or werewolf overlaps the chicken, remove it.
		// This covers both body and area overlaps.
		foreach (Node node in GetOverlappingAreas())
		{
			if (node is Area2D a && (a.IsInGroup("enemy") || a.IsInGroup("werewolf")))
			{
				QueueFree();
				return;
			}
		}

		foreach (Node node in GetOverlappingBodies())
		{
			if (node is Node2D b && (b.IsInGroup("enemy") || b.IsInGroup("werewolf")))
			{
				QueueFree();
				return;
			}
		}
	}
}
