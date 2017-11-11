using System.Web.Security;
using DocStore.WebUI.Infrastructure.Abstract;

namespace DocStore.WebUI.Infrastructure.Concrete {

    public class FormAuthProvider : IAuthProvider {

        public void Authenticate(string username) {
            FormsAuthentication.SetAuthCookie(username, true);
        }

        public void Exit() {
            FormsAuthentication.SignOut();
        }

    }

}