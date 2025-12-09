using Godot;
using System;
HighkeyTrashTitleScreen = sprite
public partial class Menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void start_button()
	{
		GD.Print("Pressed");
		HighkeyTrashTitleScreen.Visible=false;
	}
		
		
}
