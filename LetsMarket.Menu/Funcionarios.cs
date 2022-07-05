using LetsMarket.Entidades;
using LetsMarket.Repositorios;
using System.Text.RegularExpressions;

namespace LetsMarket.Menu
{
    public class Funcionarios
    {
        private static RepositorioFuncionario repositorio = new RepositorioFuncionario();

        public static Action CadastrarFuncionario = () =>
        {
            Console.Clear();
            Console.WriteLine("Cadastro de Funcionário");
            string login = LerLogin();
            Funcionario funcionario;
            do
            {
                funcionario = repositorio.Buscar(login);
                if (funcionario != null)
                {
                    Console.WriteLine("Login já está em uso!");
                    login = LerLogin();
                }
            } while (funcionario != null);
            string nome = LerNome();
            bool gerente = repositorio.Listar().Count == 0 ? true : LerGerente();
            string senha = LerSenha();
            funcionario = new Funcionario(nome, gerente, login, senha);
            repositorio.Inserir(funcionario);
            Console.WriteLine("\nFuncionário cadastrado com sucesso!");
        };

        private static bool LerGerente()
        {
            string resposta;
            do
            {
                Console.Write("Gerente (S/N): ");
                resposta = Console.ReadLine().ToUpper().Trim();
                if (resposta != "S" && resposta != "N")
                {
                    Console.WriteLine("Resposta inválida!");
                }
            } while (resposta != "S" && resposta != "N");

            return resposta == "S";
        }

        public static Action ListarFuncionarios = () =>
        {
            var funcionarios = repositorio.Listar();

            foreach(var funcionario in funcionarios)
            {
                Console.WriteLine($"Login: {funcionario.Login}");
                Console.WriteLine($"Nome: {funcionario.Nome}");
                Console.WriteLine($"Gerente: {(funcionario.Gerente ? "Sim" : "Não")}");
                Console.WriteLine("===============================");
            }
        };
        public static Funcionario FazerLogin()
        {
            Console.WriteLine("Efetue o login no sistema");
            Console.Write("Login: ");
            var login = Console.ReadLine();
            Console.Write("Senha: ");
            var senha = Console.ReadLine();
            if (repositorio.Listar().Count() == 0)
            {
                if (login == "admin" && senha == "admin")
                {
                    return new Funcionario("", false, login, senha);
                }
                Console.WriteLine("Usuário ou senha inválido!");
            }
            else
            {
                var funcionario = repositorio.Buscar(login);
                if (funcionario == null)
                {
                    Console.WriteLine("Funcionário não encontrado!");
                }
                else
                {
                    if (funcionario.Senha == senha)
                    {
                        return funcionario;
                    }
                    Console.WriteLine("Senha inválida!");
                }
            }
            return null;
        }

        private static string LerNome()
        {
            string nome;
            string[] sobrenome;
            do
            {
                Console.Write("Nome: ");
                nome = Console.ReadLine();
                sobrenome = nome.Trim().Split(" ");
                if (sobrenome.Length < 2)
                {
                    Console.WriteLine("Campo obrigatório!");
                }
            } while (sobrenome.Length < 2);
            return nome;
        }
        //public static string LerCargo()
        //{
        //    string cargo;
        //    do
        //    {
        //        Console.Write("Cargo: ");
        //        cargo = Console.ReadLine().Trim();
        //        if (cargo == null)
        //        {
        //            Console.WriteLine("Campo obrigatório!");
        //        }
        //    } while (cargo == null);
        //    return cargo;
        //}
        private static string LerLogin()
        {
            string login;
            Console.WriteLine("\nLogin deve conter:\n" +
                "* Somente letras e/ou números\n" +
                "* Nenhum caractere especial\n" +
                "* Sem espaços em branco\n" +
                "* Ter mais de 5 caracters e menos de 10\n");
            var regex = "^[a-zA-Z0-9]{5,10}$";
            Match match;
            do
            {
                Console.Write("Login: ");
                login = Console.ReadLine().Trim();
                match = Regex.Match(login, regex);
                if (!match.Success)
                {
                    Console.WriteLine("Login inválido, tente novamente!");
                }
            } while (!match.Success);
            return login;
        }

        private static string LerSenha()
        {
            string senha;
            bool senhaValida = false;

            Console.WriteLine("\nSenha deve conter:\n" +
                "* Uma letra maiúscula e uma minúscula\n" +
                "* Um número\n" +
                "* Um caracter especial\n" +
                "* Ter mais de 6 caracters e menos de 10\n");
            do
            {
                Console.Write("Senha: ");
                senha = Console.ReadLine().Trim();
                if (senha == null)
                {
                    Console.WriteLine("Campo obrigatório!");
                }
                var temNumero = new Regex(@"[0-9]+");
                var temMaiuscula = new Regex(@"[A-Z]+");
                var quantidade = new Regex(@".{5,10}");
                var temMinuscula = new Regex(@"[a-z]+");
                var temCaracterEspecial = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
                if (!temNumero.IsMatch(senha))
                {
                    Console.WriteLine("Senha precisa ter no mínimo um número (0-9).");
                }
                else if (!temMaiuscula.IsMatch(senha))
                {
                    Console.WriteLine("Senha precisa ter no mínimo uma letra maiúscula (A-Z).");
                }
                else if (!quantidade.IsMatch(senha))
                {
                    Console.WriteLine("Senha deve ter mais de 6 caracters e menos de 10.");
                }
                else if (!temMinuscula.IsMatch(senha))
                {
                    Console.WriteLine("Senha precisa ter no mínimo uma letra minúscula (a-z)");
                }
                else if (!temCaracterEspecial.IsMatch(senha))
                {
                    Console.WriteLine("Senha precisa ter um caracter especial ");
                }
                else
                {
                    senhaValida = true;
                }
            } while (senhaValida == false);
            return senha;
        }
    }

}