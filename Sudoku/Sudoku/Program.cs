using System;
using System.Collections;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            string strSudoku = "1 3 2 5 7 9 4 6 8\n" +
                                "4 9 8 2 6 1 3 7 5\n" +
                                "7 5 6 3 8 4 2 1 9\n" +
                                "6 4 3 1 5 8 7 9 2\n" +
                                "5 2 1 7 9 3 8 4 6\n" +
                                "9 8 7 4 2 6 5 3 1\n" +
                                "2 1 4 9 3 5 6 8 7\n" +
                                "3 6 5 8 1 7 9 2 4\n" +
                                "8 7 9 6 4 2 1 5 3\n";

            int[,] sudoku = ConverterStringParaMatriz(strSudoku);

            if (ValidarSudoku(sudoku))
            {
                Console.WriteLine("SIM");
            } else
            {
                Console.WriteLine("NAO");
            }
        }

        private static int[,] ConverterStringParaMatriz(string sudoku)
        {
            int[,] sudokuMatriz = new int[9, 9];

            sudoku = sudoku.Replace("\n", " ");
            string[] numerosSeparados = sudoku.Split(" ");

            int count = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudokuMatriz[i, j] = Convert.ToInt32(numerosSeparados[count]);
                    count++;
                }
            }

            return sudokuMatriz;
        }

        private static bool TemRepetição(int[] arr)
        {
            Hashtable table = new Hashtable();

            foreach (int i in arr)
            {
                if (table.ContainsKey(i))
                    return true;
                else
                    table.Add(i, i);
            }

            return false;
        }

        private static int[] PegarBlocoComoArray(int posicaoBlocoI, int posicaoBlocoJ, int[,] sudoku)
        {
            int offsetI = (posicaoBlocoI * 3);
            int offsetJ = (posicaoBlocoJ * 3);

            int[] bloco = new int[9];
            int blocoCount = 0;

            for (int i = offsetI; i < offsetI + 3; i++)
            {
                for (int j = offsetJ; j < offsetJ + 3; j++)
                {
                    bloco[blocoCount] = sudoku[i, j];
                    blocoCount++;
                }
            }

            return bloco;
        }

        private static bool VerificarBlocos(int[,] sudoku)
        {
            for (int posicaoBlocoI = 0; posicaoBlocoI < 3; posicaoBlocoI++)
            {
                for (int posicaoBlocoJ = 0; posicaoBlocoJ < 3; posicaoBlocoJ++)
                {
                    int[] bloco = PegarBlocoComoArray(posicaoBlocoI, posicaoBlocoJ, sudoku);

                    if (TemRepetição(bloco))
                        return false;
                }
            }

            return true;
        }

        private static int[] PegarLinhaComoArray(int i, int[,] sudoku)
        {
            int[] linha = new int[9];
            int linhaCount = 0;

            for (int j = 0; j < 9; j++)
            {
                linha[linhaCount] = sudoku[i, j];
                linhaCount++;
            }

            return linha;
        }

        private static bool VerificarLinhas(int[,] sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                int[] linha = PegarLinhaComoArray(i, sudoku);

                if (TemRepetição(linha))
                    return false;
            }

            return true;
        }

        private static int[] PegarColunasComoArray(int j, int[,] sudoku)
        {
            int[] coluna = new int[9];
            int colunaCount = 0;

            for (int i = 0; i < 9; i++)
            {
                coluna[colunaCount] = sudoku[i, j];
                colunaCount++;
            }

            return coluna;
        }

        private static bool VerificarColunas(int [,] sudoku)
        {
            for (int j = 0; j < 9; j++)
            {
                int[] coluna = PegarColunasComoArray(j, sudoku);

                if (TemRepetição(coluna))
                    return false;
            }

            return true;
        }

        private static bool ValidarSudoku(int[,] sudoku)
        {
            return VerificarBlocos(sudoku) && VerificarLinhas(sudoku) && VerificarColunas(sudoku);
        }
    }
}
