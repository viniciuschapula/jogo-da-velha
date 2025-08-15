using System;

namespace MyApp
{
    internal class Program
    {
        static char[] tabuleiro = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char jogadorAtual = 'X';
        static bool contraPC = false;
        static Dificuldade dificuldade;
        static bool TelaInicial = true;

        static void Main()
        {
            while (TelaInicial)
            {
                Console.WriteLine("JOGO DA VELHA");
                Console.WriteLine("Deseja jogar contra o computador? (s/n): ");
                contraPC = Console.ReadLine().ToLower() == "s";
                Console.Clear();

                Console.WriteLine("Escolha a dificuldade: ");
                Console.WriteLine("1 - Facil \n 2 - Medio \n 3 - Dificil");

                string codigoDificuldade = Console.ReadLine();
                SetDificuldade(codigoDificuldade);
            }

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

        //static void JogadaComputador()
        //{
        //    Random rand = new Random();
        //    int posicao;
        //    do
        //    {
        //        posicao = rand.Next(1, 10);
        //    } while (tabuleiro[posicao - 1] == 'X' || tabuleiro[posicao - 1] == 'O');

        //    Console.WriteLine($"Computador escolheu a posição {posicao}");
        //    tabuleiro[posicao - 1] = jogadorAtual;
        //    System.Threading.Thread.Sleep(1000);
        //}

        static void JogadaComputador()
        {
            int posicao = -1;

            if (dificuldade == Dificuldade.Facil)
            {
                posicao = JogadaAleatoria();
            }
            else if (dificuldade == Dificuldade.Medio)
            {
                // Tenta bloquear
                posicao = EncontrarJogadaVencedora('X');
                if (posicao == -1) // Se não há ameaça, joga aleatório
                    posicao = JogadaAleatoria();
            }
            else if (dificuldade == Dificuldade.Dificil)
            {
                // 1. Tenta ganhar
                posicao = EncontrarJogadaVencedora('O');

                // 2. Se não pode ganhar, tenta bloquear
                if (posicao == -1)
                    posicao = EncontrarJogadaVencedora('X');

                // 3. Se não há risco, segue estratégia
                if (posicao == -1)
                {
                    if (tabuleiro[4] != 'X' && tabuleiro[4] != 'O')
                        posicao = 5; // Centro
                    else
                        posicao = EscolherMelhorPosicao();
                }
            }

            Console.WriteLine($"Computador escolheu a posição {posicao}");
            tabuleiro[posicao - 1] = jogadorAtual;
            System.Threading.Thread.Sleep(1000);
        }

        static int JogadaAleatoria()
        {
            Random rand = new Random();
            int posicao;
            do
            {
                posicao = rand.Next(1, 10);
            } while (tabuleiro[posicao - 1] == 'X' || tabuleiro[posicao - 1] == 'O');
            return posicao;
        }

        static int EncontrarJogadaVencedora(char simbolo)
        {
            int[,] combinacoes = {
        {0,1,2}, {3,4,5}, {6,7,8}, //Linhas
        {0,3,6}, {1,4,7}, {2,5,8}, //Colunas
        {0,4,8}, {2,4,6} //Diagonais
    };

            for (int i = 0; i < combinacoes.GetLength(0); i++)
            {
                int a = combinacoes[i, 0], b = combinacoes[i, 1], c = combinacoes[i, 2];

                if (tabuleiro[a] == simbolo && tabuleiro[b] == simbolo && tabuleiro[c] != 'X' && tabuleiro[c] != 'O')
                    return c + 1;
                if (tabuleiro[a] == simbolo && tabuleiro[c] == simbolo && tabuleiro[b] != 'X' && tabuleiro[b] != 'O')
                    return b + 1;
                if (tabuleiro[b] == simbolo && tabuleiro[c] == simbolo && tabuleiro[a] != 'X' && tabuleiro[a] != 'O')
                    return a + 1;
            }
            return -1;
        }

        static int EscolherMelhorPosicao()
        {
            int[] cantos = { 1, 3, 7, 9 };
            int[] lados = { 2, 4, 6, 8 };

            foreach (var canto in cantos)
                if (tabuleiro[canto - 1] != 'X' && tabuleiro[canto - 1] != 'O')
                    return canto;

            foreach (var lado in lados)
                if (tabuleiro[lado - 1] != 'X' && tabuleiro[lado - 1] != 'O')
                    return lado;



            // Se nada disponível, joga aleatório
            return JogadaAleatoria();
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

        static void SetDificuldade(string codigoDificuldade)
        {
            Dificuldade codigo;
            if (Enum.TryParse(codigoDificuldade, out codigo)){
                TelaInicial = false;
                dificuldade = codigo;
            }
            else
            {
                Console.WriteLine("Digite um código de dificuldade valido! \n Codigos Validos: 1, 2, 3");
                TelaInicial = true;
            }
        }

        private enum Dificuldade
        {
            Facil = 1,
            Medio = 2,
            Dificil = 3
        }
    }
}
