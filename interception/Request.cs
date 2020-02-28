using System.Threading.Tasks;

namespace Example
{
    public abstract class Request
    {
        public abstract Task Get();
    }
}
