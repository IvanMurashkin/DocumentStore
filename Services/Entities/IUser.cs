
namespace Services.Entities {
    public interface IUser : IEntity {

        string Login { get; set; }
        string Password { get; set; }

    }
}
