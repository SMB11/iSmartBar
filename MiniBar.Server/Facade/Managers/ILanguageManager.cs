using BusinessEntities.Culture;
using SharedEntities.DTO.Global;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface ILanguageManager
    {
        Task<List<LanguageDTO>> GetAll();
    }
}
