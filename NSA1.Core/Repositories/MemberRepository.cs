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
    public class MemberRepository : IMemeberDetailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Register> _userManager;
        private readonly ILogger<MemberDetailView> _logger;
        private readonly IEmailSender _emailSender;
        public MemberRepository(ApplicationDbContext context, IMapper mapper, UserManager<Register> userManager, ILogger<MemberDetailView> logger, IEmailSender emailSender)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        public async Task<(MemberDetailView createMemberView, string message, bool isSuccessful)> AddCreateMemberViewAsync(CreateMemberView model)
        {
            var memberExist = await _context.MemberProfiles.Where(x => x.FirstName == model.FirstName && x.LastName == model.LastName).FirstOrDefaultAsync();
            if (memberExist != null)
                return await Task.FromResult((new MemberDetailView(), "These details already exist", false));
            var memberDetailsExist = await _context.Clubs.Where(x => x.RegisterId == model.RegisterId).FirstOrDefaultAsync();
            if (memberDetailsExist != null)
                return await Task.FromResult((new MemberDetailView(), "This Member details filled", false));
            var memberDetail = _mapper.Map<MemberProfile>(model);
            var memberDetailView = _mapper.Map<MemberDetailView>(memberDetail);
            try
            {
                memberDetail.CreateDate = DateTime.Now;
                memberDetail.CreatedBy = $"{memberDetailView.LastName}{memberDetailView.FirstName}";
                _context.MemberProfiles.Add(memberDetail);
                var regClub = await _context.Users.Where(u => u.Id == memberDetail.RegisterId).FirstOrDefaultAsync();
                regClub.DetailCompleted = true;
                _context.Update(regClub);
                await _context.SaveChangesAsync();
                return await Task.FromResult((memberDetailView, "Member  added  details successfully", true));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Registered Memeber with  Regid {memberDetailView.RegisterId} and Member name {memberDetailView.LastName} {memberDetailView.FirstName}");
                ex.Message.ToString();
                throw ex.InnerException;
            }
        }

        public async Task<(MemberDetailView memberDetailView, string message, bool isSuccessful)> DeleteMemberDetailViewAsync(int Id)
        {
            var memberDetail = await _context.MemberProfiles.FindAsync(Id);
            if (memberDetail == null)
            {
                return await Task.FromResult((new MemberDetailView(), "Member detail not found", false));
            }
            memberDetail.IsMemberAccountDeleted = true;
            _context.MemberProfiles.Update(memberDetail);
            await _context.SaveChangesAsync();

            var memberDetailView = _mapper.Map<MemberDetailView>(memberDetail);

            _logger.LogInformation($"The delete only disable Member Personal Detail with id {memberDetailView.RegisterId} and name {memberDetailView.LastName}{memberDetailView.FirstName}");
            return await Task.FromResult((memberDetailView, "Member delete(Disable) was successfully", true));
        }

        public async Task<(IEnumerable<MemberDetailView> memberDetailView, string message, bool isSuccessful)> GetMemberDetailViewAsync()
        {
            var members = await _context.MemberProfiles.Include(x => x.Register).ToListAsync();
            if (members.Count == 0)
            {
                return await Task.FromResult((new List<MemberDetailView>(), "No Records found", false));
            }
            var membersToReturn = _mapper.Map<IEnumerable<MemberDetailView>>(members);

            return await Task.FromResult((membersToReturn, "List of member returned successfully", true));
        }

        public async Task<(MemberDetailView memberDetailView, string message, bool isSuccessful)> GetMemberDetailViewByClubIdAsync(string memberId)
        {
            var memberDetail = await _context.MemberProfiles.Where(x => x.RegisterId == memberId).Include(x => x.Register).FirstOrDefaultAsync();
            if (memberDetail == null)
            {
                return await Task.FromResult((new MemberDetailView(), "Member details not found", false));
            }
            var memberView = _mapper.Map<MemberDetailView>(memberDetail);
            return await Task.FromResult((memberView, "Member details returned successfully", true));
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
