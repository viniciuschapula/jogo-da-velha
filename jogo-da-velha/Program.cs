//Variaveis
char[] tabuleiro = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
char charDaVez = 'X';
int jogadorDaVez = 1;
bool contraPC = false;
int dificuldade = 0;
bool TelaInicial = true;
bool TelaPlayerOuPC = true;
bool TelaDificuldade = true;
bool TelaPlayer = true;
bool TelaJogarNovamente = true;
bool TelaRanking = true;
bool Principal = true;
bool TelaJogo = true;
int PontosEmpates = 0;
int PontosVitoriaPlayer1 = 0;
int PontosVitoriaPlayer2 = 0;
int PontosVitoriaComputador = 0;


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


void ExibirTabuleiro()
{
    Console.Clear();

    Console.WriteLine($" {tabuleiro[0]} | {tabuleiro[1]} | {tabuleiro[2]} ");
    Console.WriteLine("---|---|---");

    Console.WriteLine($" {tabuleiro[3]} | {tabuleiro[4]} | {tabuleiro[5]} ");
    Console.WriteLine("---|---|---");

    Console.WriteLine($" {tabuleiro[6]} | {tabuleiro[7]} | {tabuleiro[8]} ");
}

void ExibirTelaInicial()
{
    while (TelaInicial)
    {
        Console.Clear();
        Console.WriteLine("JOGO DA VELHA \n");
        Console.WriteLine("Anelise Ferreira Alves - 2025106346");
        Console.WriteLine("Lucas Kauan Kraj - 2025105504");
        Console.WriteLine("Vinicius de Oliveira Chapula - 2021206186");
        Console.WriteLine("Vinicius Henrique Narciso Biazzetto - 2025106462\n");

        Console.WriteLine("RANKING");
        Console.WriteLine($"JOGADOR 1: {PontosVitoriaPlayer1}");
        Console.WriteLine($"JOGADOR 2: {PontosVitoriaPlayer2}");
        Console.WriteLine($"COMPUTADOR: {PontosVitoriaComputador}");
        Console.WriteLine($"EMPATES: {PontosEmpates}\n");

        Console.WriteLine("1 - JOGAR");
        Console.WriteLine("2 - SAIR");
        int[] opcoesValidas = { 1, 2 };
        string opcaoSelecionada = Console.ReadLine().Trim();
        bool opcaoValida = ValidarOpcao(opcaoSelecionada, opcoesValidas);

        if (!opcaoValida)
        {
            Console.WriteLine("Selecione uma opção válida!");
            Console.ReadKey();
        }
        else
        {
            switch (opcaoSelecionada)
            {
                case "1":
                    TelaInicial = false;
                    break;
                //Finaliza o programa
                case "2":
                    Environment.Exit(0);
                    return;
            }
        }
    }

    TelaInicial = true;
}

void ExibirModoDeJogo()
{
    while (TelaPlayerOuPC)
    {
        Console.Clear();
        Console.WriteLine("1 - PLAYER VS. PLAYER");
        Console.WriteLine("2 - PLAYER VS. COMPUTADOR");

        int[] opcoesValidas = { 1, 2 };
        string opcaoSelecionada = Console.ReadLine().Trim();
        bool opcaoValida = ValidarOpcao(opcaoSelecionada, opcoesValidas);

        if (!opcaoValida)
        {
            Console.WriteLine("Selecione uma opção válida!");
            TelaPlayerOuPC = true;
        }
        else
        {
            if (opcaoSelecionada == "1")
                contraPC = false;
            else
                contraPC = true;

            TelaPlayerOuPC = false;
        }
    }

    TelaPlayerOuPC = true;
}

void ExibirSelecaoDificuldade()
{
    while (TelaDificuldade && contraPC)
    {
        Console.Clear();

        Console.WriteLine("Você escolheu jogar contra o computador!");
        Console.WriteLine("Jogador será X e o Computador será O.");
        charDaVez = 'X';

        Console.Clear();

        Console.WriteLine("Escolha a dificuldade: ");
        Console.WriteLine("1 - Facil  \n2 - Medio  \n3 - Dificil");
        int[] opcoesValidas = { 1, 2, 3 };
        string opcaoSelecionada = Console.ReadLine().Trim();
        bool opcaoValida = ValidarOpcao(opcaoSelecionada, opcoesValidas);

        if (!opcaoValida)
        {
            Console.WriteLine("Selecione uma opção válida!");
            TelaDificuldade = true;
        }
        else
        {
            SetDificuldade(opcaoSelecionada);
            TelaDificuldade = false;
        }
    }

    TelaDificuldade = true;
}

void ExibirSelecaoPlayers()
{
    while (TelaPlayer && !contraPC)
    {
        Console.Clear();

        Console.WriteLine("JOGADOR 1 ESCOLHA:");
        Console.WriteLine("1 - X");
        Console.WriteLine("2 - O");

        int[] opcoesValidas = { 1, 2 };
        string opcaoSelecionada = Console.ReadLine().Trim();
        bool opcaoValida = ValidarOpcao(opcaoSelecionada, opcoesValidas);

        if (!opcaoValida)
        {
            Console.WriteLine("Selecione uma opção válida!");
            TelaPlayer = true;
        }
        else
        {

            Console.WriteLine();

            if (opcaoSelecionada == "1")
                charDaVez = 'X';
            else
                charDaVez = 'O';

            char jogador2;

            if (charDaVez == 'X')
                jogador2 = 'O';
            else
                jogador2 = 'X';

            Console.Clear();

            Console.WriteLine($"Jogador 1 será {charDaVez}.");
            Console.WriteLine($"Jogador 2 será {jogador2}.");
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine().Trim();
            Console.Clear();
            TelaPlayer = false;
        }
    }

    TelaPlayer = true;
}

