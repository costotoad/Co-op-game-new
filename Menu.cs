using Godot;
using System;
public partial class Menu : Node
{
	private Sprite2D titleScreen;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		titleScreen=GetNode<Sprite2D>("HighkeyTrashTitleScreen") as Sprite2D;
		GetTree().Paused=true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("start")){
			GD.Print("EFS");
			start_button();
		}
	}
	
	public void start_button()
	{
		GD.Print("Pressed");
		titleScreen.Visible=false;
		GetTree().Paused=false;
		QueueFree();
	}
		
		
}
