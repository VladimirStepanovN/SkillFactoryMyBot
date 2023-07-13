using SkillFactoryMyBot.Models;

namespace SkillFactoryMyBot.Services
{
    public interface IStorage
    {
        public Session GetSession(long chatId);
    }
}
