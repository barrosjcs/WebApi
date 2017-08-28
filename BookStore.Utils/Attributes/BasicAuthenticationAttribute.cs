using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BookStore.Utils.Attributes
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private const string BasicAuthResponseHeader = "WWW-Autenticate";
        private const string BasicAuthResponseHeaderValue = "Basic";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Recebe informações do header de requisição
            //var header = actionContext.Request.Headers;

            //Recebe valores de autorização do header
            AuthenticationHeaderValue authValue = actionContext.Request.Headers.Authorization;
            if (authValue != null && !string.IsNullOrWhiteSpace(authValue.Parameter) && authValue.Scheme == BasicAuthResponseHeaderValue)
            {
                string[] credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authValue.Parameter)).Split(':');

                // credentials terá usuário e senha, com isso poderá verificar se as informações estão validas
            }
            else
            {
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            base.OnAuthorization(actionContext);
        }
    }
}
