using SimpleBotCore.Bot;
using SimpleBotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleBot : BotDialog
    {
        protected async override Task BotConversation()
        {
            string nome;

            await WriteAsync("Boa noite!");

            await WriteAsync("Qual o seu nome?");

            nome = await ReadAsync();

            await WriteAsync($"{nome}, bem vindo ao Oraculo. Faça sua pergunta");

            while (true)
            {
                string texto = await ReadAsync();

                if( texto.EndsWith("?") )
                {
                    await WriteAsync("Processando...");

                    await Task.Delay(5000);

                    await WriteAsync("Resposta não encontrada");
                }
                else
                {
                    await WriteAsync("Você disse: " + texto);
                }
            }
        }
    }
}
