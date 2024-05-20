using Godot;

namespace ConboiSprint;

public partial class ChangeSceneButton: Button {
    [Export]
    private GameScene _scene;
    private SceneChanger _sceneChanger;

    public override void _Ready()
    {
        base._Ready();

        _sceneChanger = GetNode<SceneChanger>("/root/SceneChanger");

        Pressed += OnClick;
    }

    public void OnClick()
    {
        _sceneChanger.ChangeScene(_scene);
    }
}