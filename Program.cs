using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Menu();
            Console.Write("Escolha uma opção: ");
            var resp = Console.ReadLine();
            Console.Clear();

            if (resp == "1")
            {
                Console.WriteLine(new string('-', 30));
                Console.Write("Digite seu CPF: ");
                var cpf = Console.ReadLine() ?? string.Empty;
                Console.Clear();

                var cpfNovo = CpfTratado(cpf);

                if (cpfNovo.Length != 11)
                {
                    Console.WriteLine(new string('-', 30));
                    Console.WriteLine($"O número que você digitou \"{cpf}\" não está correto (precisa ter 11 dígitos).");
                    Console.WriteLine(new string('-', 30));
                    continue;
                }

                var noveDigitos = cpfNovo.Substring(0, 9);
                var dezDigitos = cpfNovo.Substring(0, 10);
                var ultimosDois = cpfNovo.Substring(9, 2);

                int digito1 = Operacao(noveDigitos);
                int digito2 = Operacao(dezDigitos);

                if (digito1 == (ultimosDois[0] - '0') && digito2 == (ultimosDois[1] - '0'))
                {
                    Console.WriteLine(new string('-', 30));
                    Console.WriteLine($"O CPF {FormataCpf(cpfNovo)} está correto");
                    Console.WriteLine(new string('-', 30));
                    Thread.Sleep(2500);
                }
                else
                {
                    Console.WriteLine(new string('-', 30));
                    Console.WriteLine($"O CPF {FormataCpf(cpfNovo)} não está correto");
                    Console.WriteLine(new string('-', 30));
                    Thread.Sleep(2500);
                }
            }
            else if (resp == "2")
            {
                break;
            }
            else
            {
                Console.WriteLine(new string('-', 30));
                Console.WriteLine("Você digitou a opção errada");
                Console.WriteLine(new string('-', 30));
                continue;
            }
        }
    }

    static int Operacao(string qntsDigitos)
    {
        // Mesma ideia do Python: se veio com 9 dígitos, começa em 10; se veio com 10, começa em 11.
        int contadorRegressivo = qntsDigitos.Length == 9 ? 10 : 11;
        int resultadoDigito = 0;

        foreach (char c in qntsDigitos)
        {
            resultadoDigito += (c - '0') * contadorRegressivo;
            contadorRegressivo--;
        }

        int digito = (resultadoDigito * 10) % 11;
        digito = digito > 9 ? 0 : digito;
        return digito;
    }

    static void Menu()
    {
        Console.WriteLine(new string('=', 30));
        Console.WriteLine("VALIDADOR DE CPF".PadLeft(20).PadRight(30));
        Console.WriteLine(new string('=', 30));
        Console.WriteLine("1- Validar um CPF");
        Console.WriteLine("2- Sair");
        Console.WriteLine(new string('-', 30));
    }

    static string CpfTratado(string cpf)
    {
        var cpfNovo = "";
        foreach (char c in cpf)
        {
            if (char.IsDigit(c))
                cpfNovo += c;
        }
        return cpfNovo;
    }

    static string FormataCpf(string cpf11)
    {
        // Apenas para imprimir bonitinho
        return $"{cpf11.Substring(0, 3)}.{cpf11.Substring(3, 3)}.{cpf11.Substring(6, 3)}-{cpf11.Substring(9, 2)}";
    }
}
