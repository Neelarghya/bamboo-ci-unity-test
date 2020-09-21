using System;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class BuildCommand
{
    private static string _locationPath;
    private static string _buildTarget;

    static void PerformBuild()
    {
        Console.WriteLine(":: Starting Build");

        _locationPath = GetArgument("-customBuildPath");
        _buildTarget = GetArgument("-buildTarget")?.ToLower();
        
        Console.WriteLine(":: Got Build Path " + _locationPath);
        Console.WriteLine(":: Got Build Target " + _buildTarget);

        var scenes = new[]
        {
            "Assets/Scenes/Main.unity"
        };
        
        BuildTarget buildTarget;

        switch (_buildTarget)
        {
            case "android":
                buildTarget = BuildTarget.Android;
                break;
            case "ios":
                buildTarget = BuildTarget.iOS;
                break;
            default:
                buildTarget = BuildTarget.Android;
                break;
        }
        
        var buildInfo = BuildPipeline.BuildPlayer(scenes, _locationPath, buildTarget, BuildOptions.None);
        if (buildInfo.summary.result == BuildResult.Succeeded)
        {
            Console.WriteLine(":: Done with build");
            EditorApplication.Exit(0);
        }
        else
        {
            Console.WriteLine(":: Build error");
            foreach (var step in buildInfo.steps)
            {
                foreach (var message in step.messages)
                {
                    Console.WriteLine(step.name + " -- " + message.content);
                }
            }

            EditorApplication.Exit(1);
        }
    }

    static string GetArgument(string name)
    {
        string[] args = Environment.GetCommandLineArgs();
        for (int index = 0; index < args.Length; index++)
        {
            if (args[index].Contains(name))
            {
                return args[index + 1];
            }
        }

        return null;
    }

    static void CloseUnity()
    {
        EditorApplication.Exit(0);
    }
}