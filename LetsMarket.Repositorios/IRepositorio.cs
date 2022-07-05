namespace LetsMarket.Repositorios
{
    public interface IRepositorio<T> where T : class
    {
        List<T> Entidades { get; }
        void Inserir(T entidade);
        List<T> Listar();
        T Buscar(string valor);
    }
}
