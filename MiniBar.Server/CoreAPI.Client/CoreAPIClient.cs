using Flurl.Http;
using SharedEntities.DTO.Global;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Client
{
    public static class CoreAPIClient
    {
        private static string ApiFormat = "http://localhost/MiniBar.Core/api{0}";

        public static async Task<List<LanguageDTO>> GetLanguages()
        {
            return await String.Format(ApiFormat, "/language/").GetJsonAsync<List<LanguageDTO>>();
        }
        
        public static async Task<ProductDTO> GetProductByID(int id)
        {
            return await String.Format(ApiFormat, "/product/" + id).GetJsonAsync<ProductDTO>();
        }

        public static async Task<bool> ValidateToken(string token)
        {
            try
            {
                var res = await String.Format(ApiFormat, "/authentication/validate").WithHeader("Authentication", "Bearer " + token).GetAsync();
                return res.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}
