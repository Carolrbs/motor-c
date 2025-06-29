using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaControleDeMotores
{
    public class Motor
    {
        public int Id { get; set; }
        public double GastoManutencao { get; set; }
        public double ValorOriginal { get; set; } // Adicionado para custo-benefício

        public Motor(int id, double valorOriginal)
        {
            Id = id;
            ValorOriginal = valorOriginal;
            GastoManutencao = 0; // Inicialmente sem gastos de manutenção
        }

        // Calcula o custo total do motor (Valor Original + Gastos de Manutenção)
        public double CustoTotal => ValorOriginal + GastoManutencao;

        // Propriedade para calcular uma métrica de custo-benefício (quanto menor, melhor)
        // Pode ser ajustado conforme a lógica de negócio real.
        // Por exemplo, aqui estamos considerando que o custo-benefício é o custo total.
        public double CustoBeneficio => CustoTotal;
    }

    public class MotorStats
    {
        private List<Motor> motores; // Lista de motores

        public MotorStats()
        {
            // Inicializa a lista de motores com valores originais
            motores = new List<Motor>
            {
                new Motor(1, 5000.00), // Exemplo: Motor 1 com valor original de 5000
                new Motor(2, 7500.00),
                new Motor(3, 3000.00),
                new Motor(4, 12000.00),
                new Motor(5, 6000.00)
            };
        }

        // Exibe a lista de motores disponíveis
        public void ExibirMotores()
        {
            Console.WriteLine("\n--- Motores Disponíveis ---");
            foreach (var m in motores)
            {
                Console.WriteLine($"ID: {m.Id}, Valor Original: R$ {m.ValorOriginal:F2}, Gasto Manutenção: R$ {m.GastoManutencao:F2}");
            }
        }

        // Adiciona um valor de manutenção ao motor escolhido
        public void AdicionarGastoMotor()
        {
            Console.Write("\nDigite o ID do motor para adicionar gastos: ");
            if (int.TryParse(Console.ReadLine(), out int motorId))
            {
                Motor motorEscolhido = motores.FirstOrDefault(m => m.Id == motorId);

                if (motorEscolhido != null)
                {
                    Console.Write($"Digite o valor a ser adicionado ao Motor ID {motorId}: R$ ");
                    if (double.TryParse(Console.ReadLine(), out double valorAdicionado) && valorAdicionado >= 0)
                    {
                        motorEscolhido.GastoManutencao += valorAdicionado;
                        Console.WriteLine($"Gasto de R$ {valorAdicionado:F2} adicionado ao Motor ID {motorId}. Novo gasto total: R$ {motorEscolhido.GastoManutencao:F2}");
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido. Por favor, digite um número positivo.");
                    }
                }
                else
                {
                    Console.WriteLine("Motor não encontrado. Verifique o ID digitado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, digite um número.");
            }
        }

        // Exibe o motor que mais gastou em manutenção (considerando o valor inicial de 0)
        public void ExibirMotorMaisGastos()
        {
            Motor motorMaisGastos = motores.Where(m => m.GastoManutencao > 0).OrderByDescending(m => m.GastoManutencao).FirstOrDefault();

            if (motorMaisGastos != null)
            {
                Console.WriteLine($"\nMotor com mais gastos: ID {motorMaisGastos.Id} (R$ {motorMaisGastos.GastoManutencao:F2})");
            }
            else
            {
                Console.WriteLine("\nNenhum motor com gastos registrado!");
            }
        }

        // Exibe o motor que menos gastou em manutenção (apenas aqueles com gastos registrados)
        public void ExibirMotorMenosGastos()
        {
            Motor motorMenosGastos = motores.Where(m => m.GastoManutencao > 0).OrderBy(m => m.GastoManutencao).FirstOrDefault();

            if (motorMenosGastos != null)
            {
                Console.WriteLine($"\nMotor com menos gastos: ID {motorMenosGastos.Id} (R$ {motorMenosGastos.GastoManutencao:F2})");
            }
            else
            {
                Console.WriteLine("\nNenhum motor com gastos registrado!");
            }
        }

        // Gera e exibe um ranking de custo-benefício
        public void GerarRankingCustoBeneficio()
        {
            Console.WriteLine("\n--- Ranking de Custo-Benefício ---");
            Console.WriteLine("(Menor Custo Total = Melhor Custo-Benefício)");

            // Ordena os motores pelo custo total (Valor Original + Gasto Manutenção)
            // do menor para o maior para indicar melhor custo-benefício.
            var ranking = motores.OrderBy(m => m.CustoBeneficio).ToList();

            if (ranking.Any())
            {
                int posicao = 1;
                foreach (var m in ranking)
                {
                    Console.WriteLine($"{posicao}. ID: {m.Id}, Valor Original: R$ {m.ValorOriginal:F2}, Gasto Manutenção: R$ {m.GastoManutencao:F2}, Custo Total: R$ {m.CustoTotal:F2}");
                    posicao++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum motor para gerar ranking.");
            }
        }

        // Método principal para rodar a aplicação
        public void RunApplication()
        {
            string opcao = "";

            Console.WriteLine("Bem-vindo à base de dados dos motores!");

            while (opcao != "0")
            {
                Console.WriteLine("\n--- Menu Principal ---");
                Console.WriteLine("1 - Visualizar Motores");
                Console.WriteLine("2 - Adicionar Gasto a um Motor");
                Console.WriteLine("3 - Exibir Motor que Mais Gastou");
                Console.WriteLine("4 - Exibir Motor que Menos Gastou");
                Console.WriteLine("5 - Gerar Ranking de Custo-Benefício");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ExibirMotores();
                        break;
                    case "2":
                        ExibirMotores(); // Mostra os motores antes de pedir para escolher
                        AdicionarGastoMotor();
                        break;
                    case "3":
                        ExibirMotorMaisGastos();
                        break;
                    case "4":
                        ExibirMotorMenosGastos();
                        break;
                    case "5":
                        GerarRankingCustoBeneficio();
                        break;
                    case "0":
                        Console.WriteLine("\nSaindo do sistema...");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Tente novamente.");
                        break;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MotorStats app = new MotorStats();
            app.RunApplication();
        }
    }
}