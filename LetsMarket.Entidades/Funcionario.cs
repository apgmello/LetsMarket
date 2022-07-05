namespace LetsMarket.Entidades
{
    public class Funcionario
    {
        public string Nome { get; set; }
        public bool Gerente { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        protected Funcionario() { }

        public Funcionario(string nome, bool gerente, string login, string senha)
        {
            Nome = nome;
            Gerente = gerente;
            Login = login;
            Senha = senha;
        }

    }
}
