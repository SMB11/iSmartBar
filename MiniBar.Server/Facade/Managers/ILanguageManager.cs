using BusinessEntities.Culture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface ILanguageManager
    {
        List<Language> GetAll();
    }
}
