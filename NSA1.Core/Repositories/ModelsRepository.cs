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

     public class ModelsRepository : IModelDetailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Register> _userManager;
        private readonly ILogger<ModelsDetailsView> _logger;
        private readonly IEmailSender _emailSender;
        public ModelsRepository(ApplicationDbContext context, IMapper mapper, UserManager<Register> userManager, ILogger<ModelsDetailsView> logger, IEmailSender emailSender)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        public async Task<(ModelsDetailsView createModelsView, string message, bool isSuccessful)> AddCreateModelViewAsync(CreateModelView model)
        {           
            var modelDetailsExist = await _context.ModelProfiles.Where(x => x.RegisterId == model.RegisterId).FirstOrDefaultAsync();
            if (modelDetailsExist != null)
                return await Task.FromResult((new ModelsDetailsView(), "This model details filled", false));
            var modelDetail = _mapper.Map<ModelProfile>(model);
            var modelDetailView = _mapper.Map<ModelsDetailsView>(modelDetail);
            try
            {
                modelDetail.CreateDate = DateTime.Now;
                modelDetail.CreatedBy = $"{modelDetailView.FirstName} {modelDetailView.LastName}";
                _context.ModelProfiles.Add(modelDetail);
                var regmodel = await _context.Users.Where(u => u.Id == modelDetail.RegisterId).FirstOrDefaultAsync();
                regmodel.DetailCompleted = true;
                _context.Update(regmodel);
                await _context.SaveChangesAsync();
                return await Task.FromResult((modelDetailView, "model  details added successfully", true));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Added model with  Regid {modelDetailView.RegisterId} and Model name {modelDetailView.FirstName} {modelDetailView.LastName}");
                ex.Message.ToString();
                throw ex.InnerException;
            }
        }

        public async Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> DeleteModelDetailViewAsync(int Id)
        {
            var modelDetail = await _context.ModelProfiles.FindAsync(Id);
            if (modelDetail == null)
            {
                return await Task.FromResult((new ModelsDetailsView(), "Model detail not found", false));
            }
            modelDetail.IsModelAccountDeleted = true;
            _context.ModelProfiles.Update(modelDetail);
            await _context.SaveChangesAsync();

            var modelDetailView = _mapper.Map<ModelsDetailsView>(modelDetail);

            _logger.LogInformation($"The deleted only disable Model Personal Detail with id {modelDetailView.RegisterId} and name {modelDetailView.LastName} {modelDetailView.FirstName}");
            return await Task.FromResult((modelDetailView, "Model delete(Disable) was successfully", true));
        }

        public async Task<(IEnumerable<ModelsDetailsView> modelsDetailsView, string message, bool isSuccessful)> GetModelDetailViewAsync()
        {
            var models = await _context.ModelProfiles.Include(x => x.Register).ToListAsync();
            if (models.Count == 0)
            {
                return await Task.FromResult((new List<ModelsDetailsView>(), "No Records found", false));
            }
            var modelToReturn = _mapper.Map<IEnumerable<ModelsDetailsView>>(models);

            return await Task.FromResult((modelToReturn, "List of models returned successfully", true));
        }

        public Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> GetModelDetailViewByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> GetModelDetailViewByModelIdAsync(string modelId)
        {
            var modelDetail = await _context.ModelProfiles.Where(x => x.RegisterId == modelId).Include(x => x.Register).FirstOrDefaultAsync();
            if (modelDetail == null)
            {
                return await Task.FromResult((new ModelsDetailsView(), "Model details not found", false));
            }
            var modelView = _mapper.Map<ModelsDetailsView>(modelDetail);
            return await Task.FromResult((modelView, "Model details returned successfully", true));
        }

        public async Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> UpdateModelDetailViewAsync(string modelId, CreateModelView model)
        {
            var modelDetail = await _context.ModelProfiles.Where(x => x.RegisterId == modelId).FirstOrDefaultAsync();

            if (modelDetail == null)
            {
                return await Task.FromResult((new ModelsDetailsView(), "Model detail not found", false));
            }
            modelDetail.LastName = model.LastName;
            modelDetail.FirstName = model.FirstName;
            modelDetail.AboutMe = model.AboutMe;
            modelDetail.Language = model.Language;
            modelDetail.BodyChanges = model.BodyChanges;
            modelDetail.BreastSize = model.BreastSize;
            modelDetail.Build = model.Build;
            modelDetail.CityOfResident = model.CityOfResident;
            modelDetail.Country = model.Country;
            modelDetail.EyeColor = model.EyeColor;
            modelDetail.PubicHair = model.PubicHair;
            modelDetail.SexualPreference = model.SexualPreference;
            modelDetail.Gender = model.Gender;
            modelDetail.Ethinicity = model.Ethinicity;
            modelDetail.MyCompetence = model.MyCompetence;
            modelDetail.Nationality = model.Nationality;
            modelDetail.NickName = model.NickName;
           // modelDetail.Age = model.Age;
            modelDetail.DateOfBirth = model.DateOfBirth;
            modelDetail.HairColor = model.HairColor;
            modelDetail.Toy = model.Toy;
            modelDetail.Weight = model.Weight;
            modelDetail.DateEdited = DateTime.Now;
            modelDetail.WhatAttractMe = model.WhatAttractMe;
            _context.ModelProfiles.Update(modelDetail);
            await _context.SaveChangesAsync();
            modelDetail.RegisterId = modelDetail.RegisterId;
            //modelDetail.Cover = modelDetail.Cover;
            //modelDetail.LogoType = modelDetail.LogoType;
            var modelDetailView = _mapper.Map<ModelsDetailsView>(modelDetail);

            _logger.LogInformation($"Updated Model Details with Regid {modelDetailView.RegisterId} and with model name {modelDetailView.LastName} {modelDetailView.FirstName}");
            return await Task.FromResult((modelDetailView, "Model details updated successfully", true));
        }
    }
}
