using Godot;

public partial class Main : Node2D
{
	[Export] public PackedScene World1Scene { get; set; }
	[Export] public PackedScene World2Scene { get; set; }
	[Export] public PackedScene World3Scene { get; set; }
	[Export] public PackedScene World4Scene { get; set; }

	public override void _Ready()
	{
		LoadWorldIntoViewport("Viewports/VC1/SubViewport1", World1Scene);
		LoadWorldIntoViewport("Viewports/VC2/SubViewport2", World2Scene);
		LoadWorldIntoViewport("Viewports/VC3/SubViewport3", World3Scene);
		LoadWorldIntoViewport("Viewports/VC4/SubViewport4", World4Scene);
	}

	private void LoadWorldIntoViewport(string path, PackedScene worldScene)
	{
		//var viewport = GetNode<SubViewport>(path);
		//var worldInstance = worldScene.Instantiate<Node2D>();
		//viewport.AddChild(worldInstance);
	}
}
