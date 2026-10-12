using Microsoft.Win32;
using System.IO;
namespace Aivana.Windows;
public sealed record ProgramCandidate(string Name, string Path);
public static class WorkspaceDiscovery
{
    public static IReadOnlyList<ProgramCandidate> GetPrograms()
    {
        var result = new Dictionary<string, ProgramCandidate>(StringComparer.OrdinalIgnoreCase);
        foreach (var hive in new[] { RegistryHive.CurrentUser, RegistryHive.LocalMachine })
        foreach (var view in new[] { RegistryView.Registry64, RegistryView.Registry32 })
        {
            using var root = RegistryKey.OpenBaseKey(hive, view);
            using var paths = root.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths");
            foreach (var name in paths?.GetSubKeyNames() ?? [])
            {
                using var key = paths!.OpenSubKey(name);
                if (key?.GetValue("") is string path && File.Exists(path.Trim('"'))) result[path] = new(Path.GetFileNameWithoutExtension(name), path.Trim('"'));
            }
        }
        var shellType=Type.GetTypeFromProgID("WScript.Shell");
        if(shellType!=null)
        {
            object? shell=null;
            try
            {
                shell=Activator.CreateInstance(shellType);
                foreach(var directory in new[] { Environment.GetFolderPath(Environment.SpecialFolder.Programs), Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms) })
                {
                    if(!Directory.Exists(directory)) continue;
                    foreach(var link in Directory.EnumerateFiles(directory,"*.lnk",new EnumerationOptions { RecurseSubdirectories=true,IgnoreInaccessible=true,AttributesToSkip=FileAttributes.ReparsePoint }).Take(2000))
                    {
                        object? shortcut=null;
                        try
                        {
                            shortcut=((dynamic)shell!).CreateShortcut(link);
                            string target=((dynamic)shortcut).TargetPath;
                            if(target.EndsWith(".exe",StringComparison.OrdinalIgnoreCase) && File.Exists(target)) result.TryAdd(target,new(Path.GetFileNameWithoutExtension(link),target));
                        }
                        catch(Exception ex) when(ex is System.Runtime.InteropServices.COMException or IOException or Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { }
                        finally { if(shortcut!=null && System.Runtime.InteropServices.Marshal.IsComObject(shortcut)) System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut); }
                    }
                }
            }
            catch(Exception ex) when(ex is System.Runtime.InteropServices.COMException or UnauthorizedAccessException or IOException) { }
            finally { if(shell!=null && System.Runtime.InteropServices.Marshal.IsComObject(shell)) System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell); }
        }
        return result.Values.OrderBy(p => p.Name).ToArray();
    }
}
