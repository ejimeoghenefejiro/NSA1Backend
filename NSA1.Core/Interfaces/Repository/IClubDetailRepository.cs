using NSA1.Core.Dto.CreateViewModels;
using NSA1.Core.Dto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Interfaces.Repository
{
    public  interface IClubDetailRepository
    {
        Task<(ClubDetailView createClubView, string message, bool isSuccessful)> AddCreateClubViewAsync(CreateClubView model);
        Task<(ClubDetailView createClubView, string message, bool isSuccessful)> UpdateClubDetailViewAsync(string clubtId, CreateClubView model);
        Task<(ClubDetailView createClubView, string message, bool isSuccessful)> DeleteClubDetailViewAsync(int Id);
        Task<(IEnumerable<ClubDetailView> createClubView, string message, bool isSuccessful)> GetClubDetailViewAsync();
        Task<(ClubDetailView createClubView, string message, bool isSuccessful)> GetClubDetailViewByIdAsync(int Id);
        Task<(ClubDetailView createClubView, string message, bool isSuccessful)> GetClubDetailViewByClubIdAsync(string clubId);
    }
}
