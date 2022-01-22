using NSA1.Core.Dto.CreateViewModels;
using NSA1.Core.Dto.ViewModels;
using NSA1.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Repositories
{
     public class ModelsRepository : IModelDetailRepository
    {
        public Task<(ModelsDetailsView createModelsView, string message, bool isSuccessful)> AddCreateModelViewAsync(CreateModelView model)
        {
            throw new NotImplementedException();
        }

        public Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> DeleteModelDetailViewAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<ModelsDetailsView> modelsDetailsView, string message, bool isSuccessful)> GetModelDetailViewAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> GetModelDetailViewByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> GetModelDetailViewByModelIdAsync(string modelId)
        {
            throw new NotImplementedException();
        }

        public Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> UpdateModelDetailViewAsync(string modeltId, CreateModelView model)
        {
            throw new NotImplementedException();
        }
    }
}
