namespace LA.Utilities.SceneName
{
    public static class SceneNameUtility
    {
        public static string GetNameFromPath(string path) => path.Split('/')[^1].Split('.')[0];
    }
}