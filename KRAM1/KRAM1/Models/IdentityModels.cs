using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KRAM1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [Required]
        [StringLength(24)]
        public string Name { get; set; }
        public int UploadedPicId { get; set; }
        public string ProfilePic { get; set; }
        public bool IsAdmin { get; set; }
        public virtual IList<Comment> Comment { get; set; }
        public virtual IList<Reaction> Reaction { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("KRAM")
        {
        }
        //public ApplicationDbContext()
        //    : base("KRAMTEST", throwIfV1Schema: false)
        //{
        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }

    }
}
