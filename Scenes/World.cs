using Godot;
using System;

public partial class World : Node2D
{
	[Export]
	private Player player1;
	[Export]
	private Player player2;
	[Export]
	private Player player3;
	[Export]
	private Player werewolf;
	[Export]
	private Camera2D camera;
	[Export]
	public int playerID = 0;
	
	public override void _Ready(){
		if(playerID==0){
			camera.Reparent(player1);
			camera.Position=Vector2.Zero;
		}
		if(playerID==1){
			camera.Reparent(player2);
			camera.Position=Vector2.Zero;
		}
		if(playerID==2){
			camera.Reparent(player3);
			camera.Position=Vector2.Zero;
		}
		if(playerID==3){
			camera.Reparent(werewolf);
			camera.Position=Vector2.Zero;
		}
	}
}
