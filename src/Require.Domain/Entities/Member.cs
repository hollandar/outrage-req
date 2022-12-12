using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Require.Domain.Entities
{
    public enum MemberStateEnum
    {
        None, Member, Invitation
    }

    public enum MemberRoleEnum
    {
        None, Owner, Administrator, Member, Viewer
    }

    public class Member
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CabinetId { get; set; } = Guid.Empty;
        public virtual Cabinet? Cabinet { get; set; }

        public Guid UserId { get; set; } = Guid.Empty;
        public virtual User User { get; set; }

        public MemberStateEnum State { get; set; } = MemberStateEnum.None;
        public string InvitedAddress { get; set; } = string.Empty;

        public MemberRoleEnum Role { get; set; } = MemberRoleEnum.None;
    }
}
