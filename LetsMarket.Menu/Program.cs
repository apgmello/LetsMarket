using LetsMarket.Entidades;

namespace LetsMarket.Menu
{
    public class Program //Deveria chamar menu? menuSupremo?
    {
        public static void Main(string[] args)
        {
            Funcionario funcionario;
            do
            {
                do
                {
                    funcionario = Funcionarios.FazerLogin();
                } while (funcionario == null);
                if (funcionario.Login == "admin")
                {
                    Funcionarios.CadastrarFuncionario();
                    funcionario = null; //sair
                    Console.Clear();
                }
            } while (funcionario == null);
            Console.ResetColor();
            Console.Title = "Let's Store";

            var menu = new MenuItem("Menu Principal");

            var produtos = new MenuItem("Produtos");
            produtos.Add(new MenuItem("Cadastrar Produtos", Produtos.CadastrarProdutos));
            produtos.Add(new MenuItem("Listar Produtos", Produtos.ListarProdutos));

            var funcionarios = new MenuItem("Funcionários");
            funcionarios.Add(new MenuItem("Cadastrar Funcionários", Funcionarios.CadastrarFuncionario));
            funcionarios.Add(new MenuItem("Listar Funcionários", Funcionarios.ListarFuncionarios));

            var submenu = new MenuItem("Submenu");
            submenu.Add(new MenuItem("item do submenu"));

            menu.Add(produtos);
            menu.Add(funcionarios);
            menu.Add(submenu);

            menu.Execute();
        }
    }
}