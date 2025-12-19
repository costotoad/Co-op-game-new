using Godot;

public partial class DeathArea : Area2D
{
	private void OnBodyEntered(Node body)
	{
		if (body is Player player)
		{
			player.Die(player);
			GD.Print("Player died!");
		}
	}
}
