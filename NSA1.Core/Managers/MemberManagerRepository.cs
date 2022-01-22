using NSA1.Core.Dto.CreateViewModels;
using NSA1.Core.Dto.ViewModels;
using NSA1.Core.Interfaces.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Managers
{
    public class MemberManagerRepository : IMemberManagerRepository
    {
        public Task<(MemberDetailView createMemberView, string message, bool isSuccessful)> AddCreateMemberViewAsync(CreateMemberView model)
        {
            throw new NotImplementedException();
        }

        public Task<(MemberDetailView memberDetailView, string message, bool isSuccessful)> DeleteMemberDetailViewAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<MemberDetailView> memberDetailView, string message, bool isSuccessful)> GetMemberDetailViewAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(MemberDetailView memberDetailView, string message, bool isSuccessful)> GetMemberDetailViewByClubIdAsync(string memberId)
        {
            throw new NotImplementedException();
        }

        public Task<(MemberDetailView memberDetailView, string message, bool isSuccessful)> GetMemberDetailViewByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<(MemberDetailView memberDetailView, string message, bool isSuccessful)> UpdateMemberDetailViewAsync(string memberId, CreateMemberView model)
        {
            throw new NotImplementedException();
        }
    }
}
