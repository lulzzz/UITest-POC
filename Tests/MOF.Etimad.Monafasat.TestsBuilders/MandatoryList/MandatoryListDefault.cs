using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders
{
    public class MandatoryListDefault
    {

      

        public MandatoryList GetMandatoryListWithProdcuts()
        {
            MandatoryList mandatoryList = new MandatoryList();
            MandatoryListProduct product = GetMandatoryListProdcut();
            mandatoryList.Products = new List<MandatoryListProduct>() {
            product,
            product
            };
            mandatoryList.Add();

            return mandatoryList;
        }

        public MandatoryList GetMandatoryListWithOutProdcuts()
        {
            MandatoryList mandatoryList = new MandatoryList();
            mandatoryList.Products = new List<MandatoryListProduct>();
            mandatoryList.Add();
            return mandatoryList;
        }


        public MandatoryList GetMandatoryListWithStatusWaitingApprove()
        {
            MandatoryList mandatoryList = new MandatoryList();
            MandatoryListProduct product = GetMandatoryListProdcut();
            mandatoryList.Products = new List<MandatoryListProduct>() {
            product,
            product
            };
            mandatoryList.Add();
            mandatoryList.SendToApproval();

            return mandatoryList;
        }

        public MandatoryList GetMandatoryListWithStatusApprove()
        {
            MandatoryList mandatoryList = new MandatoryList();
            MandatoryListProduct product = GetMandatoryListProdcut();
            mandatoryList.Products = new List<MandatoryListProduct>() {
            product,
            product
            };
            mandatoryList.Add();
            mandatoryList.SendToApproval();
            mandatoryList.Approve();

            return mandatoryList;
        }
        public MandatoryList GetMandatoryListWithStatusWaitingCancelRequest()
        {
            MandatoryList mandatoryList = new MandatoryList();
            MandatoryListProduct product = GetMandatoryListProdcut();
            mandatoryList.Products = new List<MandatoryListProduct>() {
            product,
            product
            };
            mandatoryList.Add();
            mandatoryList.SendToApproval();
            mandatoryList.Approve();
            mandatoryList.RequestDelete();
            return mandatoryList;
        }

        public MandatoryList GetMandatoryListWithStatusRejected()
        {
            MandatoryList mandatoryList = new MandatoryList();
            MandatoryListProduct product = GetMandatoryListProdcut();
            mandatoryList.Products = new List<MandatoryListProduct>() {
            product,
            product
            };
            mandatoryList.Add();
            mandatoryList.SendToApproval();
            mandatoryList.Reject("Test");
            return mandatoryList;
        }

        public MandatoryListProduct GetMandatoryListProdcut()
        {
            MandatoryListProduct product = new MandatoryListProduct() { 
            CSICode = "1010",
            NameAr = "First Product",
            NameEn = "First Product",
            PriceCelling = 10,
            DescriptionAr = "Hello First Product",
            DescriptionEn = "English Description",
            MandatoryListId = 1,
            
            };

            return product;
        }

        public MandatoryListChangeRequest GetMandatoryListChangeRequest()
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListChangeRequest()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
                Products = new List<MandatoryListProductChangeRequest>() {
                    GetMandatoryListProductChangeRequest(),
                    GetMandatoryListProductChangeRequest()
                }
            };
            changeRequest.Add();
            return changeRequest;
        }
        public MandatoryListChangeRequest GetMandatoryListChangeRequestWithRejectedStatus()
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListChangeRequest()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
                Products = new List<MandatoryListProductChangeRequest>() {
                    GetMandatoryListProductChangeRequest(),
                    GetMandatoryListProductChangeRequest()
                }
            };
            changeRequest.Add();
            changeRequest.Reject("Test");
            return changeRequest;
        }

        public MandatoryListProductChangeRequest GetMandatoryListProductChangeRequest()
        {
            MandatoryListProductChangeRequest productChangeRequest = new MandatoryListProductChangeRequest()
            {
                CSICode = "1010",
                DescriptionAr = "Name Ar",
                DescriptionEn = "Name En",
                NameAr = "Name",
                NameEn = "NameEn",
                PriceCelling = 1,
            };
            return productChangeRequest;
        }


        public MandatoryList GetMandatoryListWithStatusApproveAndRejectChangeRequest()
        {
            List<MandatoryListChangeRequest> mandatoryListChangeRequests = new List<MandatoryListChangeRequest>();
            MandatoryListChangeRequest changeRequest = new MandatoryListChangeRequest()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",

                Products = new List<MandatoryListProductChangeRequest>() {
                    GetMandatoryListProductChangeRequest(),
                    GetMandatoryListProductChangeRequest()
                }
            };
            changeRequest.Add();
            changeRequest.Reject("Test");
            mandatoryListChangeRequests.Add(changeRequest);
            MandatoryList mandatoryList = new MandatoryList();
            MandatoryListProduct product = GetMandatoryListProdcut();
            mandatoryList.ChangeRequests = mandatoryListChangeRequests;
            mandatoryList.Products = new List<MandatoryListProduct>() {
            product,
            product
            };
            mandatoryList.Add();
            mandatoryList.SendToApproval();
            mandatoryList.Approve();
            return mandatoryList;
        }
        public MandatoryList GetMandatoryListWithStatusApproveAndChangeRequest()
        {
            List<MandatoryListChangeRequest> mandatoryListChangeRequests = new List<MandatoryListChangeRequest>();
            MandatoryListChangeRequest changeRequest = new MandatoryListChangeRequest()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
             
                Products = new List<MandatoryListProductChangeRequest>() {
                    GetMandatoryListProductChangeRequest(),
                    GetMandatoryListProductChangeRequest()
                }
            };
            changeRequest.Add();
            mandatoryListChangeRequests.Add(changeRequest);
            MandatoryList mandatoryList = new MandatoryList();
            MandatoryListProduct product = GetMandatoryListProdcut();
            mandatoryList.ChangeRequests = mandatoryListChangeRequests;
            mandatoryList.Products = new List<MandatoryListProduct>() {
            product,
            product
            };
            mandatoryList.Add();
            mandatoryList.SendToApproval();
            mandatoryList.Approve();
            return mandatoryList;
        }


        public MandatoryListDetailsViewModel GetMandatoryListDetailsData()
        { 
        return new MandatoryListDetailsViewModel(){
            DivisionCode = "1010",
            DivisionNameAr = "Name Ar",
            DivisionNameEn = "Name En",
        };
        }



        public MandatoryListViewModel GetMandatoryListViewModelData()
        {
            return new MandatoryListViewModel()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
            };
        }

        public CSICodeDetailsModel GetCSICodeDetailsModel()
        {
            return new CSICodeDetailsModel() {
                DivisionCode = "1010",
                DivisionName = "Name Ar",
                CSIName = "Name En",
            };
        }



        public List<MandatoryListForQuantityTableViewModel> GetMandatoryListForQuantityTableViewModelModel()
        {
            List<MandatoryListForQuantityTableViewModel> mandatoryListForQuantityTable = new List<MandatoryListForQuantityTableViewModel>();

           
             MandatoryListForQuantityTableViewModel mandatoryListForQuantityTableViewModel=new MandatoryListForQuantityTableViewModel ()
            {
                ProductName = "ProductName",
                CSICode = "CSICode"
            };
            mandatoryListForQuantityTable.Add(mandatoryListForQuantityTableViewModel);
            return mandatoryListForQuantityTable;
        }

        public MandatoryListApprovalViewModel GetMandatoryListApprovalViewModelData()
        {
            return new MandatoryListApprovalViewModel()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
            };
        }

        public QueryResult<MandatoryListIndexViewModel> GetMandatoryListIndexViewModelModel()
        {


            List<MandatoryListIndexViewModel> mandatoryListIndex = new List<MandatoryListIndexViewModel>();
            MandatoryListIndexViewModel mandatoryListIndexViewModel = new MandatoryListIndexViewModel()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
            };
            mandatoryListIndex.Add(mandatoryListIndexViewModel);
            return new QueryResult<MandatoryListIndexViewModel>(mandatoryListIndex, mandatoryListIndex.Count, 1, 6);
        }


    }
}
