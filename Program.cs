using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyApp
{
    internal class Program
    {
        static char[] tabuleiro = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char jogadorAtual = 'X';
        static bool contraPC = false;
        static Dificuldade dificuldade;
        static bool TelaInicial = true;
        static bool TelaPlayerOuPC = true;
        static bool TelaDificuldade = true;
        static bool TelaPlayer = true;
        static bool TelaJogarNovamente = true;
        static bool TelaRanking = true;
        static bool Principal = true;
        static bool TelaJogo = true;
        static int PontosEmpates { get; set; }
        static int PontosVitoriaPlayer1 { get; set; }
        static int PontosVitoriaPlayer2 { get; set; }
        static int PontosVitoriaComputador { get; set; }


        static void Main()
        {
            while (Principal)
            {
                Console.Clear();
                ResetarVariaveis();

                ExibirTelaInicial();

                ExibirModoDeJogo();

                ExibirSelecaoDificuldade();

                ExibirSelecaoPlayers();

                ExibirTelaDoJogo();
            }
        }

        static void ExibirTabuleiro()
        {
            Console.Clear(); // Limpa a tela para colocar o tabuleiro.

            // Desenha o tabuleiro com as posições e símbolos preenchidos
            Console.WriteLine("-------------");
            Console.WriteLine($"| {tabuleiro[0]} | {tabuleiro[1]} | {tabuleiro[2]} |"); 
            Console.WriteLine($"| {tabuleiro[3]} | {tabuleiro[4]} | {tabuleiro[5]} |");
            Console.WriteLine($"| {tabuleiro[6]} | {tabuleiro[7]} | {tabuleiro[8]} |");
            Console.WriteLine("-------------");
        }

        static void ExibirTelaInicial()
        {
            while (TelaInicial)
            {
                Console.Clear();
                Console.WriteLine("JOGO DA VELHA \n"); // Mostra o título na tela

                Console.WriteLine("RANKING");
                Console.WriteLine($"JOGADOR 1: {PontosVitoriaPlayer1}");
                Console.WriteLine($"JOGADOR 2: {PontosVitoriaPlayer2}");
                Console.WriteLine($"COMPUTADOR: {PontosVitoriaComputador}");
                Console.WriteLine($"EMPATES: {PontosEmpates}\n");

                Console.WriteLine("1 - JOGAR"); // Mostra na tela a opção de para o programa
                Console.WriteLine("2 - SAIR"); // Mostra na tela a opção de para o programa
                int[] opcoesValidas = { 1, 2};
                string opcaoSelecionada = Console.ReadLine();
                bool opcaoValida = ValidarOpcao(opcaoSelecionada, opcoesValidas);

                if (!opcaoValida)
                {
                    Console.WriteLine("Selecione uma opção válida!");
                }
                else
                {
                    switch (opcaoSelecionada)
                    {
                        case "1":
                            TelaInicial = false;
                            break;
                        case "2":
                            Environment.Exit(0);
                            return;
                    }
                }
            }

            TelaInicial = true;
        }

        static void ExibirModoDeJogo()
        {
            while (TelaPlayerOuPC)
            {
                Console.Clear();
                Console.WriteLine("1 - PLAYER VS. PLAYER");
                Console.WriteLine("2 - PLAYER VS. COMPUTADOR");

                int[] opcoesValidas = { 1, 2 };
                string opcaoSelecionada = Console.ReadLine();
                bool opcaoValida = ValidarOpcao(opcaoSelecionada, opcoesValidas);

                if (!opcaoValida)
                {
                    Console.WriteLine("Selecione uma opção válida!");
                    TelaPlayerOuPC = true;
                }
                else
                {
                    contraPC = opcaoSelecionada == "1" ? false : true;
                    TelaPlayerOuPC = false;
                }
            }

            TelaPlayerOuPC = true;
        }

        static void ExibirSelecaoDificuldade()
        {
            while (TelaDificuldade && contraPC)
            {
                Console.Clear();

                Console.WriteLine("Você escolheu jogar contra o computador!");
                Console.WriteLine("Jogador será X e o Computador será O.");
                jogadorAtual = 'X'; // jogador sempre começa como X contra o Computador

                Console.Clear();

                Console.WriteLine("Escolha a dificuldade: ");
                Console.WriteLine("1 - Facil  \n2 - Medio  \n3 - Dificil"); // Mostra na tela qual número o jogador deve usar para dificuldade, e \n pula a linha
                int[] opcoesValidas = { 1, 2, 3 };
                string opcaoSelecionada = Console.ReadLine();
                bool opcaoValida = ValidarOpcao(opcaoSelecionada, opcoesValidas);

                if (!opcaoValida)
                {
                    Console.WriteLine("Selecione uma opção válida!");
                    TelaDificuldade = true;
                }
                else
                {
                    SetDificuldade(opcaoSelecionada); // Chama o método que valida e aplica a dificuldade
                    TelaDificuldade = false;
                }
            }

            TelaDificuldade = true;
        }

        static void ExibirSelecaoPlayers()
        {
            while (TelaPlayer && !contraPC) // Se for jogar contra outro jogador
            {
                Console.Clear();
                //Mostra na tela o modo de jogo e qual opção o jogador gostara de ir
                Console.WriteLine("JOGADOR 1 ESCOLHA:");
                Console.WriteLine("1 - X ");
                Console.WriteLine("2 - O");

                int[] opcoesValidas = { 1, 2 };
                string opcaoSelecionada = Console.ReadLine();
                bool opcaoValida = ValidarOpcao(opcaoSelecionada, opcoesValidas);

                if (!opcaoValida)
                {
                    Console.WriteLine("Selecione uma opção válida!");
                    TelaPlayer = true;
                }
                else
                {
                    // Pula linha após a entrada do usuário
                    Console.WriteLine();

                    // Define jogadorAtual baseado na escolha, se inválida, assume 'X' por padrão
                    jogadorAtual = opcaoSelecionada == "1" ? 'X' : 'O';

                    // Player 2 recebe automaticamente o outro símbolo
                    char jogador2 = jogadorAtual == 'X' ? 'O' : 'X';

                    Console.Clear(); // Limpa a tela do console
                                     // Mostra para o Player 2 qual símbolo ele terá
                    Console.WriteLine($"Jogador 1 será {jogadorAtual}.");
                    Console.WriteLine($"Jogador 2 será {jogador2}.");
                    Console.WriteLine("Pressione ENTER para continuar...");
                    Console.ReadLine();// Aguarda o jogador pressionar ENTER antes de continuar
                    Console.Clear(); // Limpa a tela do console
                    TelaPlayer = false;
                }
            }

            TelaPlayer = true;
        }

        static void ExibirTelaDoJogo()
        {
            while (TelaJogo)
            {
                ExibirTabuleiro();

                if (jogadorAtual == 'O' && contraPC)
                    JogadaComputador();
                else
                    JogadaJogador();

                bool verificaVitoria = VerificarVitoria();

                bool verificaEmpate = VerificarEmpate();

                if (verificaVitoria == true)
                {
                    ExibirTabuleiro();

                    if (jogadorAtual == 'X')
                        PontosVitoriaPlayer1++;
                    else if (!contraPC)
                        PontosVitoriaPlayer2++;
                    else
                        PontosVitoriaComputador++;

                    Console.WriteLine($"JOGADOR {jogadorAtual} VENCEU!\n"); //Mostra na tela a opção de sair

                    TelaFinal();
                }

                if (verificaEmpate)
                {
                    ExibirTabuleiro();

                    PontosEmpates++;

                    Console.WriteLine($"EMPATE!!\n"); //Mostra na tela a opção de sair

                    TelaFinal();
                }

                if (!verificaVitoria && !verificaEmpate)
                    TrocarJogador();
            }

            TelaJogo = true;
        }

        static void ExibirRanking()
        {
            Console.Clear();

            while (TelaRanking)
            {
                Console.WriteLine("PONTUAÇÃO\n");
                Console.WriteLine($"JOGADOR 1: {PontosVitoriaPlayer1}");
                Console.WriteLine($"JOGADOR 2: {PontosVitoriaPlayer2}");
                Console.WriteLine($"COMPUTADOR: {PontosVitoriaComputador}");
                Console.WriteLine($"EMPATES: {PontosEmpates}\n");

                Console.WriteLine($"1 - VOLTAR");
                int[] opcoesValidas = { 1 };
                string opcaoSelecionada = Console.ReadLine();
                bool opcaoValida = ValidarOpcao(opcaoSelecionada, opcoesValidas);

                if (!opcaoValida)
                {
                    Console.WriteLine("Selecione uma opção válida!");
                    TelaRanking = true;
                }
                else
                {
                    switch (opcaoSelecionada)
                    {
                        case "1":
                            TelaRanking = false;
                            Console.Clear();
                            break;
                    }
                }
            }

            TelaRanking = true;
        }

        static void TelaFinal()
        {
            while (TelaJogarNovamente)
            {
                Console.WriteLine("1 - JOGAR NOVAMENTE"); //Mostra na tela a opção de sair
                Console.WriteLine("2 - RANKING"); //Mostra na tela a opção de sair
                Console.WriteLine("3 - TELA INICIAL"); //Mostra na tela a opção de sair

                int[] opcoesValidas = { 1, 2, 3 };
                string opcaoSelecionada = Console.ReadLine();
                bool opcaoValida = ValidarOpcao(opcaoSelecionada, opcoesValidas);

                if (!opcaoValida)
                {
                    Console.WriteLine("Selecione uma opção válida!");
                    TelaJogo = true;
                }
                else
                {
                    switch (opcaoSelecionada)
                    {
                        case "1":
                            // Reinicia o tabuleiro mostrando 1 à 9 novamente.
                            for (int i = 0; i < tabuleiro.Length; i++)
                                tabuleiro[i] = (char)('1' + i);

                            jogadorAtual = 'X'; // volta pro X começar
                            TelaJogarNovamente = false;
                            break;
                        case "2":
                            ExibirRanking();
                            TelaJogarNovamente = true;
                            break;
                        case "3":
                            TelaJogo = false;
                            TelaJogarNovamente = false;
                            break;
                    }
                }
            }

            TelaJogarNovamente = true;
        }

        static void ResetarVariaveis()
        {
            for (int i = 0; i < tabuleiro.Length; i++)
                tabuleiro[i] = (char)('1' + i);
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
                {0,1,2}, {3,4,5}, {6,7,8}, // Linhas
                {0,3,6}, {1,4,7}, {2,5,8}, // Colunas
                {0,4,8}, {2,4,6} // Diagonais
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

        static bool ValidarOpcao(string opcaoSelecionada, int[] opcoesValidas)
        {
            if (int.TryParse(opcaoSelecionada, out int opcaoValida))
            {
                if (opcoesValidas.Any(x => opcaoValida == x))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        static void TrocarJogador()
        {
            // Se o jogador atual for X, passa a ser O.
            // Se não for X (ou seja, for O), passa a ser X, por isso os : servem, eles verificam se não é, e o ponto de interrogação verifica se é verdadeiro.
            jogadorAtual = jogadorAtual == 'X' ? 'O' : 'X';
        }

        static bool VerificarVitoria()
        {
            // Matriz de combinações possiveis
            int[,] combinacoes = {
            {0,1,2}, {3,4,5}, {6,7,8}, // Linhas
            {0,3,6}, {1,4,7}, {2,5,8}, // Colunas
            {0,4,8}, {2,4,6} // Diagonais
            };

            // Loop de acordo com o numero de matrizes
            for (int i = 0; i < combinacoes.GetLength(0); i++)
            {
                // Captura o valor da matriz que esta na vez
                int posicaoA = combinacoes[i, 0], posicaoB = combinacoes[i, 1], posicaC = combinacoes[i, 2];

                // Verifica se a combinação da matriz da vez é uma combinação de vitoria
                if (tabuleiro[posicaoA] == jogadorAtual && tabuleiro[posicaoB] == jogadorAtual && tabuleiro[posicaC] == jogadorAtual)
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

            if (Enum.TryParse(codigoDificuldade, out codigo))
            {
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
