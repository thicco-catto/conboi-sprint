using Godot;

namespace ConboiSprint;

public partial class PersistentData : Node
{
    private const string SAVE_FILE = "user://savegame.save";
    public ulong HighScore { get; private set; } = 0;

    public override void _EnterTree()
    {
        base._EnterTree();

        LoadData();
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        SaveData();
    }

    public void LoadData()
    {
        if (!FileAccess.FileExists(SAVE_FILE))
        {
            return;
        }

        using FileAccess saveGame = FileAccess.Open(SAVE_FILE, FileAccess.ModeFlags.Read);

        HighScore = saveGame.Get64();
    }

    public void SaveData()
    {
        using FileAccess saveGame = FileAccess.Open(SAVE_FILE, FileAccess.ModeFlags.Write);

        saveGame.Store64(HighScore);
    }

    public bool TrySetHighScore(ulong newScore)
    {
        if(newScore > HighScore)
        {
            HighScore = newScore;
            return true;
        }

        return false;
    }
}