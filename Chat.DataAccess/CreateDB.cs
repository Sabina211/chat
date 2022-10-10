namespace Chat.DataAccess
{
    public class CreateDB
    {
        public static void Create(ChatDbContext context)
        {
            var login = "admin";
            context.Database.EnsureCreated();
            if (context.Users.Any()) return;
            using (var trans = context.Database.BeginTransaction())
            {
                context.Users.Add(new Entities.UserEntity() {Id=1, Login=login, CreatedAt=DateTime.UtcNow});
                context.SaveChanges();
                trans.Commit();
            }
        }
    }
}
