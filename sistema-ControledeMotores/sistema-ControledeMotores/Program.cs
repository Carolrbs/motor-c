using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_ControledeMotores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using System;
            using System.Collections.Generic;
            using System.Linq;

public class MotorStats
        {
            public int MotorId { get; set; }
            public int TotalQuantidadeComprada { get; set; }
            public double TotalValorCompra { get; set; }
            public double TotalGastoManutencao { get; set; }

            public MotorStats(int id)
            {
                MotorId = id;
                TotalQuantidadeComprada = 0;
                TotalValorCompra = 0.0;
                TotalGastoManutencao = 0.0;
            }
        }

        public class ControleMotores
        {
            private static Dictionary<int, MotorStats> motoresGuardados = new Dictionary<int, MotorStats>();

            public static void Main(string[] args)
            {
                Console.WriteLine("Oi! Esse é o seu sistema de motores!");
                bool querContinuar = true;

                while (querContinuar)
                {
                    MostrarOpcoes();
                    string oQueFazer = Console.ReadLine();
                    switch (oQueFazer)
                    {
                        case "1": ComprarMotor(); break;
                        case "2": GastarConserto(); break;
                        case "3": VerTabela(); break;
                        case "4": QuemGastouMais(); break;
                        case "5": QuemGastouMenos(); break;
                        case "0":
                            querContinuar = false;
                            Console.WriteLine("Tchau! Até a próxima!");
                            break;
                        default:
                            Console.WriteLine("Não entendi. Escolha um número que aparece na lista.");
                            break;
                    }
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            private static void MostrarOpcoes()
            {
                Console.WriteLine("\n--- O que você quer fazer agora? ---");
                Console.WriteLine("1. Registrar um motor novo que você comprou");
                Console.WriteLine("2. Adicionar um gasto de conserto para um motor");
                Console.WriteLine("3. Ver todos os motores e seus gastos");
                Console.WriteLine("4. Qual motor deu mais conserto?");
                Console.WriteLine("5. Qual motor deu menos conserto?");
                Console.WriteLine("0. Sair do sistema");
                Console.Write("Digite o número da sua escolha: ");
            }

            private static void ComprarMotor()
            {
                Console.WriteLine("\n--- Registrar Compra de Motor ---");

                int id = LerNumeroPositivo("Qual é o número (ID) deste motor? ");
                int quantidade = LerNumeroPositivo($"Quantos motores número {id} você comprou?: ");
                double precoPorMotor = LerNumeroPositivoDouble($"Qual o preço (R$) de cada motor número {id}?: ");
                double custoTotal = quantidade * precoPorMotor;

                if (!motoresGuardados.ContainsKey(id))
                {
                    motoresGuardados[id] = new MotorStats(id);
                }

                motoresGuardados[id].TotalQuantidadeComprada += quantidade;
                motoresGuardados[id].TotalValorCompra += custoTotal;
                Console.WriteLine($"Motor {id} registrado! Você comprou {quantidade} e gastou R$ {custoTotal:F2}.");
            }

            private static void GastarConserto()
            {
                Console.WriteLine("\n--- Adicionar Gasto de Conserto ---");
                int id = LerNumeroPositivo("Qual é o número (ID) do motor que você consertou?: ");

                if (motoresGuardados.ContainsKey(id))
                {
                    double valor = LerNumeroPositivoDouble($"Quanto custou o conserto do motor {id} (R$)?: ");
                    motoresGuardados[id].TotalGastoManutencao += valor;
                    Console.WriteLine($"Adicionado R$ {valor:F2} ao motor {id}. Total em consertos: R$ {motoresGuardados[id].TotalGastoManutencao:F2}.");
                }
                else
                {
                    Console.WriteLine($"Motor {id} não encontrado.");
                }
            }

            private static void VerTabela()
            {
                Console.WriteLine("\n--- Tabela de Todos os Motores ---");
                if (motoresGuardados.Count == 0)
                {
                    Console.WriteLine("Nenhum motor registrado.");
                    return;
                }

                Console.WriteLine("ID        Quantidade Comprada      Valor Compra      Gasto Conserto");
                foreach (var motor in motoresGuardados.Values.OrderBy(m => m.MotorId))
                {
                    Console.WriteLine($"{motor.MotorId,-10} {motor.TotalQuantidadeComprada,-20} {motor.TotalValorCompra,-15:F2} {motor.TotalGastoManutencao,-15:F2}");
                }
            }

            private static void QuemGastouMais()
            {
                var maisGasto = motoresGuardados.Values.OrderByDescending(m => m.TotalGastoManutencao).FirstOrDefault();
                if (maisGasto != null)
                {
                    Console.WriteLine($"Motor com mais gastos: ID {maisGasto.MotorId}. Total: R$ {maisGasto.TotalGastoManutencao:F2}.");
                }
                else
                {
                    Console.WriteLine("Nenhum gasto registrado.");
                }
            }

            private static void QuemGastouMenos()
            {
                var menosGasto = motoresGuardados.Values.Where(m => m.TotalGastoManutencao > 0).OrderBy(m => m.TotalGastoManutencao).FirstOrDefault();
                if (menosGasto != null)
                {
                    Console.WriteLine($"Motor com menos gastos: ID {menosGasto.MotorId}. Total: R$ {menosGasto.TotalGastoManutencao:F2}.");
                }
                else
                {
                    Console.WriteLine("Nenhum gasto registrado.");
                }
            }

            private static int LerNumeroPositivo(string mensagem)
            {
                int numero;
                do
                {
                    Console.Write(mensagem);
                } while (!int.TryParse(Console.ReadLine(), out numero) || numero <= 0);
                return numero;
            }

            private static double LerNumeroPositivoDouble(string mensagem)
            {
                double numero;
                do
                {
                    Console.Write(mensagem);
                } while (!double.TryParse(Console.ReadLine(), out numero) || numero <= 0);
                return numero;
            }
        }
    }
}
    }
}
