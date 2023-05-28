using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;

[CreateAssetMenu]
public class BuildSettings : ScriptableObject, IAssetBookmark
{
    [SerializeField] private string _buildName;
    [SerializeField] private string _buildVersion;
    [SerializeField] private BuildTarget _buildTarget;
    [SerializeField] private BuildTargetGroup _buildTargetGroup;
    [SerializeField] private BuildOptions _buildOptions;
    [SerializeField] private bool _isDevelopmentBuild;
    [Space]
    [TextArea(5, 10)]
    [SerializeField]
    private string _buildDescription;

    [Header("Itch.io parameters")]
    [SerializeField]
    private string _itchUserName;
    [SerializeField] private string _itchProjectName;
    [SerializeField] private string _itchChannel;

    [Space] [SerializeField] private SceneReference[] _scenesInBuild;

    #region Asset Bookmark

    public string[] BookmarkKeys => new string[] { "Build", "Deploy" };

    public void Build()
    {
        Debug.Log("Building...");

        string[] scenes = new string[_scenesInBuild.Length];

        for (int i = 0; i < _scenesInBuild.Length; i++) {
            scenes[i] = _scenesInBuild[i].ScenePath;
        }

        string buildPath = Path.Combine(Application.dataPath, "..", "Builds", _buildName, _buildVersion,
            _buildTarget.ToString());

        if (_buildTarget == BuildTarget.StandaloneWindows || _buildTarget == BuildTarget.StandaloneWindows64) {
            buildPath = Path.Combine(buildPath, Application.productName+".exe");
        }

        BuildPlayerOptions options = new();
        options.scenes = scenes;
        options.locationPathName = buildPath;
        options.target = _buildTarget;
        options.targetGroup = _buildTargetGroup;
        options.options = _buildOptions;

        if (_isDevelopmentBuild) {
            options.extraScriptingDefines = new string[] { "DEVELOPMENT_BUILD" };
        }

        BuildPipeline.BuildPlayer(options);

        Debug.Log("...Built successfully");
    }

    public void Deploy()
    {
        Debug.Log("Deploying...");

        string butlerPath = Path.Combine(Application.dataPath, "Plugins", "Butler", "butler.exe");

        string buildPath = Path.Combine(Application.dataPath, "..", "Builds", _buildName, _buildVersion,
            _buildTarget.ToString());

        ProcessStartInfo startInfo = new();
        startInfo.CreateNoWindow = false;
        startInfo.UseShellExecute = true;
        startInfo.FileName = butlerPath;
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;

        startInfo.Arguments =
            $@"push {buildPath} {_itchUserName}/{_itchProjectName}:{_itchChannel} --userversion {_buildVersion}";

        try {
            // Start the process with the info we specified.
            // Call WaitForExit and then the using statement will close.
            using (Process exeProcess = Process.Start(startInfo)) {
                exeProcess.WaitForExit();
            }
        }
        catch (Exception e) {
            Debug.LogError(e.Message);
            Debug.LogError(e.StackTrace);
            return;
        }

        Debug.Log("...Deployed successfully");
    }

    public void RunBookmarkFunction(int index)
    {
        switch (index) {
            case 0: {
                Build();
                break;
            }
            case 1: {
                Deploy();
                break;
            }
        }
    }

    #endregion
}