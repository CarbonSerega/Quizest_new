using System;
using Entities.Models.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utility;

namespace Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D429"),
                    FirstName = "InititalFirstName",
                    LastName = "InititalLastName",
                    Email = "initial_email@example.com",
                    AvatarPath = "",
                    Role = Role.User
                },
                new User
                {
                    Id = new Guid("004EFCBD-4197-4975-9E9E-1FEB02C8D428"),
                    FirstName = "OwnerFirstName",
                    LastName = "OwnerLastName",
                    Email = "owner_email@example.com",
                    AvatarPath = DirType.Avatars + "ava.jpg",
                    Role = Role.User
                }
            );
        }
    }
}
