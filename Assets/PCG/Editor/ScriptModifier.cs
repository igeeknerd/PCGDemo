using UnityEngine;
using UnityEditor;
using System.Collections;

public class HEScriptKeywordReplace : UnityEditor.AssetModificationProcessor
{
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        string file = path.Substring(index);
        if (file != ".cs" && file != ".js" && file != ".boo") return;
        string fileExtension = file;

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        file = System.IO.File.ReadAllText(path);

        file = file.Replace("#DATE#", System.DateTime.Now.ToString("d"));
        file = file.Replace("#SCRIPTFULLNAME#", path);
        file = file.Replace("#AUTHOR#", "igeeknerd");
        file = file.Replace("#UNITYVERSION#", Application.unityVersion);

        System.IO.File.WriteAllText(path, file);
        AssetDatabase.Refresh();
    }
}
