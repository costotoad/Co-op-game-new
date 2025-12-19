using Godot;

public partial class Timer : Node2D
{
	private const float TOTAL_TIME = 60f;

	private Godot.Timer countdown;
	private TextureProgressBar bar;

	public override void _Ready()
	{
		countdown = GetNode<Godot.Timer>("Countdown");
		bar = GetNode<TextureProgressBar>("Control/TextureProgressBar");

		countdown.Stop();
		countdown.WaitTime = TOTAL_TIME;
		countdown.OneShot = true;
		countdown.Autostart = false;
		countdown.Timeout += OnTimeout;
		countdown.Start();

		bar.MinValue = 0;
		bar.MaxValue = TOTAL_TIME;
		bar.Value = TOTAL_TIME;
	}

	public override void _Process(double delta)
	{
		bar.Value = countdown.TimeLeft;
	}

	private void OnTimeout()
	{
		GetTree().ReloadCurrentScene();
	}
}
