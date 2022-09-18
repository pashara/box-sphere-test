using ProjectShared.Sessions;

namespace Project.Sessions
{
    public interface ISessionInfoWritable : ISessionInfo
    {
        void Configure(ISessionDTO sessionDto);
    }
    
    public class SessionInfo : ISessionInfo, ISessionInfoWritable
    {
        public string SceneName { get; private set; }

        public void Configure(ISessionDTO sessionDto)
        {
            SceneName = sessionDto.SceneName;
        }
    }
}