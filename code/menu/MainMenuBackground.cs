using Godot;
using System;

public partial class MainMenuBackground : Node2D
{

	private PackedScene Linnut = ResourceLoader.Load<PackedScene>("res://scene/menu/MainMenuBackground/Linnut.tscn");
	private PackedScene PilvetHidas = ResourceLoader.Load<PackedScene>("res://scene/menu/MainMenuBackground/PilvetHidas.tscn");
	private PackedScene PilviNopea = ResourceLoader.Load<PackedScene>("res://scene/menu/MainMenuBackground/PilviNopea.tscn");
	private PackedScene TalotEdessä = ResourceLoader.Load<PackedScene>("res://scene/menu/MainMenuBackground/TalotEdessä.tscn");
	private PackedScene TalotKeskellä = ResourceLoader.Load<PackedScene>("res://scene/menu/MainMenuBackground/TalotKeskellä.tscn");
	private PackedScene TalotTakana = ResourceLoader.Load<PackedScene>("res://scene/menu/MainMenuBackground/TalotTakana.tscn");
	private Sprite2D pilvetHidas, pilviNopea, talotEdessä, talotKeskellä, talotTakana, talotEdessä2, talotKeskellä2, talotTakana2;
	private AnimatedSprite2D linnut;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		linnut = Linnut.Instantiate<AnimatedSprite2D>();
		pilvetHidas = PilvetHidas.Instantiate<Sprite2D>();
		pilviNopea = PilviNopea.Instantiate<Sprite2D>();
		talotEdessä = TalotEdessä.Instantiate<Sprite2D>();
		talotKeskellä = TalotKeskellä.Instantiate<Sprite2D>();
		talotTakana = TalotTakana.Instantiate<Sprite2D>();
		talotEdessä2 = TalotEdessä.Instantiate<Sprite2D>();
		talotKeskellä2 = TalotKeskellä.Instantiate<Sprite2D>();
		talotTakana2 = TalotTakana.Instantiate<Sprite2D>();

		AddChild(linnut);
		AddChild(pilvetHidas);
		AddChild(pilviNopea);
		AddChild(talotTakana);
		AddChild(talotTakana2);
		AddChild(talotKeskellä);
		AddChild(talotKeskellä2);
		AddChild(talotEdessä);
		AddChild(talotEdessä2);


		talotEdessä2.GlobalPosition -= new Vector2(600f, 0);
		talotKeskellä2.GlobalPosition -= new Vector2(600f, 0);
		talotTakana2.GlobalPosition -= new Vector2(600f, 0);

		linnut.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		linnut.GlobalPosition += new Vector2(110f * (float)delta, 0);
		if (linnut.GlobalPosition.X >= 700)
			linnut.GlobalPosition -= new Vector2(900, 0);

		pilvetHidas.GlobalPosition += new Vector2(6f * (float)delta, 0);
		if (pilvetHidas.GlobalPosition.X >= 1000)
			pilvetHidas.GlobalPosition -= new Vector2(1300, 0);

		pilviNopea.GlobalPosition -= new Vector2(18f * (float)delta, 0);
		if (pilviNopea.GlobalPosition.X <= -200)
			pilviNopea.GlobalPosition += new Vector2(1000, 0);

		talotTakana.GlobalPosition += new Vector2(150f * (float)delta, 0);
		talotTakana2.GlobalPosition += new Vector2(150f * (float)delta, 0);
		if (talotTakana.GlobalPosition.X >= 850)
			talotTakana.GlobalPosition -= new Vector2(1150, 0);
		if (talotTakana2.GlobalPosition.X >= 850)
			talotTakana2.GlobalPosition -= new Vector2(1150, 0);

		talotKeskellä.GlobalPosition += new Vector2(100f * (float)delta, 0);
		talotKeskellä2.GlobalPosition += new Vector2(100f * (float)delta, 0);
		if (talotKeskellä.GlobalPosition.X >= 850)
			talotKeskellä.GlobalPosition -= new Vector2(1150, 0);
		if (talotKeskellä2.GlobalPosition.X >= 850)
			talotKeskellä2.GlobalPosition -= new Vector2(1150, 0);

		talotEdessä.GlobalPosition += new Vector2(50f * (float)delta, 0);
		talotEdessä2.GlobalPosition += new Vector2(50f * (float)delta, 0);
		if (talotEdessä.GlobalPosition.X >= 850)
			talotEdessä.GlobalPosition -= new Vector2(1150, 0);
		if (talotEdessä2.GlobalPosition.X >= 850)
			talotEdessä2.GlobalPosition -= new Vector2(1150, 0);



	}
}
