namespace SimpleBotCore.Repositories
{
    public interface IPerguntasRepositorio
    {
        void GravarPergunta(string userId, string texto);
    }
}