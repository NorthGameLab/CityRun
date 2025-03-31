using Godot;
using System;
using System.Collections.Generic;

public partial class Fireworks : Node2D
{
    public List<GetItemAnimation> fireworks;

    public override void _Ready()
    {
        foreach (GetItemAnimation a in GetChildren())
        {
            a.Play();
        }
    }

    public void changeFireworkRarity(int i)
    {
        switch (i)
        {
            case 0:
                foreach (GetItemAnimation a in GetChildren())
                {
                    a.Animation = "FireworkCommon";
                }
                break;

            case 1:
                foreach (GetItemAnimation a in GetChildren())
                {
                    a.Animation = "FireworkRare";
                }
                break;

            case 2:
                foreach (GetItemAnimation a in GetChildren())
                {
                    a.Animation = "FireworkEpic";
                }
                break;

            default:
                foreach (GetItemAnimation a in GetChildren())
                {
                    a.Animation = "FireworkCommon";
                }
                break;

        }
    }
}
