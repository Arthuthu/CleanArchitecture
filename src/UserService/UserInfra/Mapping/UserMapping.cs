using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserDomain.Entities;

namespace UserInfra.Mapping
{
	public class UserMapping : BaseMapping<User>
	{
        public UserMapping() : base("User")
        {
        }

		public override void Configure(EntityTypeBuilder<User> builder)
		{
			base.Configure(builder);

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Username).HasMaxLength(256);
			builder.Property(x => x.Email).HasMaxLength(256);
			builder.Property(x => x.Password).HasMaxLength(32);

			builder.HasIndex(x => x.Email);
		}
	}
}
