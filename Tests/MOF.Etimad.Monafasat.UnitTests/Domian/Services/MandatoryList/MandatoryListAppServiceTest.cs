using AutoMapper;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.MandatoryList
{
    public class MandatoryListAppServiceTest
    {
        private readonly Mock<IMapper> _mapper;
        public readonly MandatoryListAppService _sut;
        private readonly Mock<INotificationAppService> _notificationAppService;
        private readonly Mock<IMandatoryListQueries> mandatoryListQueries;
        private readonly Mock<IMandatoryListCommands> _mandatoryListCommands;
        private readonly Mock<IVerificationService> _verificationService;



        public MandatoryListAppServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _notificationAppService = new Mock<INotificationAppService>();
            mandatoryListQueries = new Mock<IMandatoryListQueries>();
            _mandatoryListCommands = new Mock<IMandatoryListCommands>();
            _verificationService = new Mock<IVerificationService>();
            _sut = new MandatoryListAppService(mandatoryListQueries.Object, _mandatoryListCommands.Object, _mapper.Object, _notificationAppService.Object, _verificationService.Object);
        }

        [Fact]
        public async Task ShouldSendToApprovalSuccess()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithProdcuts());
            });
             await _sut.SendToApproval(1);
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }

        [Fact]
        public async Task ShouldApprovalSuccess()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove());
            });
            await _sut.Approve(1,"123");
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }

        [Fact]
        public async Task ShouldApprovalSuccessThrowException()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
           .Returns(() =>
           {
               return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithProdcuts());
           });            
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await  _sut.Approve(1, "123"));
        }

        [Fact]
        public async Task ShouldRejectSuccess()
        {
            MandatoryListRejectionViewModel viewModel = new MandatoryListRejectionViewModel() { Id= "69N3QwUl%208o0yRoVEsRsYA==", RejectionReason="rejectreason"};
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove());
            });
            await _sut.Reject(viewModel);
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }

        [Fact]
        public async Task ShouldRejectSuccessThrowExceptionWhenStatusIsNotRejected()
        {
            MandatoryListRejectionViewModel viewModel = new MandatoryListRejectionViewModel() { Id = "69N3QwUl%208o0yRoVEsRsYA==" };

            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
           .Returns(() =>
           {
               return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithProdcuts());
           });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.Reject(viewModel));
        }

        [Fact]
        public async Task ShouldRejectSuccessThrowExceptionWhenRejectReasonIsNull()
        {
            MandatoryListRejectionViewModel viewModel = new MandatoryListRejectionViewModel() { Id = "69N3QwUl%208o0yRoVEsRsYA==" };
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove());
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.Reject(viewModel));
        }



        [Fact]
        public async Task ShouldRequestDeleteThrowExceptionWhenStatusNotApproved()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove());
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.RequestDelete(1));
        }



        [Fact]
        public async Task ShouldRequestDeleteSuccess()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusApprove());
            });
            await _sut.RequestDelete(1);
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }



        [Fact]
        public async Task ShouldAddSuccess()
        {
            List<InputMandatoryListProductViewModel> inputMandatoryListProductViewModels = new List<InputMandatoryListProductViewModel>();
            InputMandatoryListProductViewModel product = new InputMandatoryListProductViewModel()
            {
                CSICode = "1010",
                NameAr = "First Product",
                NameEn = "First Product",
                PriceCelling = 10,
                DescriptionAr = "Hello First Product",
                DescriptionEn = "English Description",
            };
            inputMandatoryListProductViewModels.Add(product);
            InputMandatoryListViewModel mandatoryList = new InputMandatoryListViewModel()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
                StatusId = 1,
                Products = inputMandatoryListProductViewModels
            };
            _mapper.Setup(x => x.Map<Core.Entities.MandatoryList>(It.IsAny<InputMandatoryListViewModel>())).Returns(() =>
            {
                return new MandatoryListDefault().GetMandatoryListWithProdcuts();

            });
            await _sut.Add(mandatoryList);
            _mandatoryListCommands.Verify(m => m.Add(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }

        [Fact]
        public async Task ShouldAddThrowException()
        {
            InputMandatoryListViewModel mandatoryList = new InputMandatoryListViewModel()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
                StatusId = 1,
            };
            _mapper.Setup(x => x.Map<Core.Entities.MandatoryList>(It.IsAny<InputMandatoryListViewModel>())).Returns(() =>
            {
                return new MandatoryListDefault().GetMandatoryListWithOutProdcuts();
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.Add(mandatoryList));
        }

        [Fact]
        public async Task ShouldUpdateWithChangeRequestSuccess()
        {
            List<InputMandatoryListProductViewModel> inputMandatoryListProductViewModels = new List<InputMandatoryListProductViewModel>();
            InputMandatoryListProductViewModel product = new InputMandatoryListProductViewModel()
            {
                CSICode = "1010",
                NameAr = "First Product",
                NameEn = "First Product",
                PriceCelling = 10,
                DescriptionAr = "Hello First Product",
                DescriptionEn = "English Description",
            };
            inputMandatoryListProductViewModels.Add(product);
            InputMandatoryListViewModel mandatoryList = new InputMandatoryListViewModel()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
                StatusId = 1,
                Products = inputMandatoryListProductViewModels
            };
            _mapper.Setup(x => x.Map<Core.Entities.MandatoryList>(It.IsAny<InputMandatoryListViewModel>())).Returns(() =>
            {
                return new MandatoryListDefault().GetMandatoryListWithProdcuts();
            });

            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusApprove());
              });

            _mapper.Setup(x => x.Map<Core.Entities.MandatoryListChangeRequest>(It.IsAny<Core.Entities.MandatoryList>())).Returns(() =>
            {
                return new MandatoryListDefault().GetMandatoryListChangeRequest();
            });
            await _sut.Update(mandatoryList);
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateSuccess()
        {
            List<InputMandatoryListProductViewModel> inputMandatoryListProductViewModels = new List<InputMandatoryListProductViewModel>();
            InputMandatoryListProductViewModel product = new InputMandatoryListProductViewModel()
            {
                CSICode = "1010",
                NameAr = "First Product",
                NameEn = "First Product",
                PriceCelling = 10,
                DescriptionAr = "Hello First Product",
                DescriptionEn = "English Description",
            };
            inputMandatoryListProductViewModels.Add(product);
            InputMandatoryListViewModel mandatoryList = new InputMandatoryListViewModel()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
                StatusId = 1,
                Products = inputMandatoryListProductViewModels
            };
            _mapper.Setup(x => x.Map<Core.Entities.MandatoryList>(It.IsAny<InputMandatoryListViewModel>())).Returns(() =>
            {
                return new MandatoryListDefault().GetMandatoryListWithProdcuts();
            });

            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithProdcuts());
              });

            _mapper.Setup(x => x.Map<Core.Entities.MandatoryListChangeRequest>(It.IsAny<Core.Entities.MandatoryList>())).Returns(() =>
            {
                return new MandatoryListDefault().GetMandatoryListChangeRequest();
            });
            await _sut.Update(mandatoryList);
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }



        [Fact]
        public async Task ShouldGetMandatoryListDetailsSuccess()
        {
            mandatoryListQueries.Setup(m => m.GetMandatoryListDetails(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<MandatoryListDetailsViewModel>(new MandatoryListDefault().GetMandatoryListDetailsData());
            });

            var result = await _sut.GetMandatoryListDetails(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetMandatoryListDetailsForApprovalSuccess()
        {
            mandatoryListQueries.Setup(m => m.GetMandatoryListDetailsForApproval(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<MandatoryListApprovalViewModel>(new MandatoryListDefault().GetMandatoryListApprovalViewModelData());
            });
            var result = await _sut.GetMandatoryListDetailsForApproval(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldApproveEditSuccess()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusApproveAndChangeRequest());
            });
            _mapper.Setup(x => x.Map<Core.Entities.MandatoryList>(It.IsAny<Core.Entities.MandatoryListChangeRequest>())).Returns(() =>
            {
                return new MandatoryListDefault().GetMandatoryListWithStatusApprove();
            });
            await _sut.ApproveEdit(1, "123");
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }

        [Fact]
        public async Task ShouldApproveEditThrowExceptionWhenChangeRequestIsNull()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusApprove());
            });
            _mapper.Setup(x => x.Map<Core.Entities.MandatoryList>(It.IsAny<Core.Entities.MandatoryListChangeRequest>())).Returns(() =>
            {
                return new MandatoryListDefault().GetMandatoryListWithStatusApprove();
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ApproveEdit(1,"123"));
        }

        [Fact]
        public async Task ShouldApproveEditThrowExceptionStatusIsNotApproved()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithProdcuts());
            });
            _mapper.Setup(x => x.Map<Core.Entities.MandatoryList>(It.IsAny<Core.Entities.MandatoryListChangeRequest>())).Returns(() =>
            {
                return new MandatoryListDefault().GetMandatoryListWithStatusApprove();
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ApproveEdit(1, "123"));
        }


        [Fact]
        public async Task ShouldRejectEditSuccess()
        {
            MandatoryListRejectionViewModel viewModel = new MandatoryListRejectionViewModel() { Id = "69N3QwUl%208o0yRoVEsRsYA==", RejectionReason = "rejectreason" };
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusApproveAndChangeRequest());
            });
            await _sut.RejectEdit(viewModel);
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }

        [Fact]
        public async Task ShouldRejectEditThroExceptionWhenStatusIsNotApproved()
        {
            MandatoryListRejectionViewModel viewModel = new MandatoryListRejectionViewModel() { Id = "69N3QwUl%208o0yRoVEsRsYA==", RejectionReason = "rejectreason" };
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithProdcuts());
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.RejectEdit(viewModel));

        }

        [Fact]
        public async Task ShouldRejectEditThrowExceptionWhwnRjectReasonNotSend()
        {
            MandatoryListRejectionViewModel viewModel = new MandatoryListRejectionViewModel() { Id = "69N3QwUl%208o0yRoVEsRsYA=="};
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusApproveAndChangeRequest());
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.RejectEdit(viewModel));
        }



        [Fact]
        public async Task ShouldFindSuccess()
        {

         mandatoryListQueries.Setup(m => m.ProjectedFind(It.IsAny<int>()))
         .Returns(() =>
         {
             return Task.FromResult<MandatoryListViewModel>(new MandatoryListDefault().GetMandatoryListViewModelData());
         });

            var result = await _sut.Find(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldFindForEditSuccess()
        {
            List<InputMandatoryListProductViewModel> inputMandatoryListProductViewModels = new List<InputMandatoryListProductViewModel>();
            InputMandatoryListProductViewModel product = new InputMandatoryListProductViewModel()
            {
                CSICode = "1010",
                NameAr = "First Product",
                NameEn = "First Product",
                PriceCelling = 10,
                DescriptionAr = "Hello First Product",
                DescriptionEn = "English Description",
            };
            inputMandatoryListProductViewModels.Add(product);
            InputMandatoryListViewModel mandatoryList = new InputMandatoryListViewModel()
            {
                DivisionCode = "1010",
                DivisionNameAr = "Name Ar",
                DivisionNameEn = "Name En",
                StatusId = 1,
                Products = inputMandatoryListProductViewModels
            };


            mandatoryListQueries.Setup(m => m.ProjectedFind(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<MandatoryListViewModel>(new MandatoryListDefault().GetMandatoryListViewModelData());
            });
            _mapper.Setup(x => x.Map<InputMandatoryListViewModel>(It.IsAny<MandatoryListViewModel>())).Returns(() =>
            {
                return mandatoryList;
            });

            var result = await _sut.FindForEdit(1);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldSearchSuccess()
        {
            MandatoryListSearchViewModel criteria = new MandatoryListSearchViewModel();

            mandatoryListQueries.Setup(m => m.Search(It.IsAny<MandatoryListSearchViewModel>()))
            .Returns(() =>
            {
                return Task.FromResult<QueryResult<MandatoryListIndexViewModel>>(new MandatoryListDefault().GetMandatoryListIndexViewModelModel());
            });

            var result = await _sut.Search(criteria);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldReOpenSuccess()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusRejected());
            });
            await _sut.Reopen(1);
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }


        [Fact]
        public async Task ShouldReopenThrowExceptionWhenStatusIsNotRejected()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
           .Returns(() =>
           {
               return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusApprove());
           });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.Reopen(1));
        }




        [Fact]
        public async Task ShouldDeleteSuccess()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithProdcuts());
            });
            await _sut.Delete(1);
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }

        [Fact]
        public async Task ShouldDeleteThrowExceptionWhenStatusIsNotUnderstablish()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
           .Returns(() =>
           {
               return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusApprove());
           });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.Delete(1));
        }


        [Fact]
        public async Task ShouldGetValidMandatoryListCodeForQauntityTableExceluccess()
        {
            List<string> CSICodes = new List<string>() { "code1"};


            mandatoryListQueries.Setup(m => m.GetValidMandatoryListCodeForQauntityTableExcel(It.IsAny<List<string>>()))
            .Returns(() =>
            {
                return Task.FromResult<List<string>>(CSICodes);
            });

            var result = await _sut.GetValidMandatoryListCodeForQauntityTableExcel(CSICodes);
            Assert.NotNull(result);
        }

       
        [Fact]
        public async Task ShouldGetMandatoryListCSICodeInfoSuccess()
        {
            mandatoryListQueries.Setup(m => m.GetMandatoryListCSICodeInfo(It.IsAny<string>()))
            .Returns(() =>
            {
                return Task.FromResult<CSICodeDetailsModel>(new MandatoryListDefault().GetCSICodeDetailsModel());
            });

            var result = await _sut.GetMandatoryListCSICodeInfo("code");
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetAllForQuantitiyTableSuccess()
        {
            mandatoryListQueries.Setup(m => m.GetAllForQuantityTable())
            .Returns(() =>
            {
                return Task.FromResult<List<MandatoryListForQuantityTableViewModel>>(new MandatoryListDefault().GetMandatoryListForQuantityTableViewModelModel());
            });

            var result = await _sut.GetAllForQuantitiyTable();
            Assert.NotNull(result);
        }

      

        [Fact]
        public async Task ShouldCloseEditSuccess()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusApproveAndRejectChangeRequest());
            });
            await _sut.CloseEdit(1);
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }


        [Fact]
        public async Task ShouldCloseEditThrowException_When_StatusNotEqualApproved()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusRejected());
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CloseEdit(1));
        }


        [Fact]
        public async Task ShouldCloseEditThrowException_When_ChangeRequestIsEmpty()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusApprove());
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CloseEdit(1));
        }


        
        [Fact]
        public async Task ShouldRejectDeleteSuccess()
        {
            MandatoryListRejectionViewModel viewModel = new MandatoryListRejectionViewModel() { Id = "69N3QwUl%208o0yRoVEsRsYA==", RejectionReason = "rejectreason" };
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusWaitingCancelRequest());
            });
            await _sut.RejectDelete(viewModel);
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCloseEditThrowException_When_RejectReasonIsEmpty()
        {
            MandatoryListRejectionViewModel viewModel = new MandatoryListRejectionViewModel() { Id = "69N3QwUl%208o0yRoVEsRsYA==" };
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusWaitingCancelRequest());
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.RejectDelete(viewModel));
        }

        [Fact]
        public async Task ShouldCloseEditThrowException_When_StatusIsNotWaitingCancelRequest()
        {
            MandatoryListRejectionViewModel viewModel = new MandatoryListRejectionViewModel() { Id = "69N3QwUl%208o0yRoVEsRsYA==", RejectionReason = "rejectreason" };
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove());
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.RejectDelete(viewModel));
        }


        [Fact]
        public async Task ShouldApproveDeleteSuccess()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusWaitingCancelRequest());
            });
            await _sut.ApproveDelete(1, "123");
            _mandatoryListCommands.Verify(m => m.Update(It.IsAny<Core.Entities.MandatoryList>()), Times.Once);
        }

        [Fact]
        public async Task ShouldApproveDeleteThrowException_When_StatusIsNotWaitingCancelRequest()
        {
            mandatoryListQueries.Setup(m => m.Find(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Core.Entities.MandatoryList>(new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove());
            });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ApproveDelete(1, "123"));
        }

    }
}
