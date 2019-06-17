using System;
using System.Threading;
using System.Threading.Tasks;

namespace Byui.Something.Infrastructure.Common
{
    public class AuthToken
    {
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public DateTimeOffset Expires { get; set; }
        public string MetadataAddress { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }

        public AuthToken(string metadataAddress, string clientId, string clientSecret)
        {
            MetadataAddress = metadataAddress;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public async Task Lock()
        {
            await _lock.WaitAsync();
        }

        public void Release()
        {
            _lock.Release();
        }
    }
}
