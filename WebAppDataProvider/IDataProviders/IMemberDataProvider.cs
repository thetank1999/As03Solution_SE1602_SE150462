using System.Collections.Generic;
using WebAppSqlServerDataProvider.Models;

namespace WebAppDataProvider
{
    public interface IMemberDataProvider
    {
        public List<Member> FilterMemberByString(string name);
        public Member Login(string email, string password);
        public List<Member> GetMemberList();
        public Member GetMemberByEmail(string email);
        public Member GetMemberById(int id);
        public void AddMember(Member member);
        public void UpdateMember(Member member);
        public void RemoveMember(Member member);
    }
}
