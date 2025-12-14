using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Conf.Options
{
    public class AuthorConfiguration : BaseConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);

            // 1 Author -> n Book
            builder.HasMany(x => x.Books)
                   .WithOne(x => x.Author)
                   .HasForeignKey(x => x.AuthorId);
        }
    }
}
