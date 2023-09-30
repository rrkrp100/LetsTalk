using Newtonsoft.Json;

namespace DataHandler.Resources
{
    internal static class LocalFileReader
    {
        internal static async Task<T> ReadJsonFile<T>(string nameOfFile)
        {
            Console.WriteLine($"Loading {nameOfFile}");
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Resources/{nameOfFile}.json");
            Console.WriteLine(path);
            var file = await File.ReadAllTextAsync(path);
            var data = JsonConvert.DeserializeObject<T>(file);
            return data;
        }
        internal static async Task WriteJsonFile<T>(T data, string nameOfFile)
        {
            Console.WriteLine($"Writing data to {nameOfFile}");
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Resources/{nameOfFile}.json");
            Console.WriteLine(path);
            var serializedData = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(path, serializedData);
            return;
        }
    }
}