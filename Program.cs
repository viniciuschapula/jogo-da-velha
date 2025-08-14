using System;

namespace MyApp
{
    internal class Program
    {
        static char[] tabuleiro = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char jogadorAtual = 'X';
        static bool contraPC = false;

        static void Main()
        {
            Console.WriteLine("JOGO DA VELHA");
            Console.Write("Deseja jogar contra o computador? (s/n): ");
            contraPC = Console.ReadLine().ToLower() == "s";

            while (true)
            {
                ExibirTabuleiro();
                if (jogadorAtual == 'O' && contraPC)
                    JogadaComputador();
                else
                    JogadaJogador();

                if (VerificarVitoria())
                {
                    ExibirTabuleiro();
                    Console.WriteLine($"Jogador {jogadorAtual} venceu!");
                    break;
                }

                if (VerificarEmpate())
                {
                    ExibirTabuleiro();
                    Console.WriteLine("Empate!");
                    break;
                }

                TrocarJogador();
            }
        }

        static void ExibirTabuleiro()
        {
            Console.Clear();
            Console.WriteLine("-------------");
            Console.WriteLine($"| {tabuleiro[0]} | {tabuleiro[1]} | {tabuleiro[2]} |");
            Console.WriteLine($"| {tabuleiro[3]} | {tabuleiro[4]} | {tabuleiro[5]} |");
            Console.WriteLine($"| {tabuleiro[6]} | {tabuleiro[7]} | {tabuleiro[8]} |");
            Console.WriteLine("-------------");
        }

        static void JogadaJogador()
        {
            int posicao;
            while (true)
            {
                Console.Write($"Jogador {jogadorAtual}, escolha uma posição (1-9): ");
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out posicao) && posicao >= 1 && posicao <= 9)
                {
                    if (tabuleiro[posicao - 1] != 'X' && tabuleiro[posicao - 1] != 'O')
                    {
                        tabuleiro[posicao - 1] = jogadorAtual;
                        break;
                    }
                    else
                        Console.WriteLine("Posição já ocupada!");
                }
                else
                    Console.WriteLine("Entrada inválida!");
            }
        }

        static void JogadaComputador()
        {
            Random rand = new Random();
            int posicao;
            do
            {
                posicao = rand.Next(1, 10);
            } while (tabuleiro[posicao - 1] == 'X' || tabuleiro[posicao - 1] == 'O');

            Console.WriteLine($"Computador escolheu a posição {posicao}");
            tabuleiro[posicao - 1] = jogadorAtual;
            System.Threading.Thread.Sleep(1000);
        }

        static void TrocarJogador()
        {
            jogadorAtual = jogadorAtual == 'X' ? 'O' : 'X';
        }

        static bool VerificarVitoria()
        {
            //Matriz de combinações possiveis
            int[,] combinacoes = {
            {0,1,2}, {3,4,5}, {6,7,8}, //Linhas
            {0,3,6}, {1,4,7}, {2,5,8}, //Colunas
            {0,4,8}, {2,4,6} //Diagonais
            };

            //Loop de acordo com o numero de matrizes
            for (int i = 0; i < combinacoes.GetLength(0); i++)
            {
                //Captura o valor da matriz que esta na vez
                int a = combinacoes[i, 0], b = combinacoes[i, 1], c = combinacoes[i, 2];

                //Verifica se a combinação da matriz da vez é uma combinação de vitoria
                if (tabuleiro[a] == jogadorAtual && tabuleiro[b] == jogadorAtual && tabuleiro[c] == jogadorAtual)
                    return true;
            }
            return false;
        }

        static bool VerificarEmpate()
        {
            foreach (char c in tabuleiro)
                if (c != 'X' && c != 'O')
                    return false;
            return true;
        }
    }
}
