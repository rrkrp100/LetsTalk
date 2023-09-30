using Newtonsoft.Json;

namespace DataHandler
{
    public class Json_UserDataHandler : IUserDataHandler
    {

        private Dictionary<string, UserData>? _cachedUserData;

        public async Task<UserData> CreateUser(UserData userDetails)
        {
            if (userDetails == null || string.IsNullOrEmpty(userDetails.UserName))
            { 
                return null;
            }
            try
            {
                var data = await ReadUserFile();
                data ??= new Dictionary<string, UserData>();
                data.Add(userDetails.UserName, userDetails);
                WriteUserFileToDisk(data);
                _cachedUserData = data;
                return await Task.FromResult(userDetails);
            }
            catch (Exception ex)
            {
                // Use Logger
                Console.WriteLine("Unable to add new user.!", ex);
                return null;
            }
        }

        public async Task<bool> DoesUserAlreadyExist(string username)
        {
            try
            {
                await CheckOrUpdateCache();
                return Task.FromResult(_cachedUserData.ContainsKey(username)).Result;
            }
            catch (Exception ex)
            {
                // Use Logger
                Console.WriteLine("Failed To check user", ex);
                return await Task.FromResult(false);
            }

        }

        public async Task<UserData> GetUserIfPresent(string username)
        {
            try
            {
                await CheckOrUpdateCache();
                if (_cachedUserData.ContainsKey(username))
                {
                    return await Task.FromResult(_cachedUserData[username]);
                }
                
                return null;
            }

            catch(Exception ex)
            {
                // Use Logger
                Console.WriteLine("Failed To check user", ex);
                return null;
            }
        }


        private async Task<Dictionary<string, UserData>> ReadUserFile()
        {
            Console.WriteLine("Loading data from json");
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/UserData.json");
            Console.WriteLine(path);
            var file = await File.ReadAllTextAsync(path);
            var data = JsonConvert.DeserializeObject<Dictionary<string, UserData>>(file);
            return data;
        }
        private async void WriteUserFileToDisk(Dictionary<string, UserData> data)
        {
            Console.WriteLine("Writing data to json");
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/UserData.json");
            string serializedData = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(path, serializedData);
        }
        private async Task CheckOrUpdateCache(bool forceUpdate = false)
        {
            if (_cachedUserData != null && !forceUpdate)
                return;
            try
            {
                var data = await ReadUserFile();
                if (data == null)
                {
                    throw new FileLoadException("No data found in the designated storage.");
                }
                _cachedUserData = data;
            }
            catch (Exception ex)
            {
                // Use Logger
                Console.WriteLine("Unable to load user data", ex);
            }
            return;
        }
    }
}
