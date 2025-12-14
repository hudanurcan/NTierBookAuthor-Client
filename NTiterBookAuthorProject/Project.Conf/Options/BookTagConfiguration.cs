using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Conf.Options
{
    public class BookTagConfiguration : BaseConfiguration<BookTag>
    {
        public override void Configure(EntityTypeBuilder<BookTag> builder)
        {
            //  base.Configure(builder);
            builder.HasKey(x => new { x.BookId, x.TagId });

            builder.HasOne(x => x.Book)
                   .WithMany(b => b.BookTags)
                   .HasForeignKey(x => x.BookId);

            builder.HasOne(x => x.Tag)
                   .WithMany(t => t.BookTags)
                   .HasForeignKey(x => x.TagId);
        }
    }
}
