using Newtonsoft.Json;

namespace Core.Model
{
    public class UserSettings
    {
        [JsonProperty("synchronization_enabled")]
        public bool SynchronizationEnabled { get; set; } = false;

        [JsonProperty("remote_address")]
        public string RemoteAddress { get; set; } = string.Empty;

        [JsonProperty("identifier")]
        public string Identifier { get; set; } = string.Empty;

        [JsonProperty("secret")]
        public string Secret { get; set; } = string.Empty;

        [JsonProperty("encryption_enabled")]
        public bool EncryptionEnabled { get; set; } = false;

        [JsonProperty("encryption_key")]
        public string EncryptionKey { get; set; } = string.Empty;
    }
}
