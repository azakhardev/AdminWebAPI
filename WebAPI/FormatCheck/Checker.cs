using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebAPI.FormatCheck
{
    public interface IChecker
    {
        public abstract void CheckAll(Table table);
    }
}
