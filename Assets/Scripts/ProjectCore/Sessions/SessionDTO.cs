namespace ProjectCore.Sessions
{
    public interface ISessionDTO
    {
        string SceneName { get; }
    }
    
    public class SessionDTO : ISessionDTO
    {
        public string SceneName { get; set; }
    }
}