
namespace CAB201HOSPAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataBase dataBase = new Database();

            IDataHandler dataHandler = new DataHandler(dataBase);
            
            MainMenuHandler mainMenuHandler = new MainMenuHandler(dataHandler);

            mainMenuHandler.Run();
        }
    }
}