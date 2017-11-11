
namespace DocStore.WebUI.Infrastructure.Abstract {
    public interface IAuthProvider {
        void Authenticate(string username);
        void Exit();
    }
}
