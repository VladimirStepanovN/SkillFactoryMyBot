using System.Collections.Concurrent;
using SkillFactoryMyBot.Models;

namespace SkillFactoryMyBot.Services
{
    public class MemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;
        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }
        public Session GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];
            var newSession = new Session() { SelectMode = "chars" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
