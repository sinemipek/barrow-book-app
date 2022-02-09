
namespace BarrowBookApp.Services
{
    public interface IXmlSerializerService
    {
        T Deserialize<T>(string path) where T : class;
    }
}
