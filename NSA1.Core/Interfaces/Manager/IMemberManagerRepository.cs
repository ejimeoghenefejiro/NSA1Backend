using NSA1.Core.Dto.CreateViewModels;
using NSA1.Core.Dto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Interfaces.Manager
{
    public interface IMemberManagerRepository
    {
        // Member Details 
        Task<(MemberDetailView createMemberView, string message, bool isSuccessful)> AddCreateMemberViewAsync(CreateMemberView model);
        Task<(MemberDetailView memberDetailView, string message, bool isSuccessful)> UpdateMemberDetailViewAsync(string memberId, CreateMemberView model);
        Task<(MemberDetailView memberDetailView, string message, bool isSuccessful)> DeleteMemberDetailViewAsync(int Id);
        Task<(IEnumerable<MemberDetailView> memberDetailView, string message, bool isSuccessful)> GetMemberDetailViewAsync();
        Task<(MemberDetailView memberDetailView, string message, bool isSuccessful)> GetMemberDetailViewByIdAsync(int Id);
        Task<(MemberDetailView memberDetailView, string message, bool isSuccessful)> GetMemberDetailViewByClubIdAsync(string memberId);
    }
}
