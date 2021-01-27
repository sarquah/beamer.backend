using System;

namespace Beamer.Domain.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public Guid TenantId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
			{
                return false;
			}
            var objCastet = ((User)obj);
            if (objCastet.Email == this.Email && objCastet.TenantId == this.TenantId)
            {
                return true;
            }
            return false;
        }

		public override int GetHashCode()
		{
			return this.Email.GetHashCode() ^ this.TenantId.GetHashCode();
		}
	}
}
