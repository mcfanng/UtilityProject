
namespace UtilityApp.Interfaces
{
    public interface IFileUtil {

        string FindFile(params string[] searchPaths);
        string FindFolder(params string[] searchPaths);
        string[] FindFile(string fileNamePattern, params string[] searchPaths);
    }
}