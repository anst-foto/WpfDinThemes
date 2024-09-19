using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace WpfApp2;

public partial class App : Application
{
    public static App? Instance;
    public static string? StylesDirectory;
    public static IEnumerable<string>? Files;

    public App()
    {
        Instance = this;
        
        StylesDirectory = Path.Combine(
            path1: Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!, 
            path2: "Styles");
        
        Files = Directory
            .GetFiles(StylesDirectory!)
            .Select(f => new FileInfo(f))
            .Select(fi => fi.Name.Replace(".xaml", ""));
        var fileName = Path.Combine(StylesDirectory!, $"{Files!.First()}.xaml");
        LoadStyleDictionaryFromFile(fileName);
    }
    
    public void LoadStyleDictionaryFromFile(string fileName)
    {
        if (!File.Exists(fileName)) return;
        try
        {
            using var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var dic = (ResourceDictionary)XamlReader.Load(fs);
            Resources.Clear();
            Resources.MergedDictionaries.Add(dic);
        }
        catch
        {
            // ignored
        }
    }
}