using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppSqlServerDataProvider.Data;
using WebAppSqlServerDataProvider.Models;

namespace WebAppDataProvider
{
    public class MemberDataProvider : IMemberDataProvider
    {
        #region [ Fields ]
        private readonly IDbContextFactory<FStoreDBContext> _dbContextFactory;
        #endregion

        #region [ CTor ]
        public MemberDataProvider(IDbContextFactory<FStoreDBContext> dbContextFactory) {
            _dbContextFactory = dbContextFactory;
        }
        #endregion

        #region [ CRUD ]
        public void RemoveMember(Member member) {
            try {
                Member tempMember = this.GetMemberById(member.MemberId);
                if (tempMember != null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Members.Remove(tempMember);
                    context.SaveChanges();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }

        public void UpdateMember(Member member) {
            try {
                Member tempMember = this.GetMemberById(member.MemberId);
                if (tempMember != null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Members.Update(member);
                    context.SaveChanges();

                } else {
                    throw new Exception();

                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
        public void AddMember(Member member) {
            try {
                var tempMember = this.GetMemberById(member.MemberId);
                if (tempMember == null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Members.Add(member);
                    context.SaveChanges();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
        public Member GetMemberByEmail(string email) {
            Member tempMember = null;
            try {
                using var context = _dbContextFactory.CreateDbContext();
                tempMember = context.Members.FirstOrDefault(x => x.Email.Equals(email));
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return tempMember;
        }
        public Member GetMemberById(int id) {
            Member tempMember = null;
            try {
                using var context = _dbContextFactory.CreateDbContext();
                tempMember = context.Members.FirstOrDefault(x => x.MemberId == id);
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return tempMember;
        }
        public Member Login(string email, string password) {
            Member tempMember = null;
            try {
                using var context = _dbContextFactory.CreateDbContext();
                tempMember = context.Members.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return tempMember;
        }
        #endregion

        #region [ Methods - List ]
        public List<Member> FilterMemberByString(string name) {
            var MemberList = new List<Member>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                MemberList = context.Members.ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return MemberList;
        }

        public List<Member> GetMemberList() {
            var MemberList = new List<Member>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                MemberList = context.Members.ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return MemberList;
        }
        #endregion
    }
}
