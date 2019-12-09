using UnityEditor;

public class CustomHotkeysSetter
{
    [MenuItem("Edit/CustomHotkeys/PlayOrStop _F5")]
    static void PlayOrStopGame()
    {
        if (!EditorApplication.isPlaying)
            EditorApplication.ExecuteMenuItem("File/Save");

        EditorApplication.ExecuteMenuItem("Edit/Play");
    }
}