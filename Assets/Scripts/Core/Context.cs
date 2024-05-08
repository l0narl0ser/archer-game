namespace Core
{
    public class Context
    {
        private static Context _instance;
        private readonly MessageSystem _messageSystem;

        private Context()
        {
            _messageSystem = new MessageSystem();
        }
        
        public static Context Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = new Context();
                return _instance;
            }
        }
        public MessageSystem GetMessageSystem()
        {
            return _messageSystem;
        }
    }
}