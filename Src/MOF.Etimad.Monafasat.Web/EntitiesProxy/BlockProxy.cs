using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.EntitiesProxy
{
   public class BlockProxy
   {
      //private static ApiRequest<BasicTenderModel> apiRequest;
      private static ApiRequest<SupplierBlockModel> blockRequest;
      private static ApiRequest<object> blockObjectRequest;
      private static ApiRequest<SaveTableModel> blockRequestSaveTable;

      public static void Initialize(ControllerContext currentContext)
      {
         //apiRequest = new ApiRequest<BasicTenderModel>(currentContext);
         blockRequest = new ApiRequest<SupplierBlockModel>(currentContext);
         blockRequestSaveTable = new ApiRequest<SaveTableModel>(currentContext);
         blockObjectRequest = new ApiRequest<object>(currentContext);

      }

      public static async Task<QueryResult<SupplierBlockModel>> GetAsync(BlockSearchCriteriaModel criteria)
      {
         var blockList = JsonConvert.DeserializeObject<QueryResult<SupplierBlockModel>>(await blockRequest.GetAsync("Block/GetBlocks?" + UrlHelper.ToQueryString(criteria)));
         return blockList;
      }

      public static async Task<QueryResult<BlockCommitteeModel>> GetBlockCommittee(BlockCommitteeSearchCriteriaModel criteria)
      {
         var blockList = JsonConvert.DeserializeObject<QueryResult<BlockCommitteeModel>>(await blockRequest.GetAsync("Block/GetBlockCommittee?" + UrlHelper.ToQueryString(criteria)));
         return blockList;
      }

      public static async Task<SupplierBlockModel> GetBlockById(int blockId)
      {
         var block = JsonConvert.DeserializeObject<SupplierBlockModel>(await blockRequest.GetAsync("Block/GetBlockById/" + blockId));
         return block;
      }

      public static async Task<SupplierBlockModel> DeleteBlock(SupplierBlockModel model)
      {
         var result = JsonConvert.DeserializeObject<SupplierBlockModel>(await blockRequest.PostAsync("Block/DeactivateBlock", model));
         return result;
      }
      public static async Task<bool> AddBlockDetails(SupplierBlockModel model)
      {
         var result = await blockRequest.PostAsync("Block/AddBlock", model);
         return true;
      }

      public static async Task<bool> UpdateBlockDetails(SupplierBlockModel model)
      {
         var result = await blockRequest.PostAsync("Block/UpdateBlock", model);
         return true;
      }

      public static async Task<QueryResult<SupplierModel>> GetAllSuppliers(SupplierSearchCretriaModel model)
      {
         var blockList = JsonConvert.DeserializeObject<QueryResult<SupplierModel>>(await blockRequest.GetAsync("Block/GetAllSuppliers?" + UrlHelper.ToQueryString(model)));
         return blockList;
      }

   }
}
