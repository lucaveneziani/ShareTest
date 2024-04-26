using System.Collections.Concurrent;

namespace ServiceAnalyzer.WebService.Code
{
    public class ConnectionMapping
    {
        private readonly ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();

        public void Add(string email, string connectionId)
        {
            _connections.TryAdd(email, connectionId);
        }

        public string? GetConnectionId(string email)
        {
            _connections.TryGetValue(email.Trim(), out string? connectionId);
            return connectionId;
        }

        public void Remove(string email)
        {
            _connections.TryRemove(email, out _);
        }
    }
}
