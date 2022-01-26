using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSA1.Core.Data;
using NSA1.Core.Dto.CreateViewModels;
using NSA1.Core.Dto.EntityModels;
using NSA1.Core.Dto.ViewModels;
using NSA1.Core.Interfaces.Repository;
using NSA1.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Repositories
{
     public class ClubRepository : IClubDetailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Register> _userManager;
        private readonly ILogger<ClubDetailView> _logger;
        private readonly IEmailSender _emailSender;
        public ClubRepository(ApplicationDbContext context, IMapper mapper, UserManager<Register> userManager, ILogger<ClubDetailView> logger, IEmailSender emailSender)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        public async  Task<(ClubDetailView createClubView, string message, bool isSuccessful)> AddCreateClubViewAsync(CreateClubView model)
        {
            var clubExist = await _context.Clubs.Where(x => x.ClubName == model.ClubName && x.HomePhone == model.HomePhone && x.Address == model.Address).FirstOrDefaultAsync();
            if (clubExist != null)
                return await Task.FromResult((new ClubDetailView(), "These details already exist", false));
            var clubDetailsExist = await _context.Clubs.Where(x => x.RegisterId == model.RegisterId).FirstOrDefaultAsync();
            if (clubDetailsExist != null)
                return await Task.FromResult((new ClubDetailView(), "This Club details filled", false));
            var clubDetail = _mapper.Map<Club>(model);
            var clubDetailView = _mapper.Map<ClubDetailView>(clubDetail);
            try
            {
                clubDetail.CreateDate = DateTime.Now;
                clubDetail.CreatedBy = clubDetailView.ClubName;
                _context.Clubs.Add(clubDetail);
                var regClub = await _context.Users.Where(u => u.Id == clubDetail.RegisterId).FirstOrDefaultAsync();
                regClub.DetailCompleted = true;
                _context.Update(regClub);
                await _context.SaveChangesAsync();
                return await Task.FromResult((clubDetailView, "Club  added  details successfully", true));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Registered Club with  Regid {clubDetailView.RegisterId} and Club name {clubDetailView.ClubName} ");
                ex.Message.ToString();
                throw ex.InnerException;
            }
        }

        public async Task<(ClubDetailView createClubView, string message, bool isSuccessful)> DeleteClubDetailViewAsync(int Id)
        {

            var clubDetail = await _context.Clubs.FindAsync(Id);
            if (clubDetail == null)
            {
                return await Task.FromResult((new ClubDetailView(), "Club detail not found", false));
            }
            clubDetail.IsClubAccountDeleted = true;
            _context.Clubs.Update(clubDetail);
            await _context.SaveChangesAsync();

            var clubDetailView = _mapper.Map<ClubDetailView>(clubDetail);

            _logger.LogInformation($"The deleted only disable Club Personal Detail with id {clubDetailView.RegisterId} and name {clubDetailView.ClubName}");
            return await Task.FromResult((clubDetailView, "Club delete(Disable) was successfully", true));
        }

        public async Task<(IEnumerable<ClubDetailView> createClubView, string message, bool isSuccessful)> GetClubDetailViewAsync()
        {
            var clubs = await _context.Clubs.Include(x => x.Register).ToListAsync();
            if (clubs.Count == 0)
            {
                return await Task.FromResult((new List<ClubDetailView>(), "No Records found", false));
            }
            var clubsToReturn = _mapper.Map<IEnumerable<ClubDetailView>>(clubs);

            return await Task.FromResult((clubsToReturn, "List of clubs returned successfully", true));
        }

        public async Task<(ClubDetailView createClubView, string message, bool isSuccessful)> GetClubDetailViewByClubIdAsync(string clubId)
        {
            var clubDetail = await _context.Clubs.Where(x => x.RegisterId == clubId).Include(x => x.Register).FirstOrDefaultAsync();
            if (clubDetail == null)
            {
                return await Task.FromResult((new ClubDetailView(), "Club details not found", false));
            }
            var clubView = _mapper.Map<ClubDetailView>(clubDetail);
            return await Task.FromResult((clubView, "Club details returned successfully", true));
        }

        public Task<(ClubDetailView createClubView, string message, bool isSuccessful)> GetClubDetailViewByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<(ClubDetailView createClubView, string message, bool isSuccessful)> UpdateClubDetailViewAsync(string clubId, CreateClubView model)
        {
            var clubDetail = await _context.Clubs.Where(x => x.RegisterId == clubId).FirstOrDefaultAsync();

            if (clubDetail == null)
            {
                return await Task.FromResult((new ClubDetailView(), "Club detail not found", false)); 
            }
            clubDetail.ClubName = model.ClubName;
            clubDetail.Description = model.Description;
            clubDetail.street = model.street;
            clubDetail.site = model.site;
            clubDetail.Phone = model.Phone;
            clubDetail.Appartment = model.Appartment;
            clubDetail.HomePhone = model.HomePhone;
            clubDetail.Address = model.Address;
            clubDetail.Country = model.Country;
            clubDetail.ZipPostal = model.ZipPostal;
            clubDetail.Phone = model.Phone;
            clubDetail.ClubStatus = model.ClubStatus;
            _context.Clubs.Update(clubDetail);
            await _context.SaveChangesAsync();
            clubDetail.RegisterId = clubDetail.RegisterId;
            clubDetail.Cover = clubDetail.Cover;
            clubDetail.LogoType = clubDetail.LogoType;
            var clubDetailView = _mapper.Map<ClubDetailView>(clubDetail);

            _logger.LogInformation($"Updated Club Details with id {clubDetailView.RegisterId} and with club name {clubDetailView.ClubName} ");
            return await Task.FromResult((clubDetailView, "Club details updated successfully", true));
        }
    }
}
