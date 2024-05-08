using Microsoft.EntityFrameworkCore;
using NashGrub.Models;

namespace NashGrub.Data;
public class NashGrubDbContext : DbContext
{
    public DbSet<Hashtag> Hashtags { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public NashGrubDbContext(DbContextOptions<NashGrubDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>().HasData(new Review[]
        {
            new Review
            {
                Id = 1,
                Message = "the service at #3rdandlindsley was attentive and friendly, adding to the overall wonderful dining experience.",
                DateCreated = DateTime.Now,
                HashtagId = 1
            },
            new Review
            {
                Id = 2,
                Message = "the casual atmosphere at #hattieb'shotchicken makes it a great spot for a laid-back meal with family or friends. just be prepared to leave with a satisfied belly and a newfound love for barbecue!",
                DateCreated = DateTime.Now,
                HashtagId = 2

            },
            new Review
            {
                Id = 3,
                Message = "the tranquil ambiance at #lovelesscafe adds to the overall dining experience, making it a great place to unwind after a long day.",
                DateCreated = DateTime.Now,
                HashtagId = 3
            },
            new Review
            {
                Id = 4,
                Message = "the staff at #pancakepantry is always friendly and accommodating, adding to the welcoming atmosphere of the restaurant.",
                DateCreated = DateTime.Now,
                HashtagId = 4
            }
        });
        modelBuilder.Entity<Hashtag>().HasData(new Hashtag[]
        {
            new Hashtag
            {
                Id = 1,
                BusinessName = "3rdandlindsley",

            },
            new Hashtag
            {
                Id = 2,
                BusinessName = "hattieb'shotchicken",

            },
            new Hashtag
            {
                Id = 3,
                BusinessName = "lovelesscafe",

            },
            new Hashtag
            {
                Id = 4,
                BusinessName = "pancakepantry",

            },
        });
    }
}