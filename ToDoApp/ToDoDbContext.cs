namespace ToDoApp
{
    using Microsoft.EntityFrameworkCore;
    public class ToDoDbContext : DbContext
    {
        #region Public Properties
        public IConfiguration Configuration { get; }
        #endregion

        #region Constructors
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options, IConfiguration configuration)
         : base(options)
        {
            Configuration = configuration;
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>()
                .HasData(
                new ToDo { Id = 1, Name = "ToDo 1" },
                new ToDo { Id = 2, Name = "ToDo 2" },
                new ToDo { Id = 3, Name = "ToDo 3" },
                new ToDo { Id = 4, Name = "ToDo 4" },
                new ToDo { Id = 5, Name = "ToDo 5" }
                );
        }
        public virtual DbSet<ToDo> Todo { get; set; }
    }
}