void ExibirTelaDoJogo()
{
    while (TelaJogo)
    {
        ExibirTabuleiro();

        if (jogadorDaVez == 2 && contraPC)
            JogadaComputador();
        else
            JogadaJogador();

        bool verificaVitoria = VerificarVitoria();

        bool verificaEmpate = VerificarEmpate();

        if (verificaVitoria == true)
        {
            ExibirTabuleiro();

            if (jogadorDaVez == 1)
                PontosVitoriaPlayer1++;
            else if (!contraPC)
                PontosVitoriaPlayer2++;
            else
                PontosVitoriaComputador++;

            Console.WriteLine($"PLAYER {jogadorDaVez} VENCEU!\n");

            TelaFinal();
        }

        if (verificaEmpate)
        {
            ExibirTabuleiro();

            PontosEmpates++;

            Console.WriteLine($"EMPATE!!\n");

            TelaFinal();
        }

        if (!verificaVitoria && !verificaEmpate)
            TrocarJogador();
    }

    TelaJogo = true;
}

void ExibirRanking()
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
        string opcaoSelecionada = Console.ReadLine().Trim();
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

void TelaFinal()
{
    while (TelaJogarNovamente)
    {
        Console.WriteLine("1 - JOGAR NOVAMENTE");
        Console.WriteLine("2 - RANKING");
        Console.WriteLine("3 - TELA INICIAL");

        int[] opcoesValidas = { 1, 2, 3 };
        string opcaoSelecionada = Console.ReadLine().Trim();
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

                    for (int i = 0; i < tabuleiro.Length; i++)
                        tabuleiro[i] = (char)('1' + i);

                    charDaVez = 'X';
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

void ResetarVariaveis()
{
    for (int i = 0; i < tabuleiro.Length; i++)
        tabuleiro[i] = (char)('1' + i);
}

void JogadaJogador()
{
    int posicao;
    while (true)
    {
        Console.Write($"Jogador {charDaVez}, escolha uma posição (1-9): ");
        string entrada = Console.ReadLine().Trim();
        if (int.TryParse(entrada, out posicao) && posicao >= 1 && posicao <= 9)
        {
            if (tabuleiro[posicao - 1] != 'X' && tabuleiro[posicao - 1] != 'O')
            {
                tabuleiro[posicao - 1] = charDaVez;
                break;
            }
            else
                Console.WriteLine("Posição já ocupada!");
        }
        else
            Console.WriteLine("Entrada inválida!");
    }
}

void JogadaComputador()
{
    int posicao = -1;

    if (dificuldade == 1)
    {
        posicao = JogadaAleatoria();
    }
    else if (dificuldade == 2)
    {
        posicao = EncontrarJogadaVencedora('X');
        if (posicao == -1)
            posicao = JogadaAleatoria();
    }
    else if (dificuldade == 3)
    {
        posicao = EncontrarJogadaVencedora('O');

        if (posicao == -1)
            posicao = EncontrarJogadaVencedora('X');

        if (posicao == -1)
        {
            if (tabuleiro[4] != 'X' && tabuleiro[4] != 'O')
                posicao = 5;
            else
                posicao = EscolherMelhorPosicao();
        }
    }

    Console.WriteLine($"Computador escolheu a posição {posicao}");
    tabuleiro[posicao - 1] = charDaVez;
    System.Threading.Thread.Sleep(1000);
}

int JogadaAleatoria()
{
    Random rand = new Random();
    int posicao;
    do
    {
        posicao = rand.Next(1, 10);
    } while (tabuleiro[posicao - 1] == 'X' || tabuleiro[posicao - 1] == 'O');
    return posicao;
}

int EncontrarJogadaVencedora(char simbolo)
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

int EscolherMelhorPosicao()
{
    int[] cantos = { 1, 3, 7, 9 };
    int[] lados = { 2, 4, 6, 8 };

    foreach (var canto in cantos)
        if (tabuleiro[canto - 1] != 'X' && tabuleiro[canto - 1] != 'O')
            return canto;

    foreach (var lado in lados)
        if (tabuleiro[lado - 1] != 'X' && tabuleiro[lado - 1] != 'O')
            return lado;

    return JogadaAleatoria();
}

bool ValidarOpcao(string opcaoSelecionada, int[] opcoesValidas)
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

void TrocarJogador()
{
    charDaVez = charDaVez == 'X' ? 'O' : 'X';
    jogadorDaVez = jogadorDaVez == 1 ? 2 : 1;
}

bool VerificarVitoria()
{
    int[,] combinacoes = {
            {0,1,2}, {3,4,5}, {6,7,8},
            {0,3,6}, {1,4,7}, {2,5,8},
            {0,4,8}, {2,4,6}
            };

    for (int i = 0; i < combinacoes.GetLength(0); i++)
    {
        int posicaoA = combinacoes[i, 0], posicaoB = combinacoes[i, 1], posicaC = combinacoes[i, 2];

        if (tabuleiro[posicaoA] == charDaVez && tabuleiro[posicaoB] == charDaVez && tabuleiro[posicaC] == charDaVez)
            return true;
    }
    return false;
}

bool VerificarEmpate()
{
    foreach (char c in tabuleiro)
        if (c != 'X' && c != 'O')
            return false;

    return true;
}

void SetDificuldade(string codigoDificuldade)
{
    int codigo;

    if (int.TryParse(codigoDificuldade, out codigo))
    {
        dificuldade = codigo;
    }
    else
    {
        Console.WriteLine("Digite um código de dificuldade valido! \n Codigos Validos: 1, 2, 3");

        TelaInicial = true;
    }
}