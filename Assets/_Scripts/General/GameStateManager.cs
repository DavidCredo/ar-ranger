public class GameStateManager
{
    private static GameStateManager instance;
    
    private GameStateManager()
    {
        // Private constructor to prevent instantiation from outside
    }
    
    public static GameStateManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameStateManager();
            }
            return instance;
        }
    }
    
    // Other game manager methods and properties...
}
