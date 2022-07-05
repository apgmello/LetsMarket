using System.Xml.Serialization;

namespace LetsMarket.Repositorios
{
    public abstract class Repositorio<T> : IRepositorio<T>
    where T : class
    {
        private readonly string arquivoXML;
        public List<T> Entidades { get; private set; } = new();
        public Repositorio()
        {
            arquivoXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{typeof(T).Name}.xml");
            LerXML();
        }
        public void Inserir(T entidade)
        {
            Entidades.Add(entidade);
            SalvarXML();
        }
        private void SalvarXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            TextWriter write = new StreamWriter(arquivoXML);
            serializer.Serialize(write, Entidades);
            write.Close();
        }
        private void LerXML()
        {
            if (!File.Exists(arquivoXML))
            {
                SalvarXML();
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            TextReader reader = new StreamReader(arquivoXML);
            Entidades = (List<T>)serializer.Deserialize(reader);
            reader.Close();
        }
        public List<T> Listar()
        {
            return Entidades;
        }

        public abstract T Buscar(string valor);
    }
}
