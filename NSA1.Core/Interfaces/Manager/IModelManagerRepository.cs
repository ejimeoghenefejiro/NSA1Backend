using NSA1.Core.Dto.CreateViewModels;
using NSA1.Core.Dto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Interfaces.Manager
{
    public interface IModelManagerRepository
    {
        //Model Details
        Task<(ModelsDetailsView createModelsView, string message, bool isSuccessful)> AddCreateModelViewAsync(CreateModelView model);
        Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> UpdateModelDetailViewAsync(string modeltId, CreateModelView model);
        Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> DeleteModelDetailViewAsync(int Id);
        Task<(IEnumerable<ModelsDetailsView> modelsDetailsView, string message, bool isSuccessful)> GetModelDetailViewAsync();
        Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> GetModelDetailViewByIdAsync(int Id);
        Task<(ModelsDetailsView modelsDetailsView, string message, bool isSuccessful)> GetModelDetailViewByModelIdAsync(string modelId);
    }
}
