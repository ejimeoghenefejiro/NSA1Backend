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
    public class ClubManagerRepository : IClubManaagerRepository
    {
        public Task<(ClubDetailView createClubView, string message, bool isSuccessful)> AddCreateClubViewAsync(CreateClubView model)
        {
            throw new NotImplementedException();
        }

        public Task<(ClubDetailView createClubView, string message, bool isSuccessful)> DeleteClubDetailViewAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<ClubDetailView> createClubView, string message, bool isSuccessful)> GetClubDetailViewAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(ClubDetailView createClubView, string message, bool isSuccessful)> GetClubDetailViewByClubIdAsync(string clubId)
        {
            throw new NotImplementedException();
        }

        public Task<(ClubDetailView createClubView, string message, bool isSuccessful)> GetClubDetailViewByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<(ClubDetailView createClubView, string message, bool isSuccessful)> UpdateClubDetailViewAsync(string clubtId, CreateClubView model)
        {
            throw new NotImplementedException();
        }
    }
}
