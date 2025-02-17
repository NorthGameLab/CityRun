using Godot;
using System;

public partial class TestA : Label
{
	public float speed = 50.0f;
    public float duration = 2.0f;
    private float elapsedTime = 0.0f;

    public override void _Process(double delta)
    {
        Position += new Vector2(0, -speed * (float)delta);

        elapsedTime += (float)delta;
        Modulate = new Color(1, 1, 1, 1 - (elapsedTime / duration));

        if (elapsedTime >= duration)
            QueueFree();
    }
}
