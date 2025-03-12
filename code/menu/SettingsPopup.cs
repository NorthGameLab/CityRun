using Godot;
using System;

public partial class SettingsPopup : Window
{
	public Window PopupWindow;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        PopupWindow.Visible = false;
		this.CloseRequested += OnCloseRequested;
	}
	private void OnSettingsButtonPressed()
    {
        PopupWindow.Visible = true;
    }
    private void OnCloseRequested()
    {
        Hide();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
