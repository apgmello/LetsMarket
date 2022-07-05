using LetsMarket.Entidades;

namespace LetsMarket.Repositorios
{
    public class RepositorioFuncionario : Repositorio<Funcionario>
    {
        public override Funcionario Buscar(string valor)
        {
            foreach (var funcionario in Entidades)
            {
                if (funcionario.Login == valor)
                {
                    return funcionario;
                }
            }
            return null;
        }
    }
}
