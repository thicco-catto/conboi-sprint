#nullable enable

using Godot;

namespace ConboiSprint;

public enum GameScene {
    MAIN_MENU,
    GAME
}

public partial class SceneChanger : Node
{
    private PackedScene _mainMenu = null!;
    private PackedScene _game = null!;

    public override void _Ready()
    {
        _mainMenu = ResourceLoader.Load<PackedScene>("res://Scenes/MainMenu.tscn");
        _game = ResourceLoader.Load<PackedScene>("res://Scenes/Game.tscn");
    }

    private PackedScene? GetPackedScene(GameScene scene)
    {
        switch (scene)
        {
            case GameScene.MAIN_MENU:
                return _mainMenu;

            case GameScene.GAME:
                return _game;
        }

        return null;
    }

    public void ChangeScene(GameScene scene)
    {
        PackedScene? packedScene = GetPackedScene(scene);
        if(packedScene is null)
        {
            GD.PushError($"ATTEMPTED TO SWITCH TO INVALID SCENE ({scene})");
            return;
        }

        GetTree().ChangeSceneToPacked(packedScene);
    }
}