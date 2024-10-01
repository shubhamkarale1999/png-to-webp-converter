using File_Convertor.Models;
using Microsoft.EntityFrameworkCore;

namespace File_Convertor.Data
{
    public class MovieCoreDBContext : DbContext
    {
        public MovieCoreDBContext()
        { }
        public MovieCoreDBContext(DbContextOptions<MovieCoreDBContext> options)
            : base(options)
        { }
        public virtual DbSet<Movie> Movies { get; set; }

      //  public DbSet<FileUploadViewModel> UploadedFiles { get; set; }

        public DbSet<UploadedFile> UploadedFiles { get; set; }
    }
}
