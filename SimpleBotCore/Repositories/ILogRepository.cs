namespace SimpleBotCore.Repositories
{
    public interface ILogRepository
    {
        int TotalRegistros();
        void CriarLog(string texto);
    }
}
