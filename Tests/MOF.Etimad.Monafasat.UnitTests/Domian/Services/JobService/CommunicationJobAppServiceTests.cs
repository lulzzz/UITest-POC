using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.TestsBuilders.AgencyDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.CommunicationRequestDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.JobService
{
    public class CommunicationJobAppServiceTests
    {
        private readonly Mock<IGenericCommandRepository> _genericCommandRepository;
        private readonly Mock<ICommunicationRequestJobQueries> _communicationJobQueires;
        private readonly Mock<INotificationJobAppService> _notificationJobAppService;
        public readonly CommunicationRequestAppJobService _sut;
        public CommunicationJobAppServiceTests()
        {
            _genericCommandRepository = new Mock<IGenericCommandRepository>();
            _communicationJobQueires = new Mock<ICommunicationRequestJobQueries>();
            _notificationJobAppService = new Mock<INotificationJobAppService>();
            _sut = new CommunicationRequestAppJobService(_communicationJobQueires.Object, _genericCommandRepository.Object, _notificationJobAppService.Object);
        }

        [Fact]
        public async Task Should_FindTendersWithPlaintsAfterStoppingPeriodJob_SaveUpdates()
        {

            _communicationJobQueires.Setup(x => x.FindTendersWithPlaintsAfterStoppingPeriodJob()).Returns(() =>
            {
                return Task.FromResult<List<AgencyCommunicationRequest>>(new CommunicationRequestDefault().GetAgencyCommunicationRequests());
            });

            _communicationJobQueires.Setup(x => x.FindAgenciesByAgencyCodes(It.IsAny<List<string>>())).Returns(() =>
            {
                return Task.FromResult<List<GovAgency>>(new AgencyDefaults().GetGovAgencies());
            });

            await _sut.FindTendersWithPlaintsAfterStoppingPeriodJob();

            _genericCommandRepository.Verify(x => x.SaveAsync(), Times.AtLeastOnce);
            _genericCommandRepository.Verify(x => x.SaveAsync(), Times.AtMostOnce);
        }

        [Fact]
        public async Task Should_FindTendersWithPlaintsAfterStoppingPeriodJob_SaveUpdatesDirectPurchase()
        {

            _communicationJobQueires.Setup(x => x.FindTendersWithPlaintsAfterStoppingPeriodJob()).Returns(() =>
            {
                return Task.FromResult<List<AgencyCommunicationRequest>>(new CommunicationRequestDefault().GetAgencyCommunicationRequestsForDirectPurchase());
            });

            _communicationJobQueires.Setup(x => x.FindAgenciesByAgencyCodes(It.IsAny<List<string>>())).Returns(() =>
            {
                return Task.FromResult<List<GovAgency>>(new AgencyDefaults().GetGovAgencies());
            });


            await _sut.FindTendersWithPlaintsAfterStoppingPeriodJob();

            _genericCommandRepository.Verify(x => x.SaveAsync(), Times.AtLeastOnce);
            _genericCommandRepository.Verify(x => x.SaveAsync(), Times.AtMostOnce);
        }

        [Fact]
        public async Task Should_ExcludeSupplierOffer_UpdateAndSave()
        {

            _communicationJobQueires.Setup(x => x.GetExtendOfferValiditySuppliers()).Returns(() =>
            {
                return Task.FromResult<List<ExtendOffersValiditySupplier>>(new List<ExtendOffersValiditySupplier>() { new CommunicationRequestDefault().GetExtendOffersValiditySupplierById() });
            });

            _communicationJobQueires.Setup(x => x.GetNonExecludedOffersForExtendOffersByValidityIds(It.IsAny<List<int>>())).Returns(() =>
            {
                return Task.FromResult<List<Offer>>(new CommunicationRequestDefault().GetExecludedOffers());
            });

            _communicationJobQueires.Setup(x => x.GetExtendOffersValidityForExecludeSuppliers()).Returns(() =>
            {
                return Task.FromResult<List<AgencyCommunicationRequest>>(new CommunicationRequestDefault().GetAgencyCommunicationRequests());
            });


            await _sut.ExcludeSupplierOffer();
            _genericCommandRepository.Verify(x => x.Update(It.IsAny<Offer>()), Times.AtLeastOnce);
            _genericCommandRepository.Verify(x => x.Update(It.IsAny<Offer>()), Times.AtMostOnce);
            _genericCommandRepository.Verify(x => x.Update(It.IsAny<AgencyCommunicationRequest>()), Times.AtLeast(2));
            _genericCommandRepository.Verify(x => x.Update(It.IsAny<AgencyCommunicationRequest>()), Times.AtMost(2));
            _genericCommandRepository.Verify(x => x.SaveAsync(), Times.AtLeastOnce);
            _genericCommandRepository.Verify(x => x.SaveAsync(), Times.AtMostOnce);
        }

        [Fact]
        public async Task Should_UpdateAllNegotiationWaitingSupplierResponse_UpdateAndSave()
        {

            _communicationJobQueires.Setup(x => x.FindAllNegotiationWaitingSupplierResponse()).Returns(() =>
            {
                return Task.FromResult<List<NegotiationFirstStage>>(new List<NegotiationFirstStage>() { new NegotiationDefaults().GetNegotiationFirstStageWaitingSupplier() });
            });

            _communicationJobQueires.Setup(x => x.GetOfferById(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaults());
            });

            _communicationJobQueires.Setup(x => x.FindTenderByTenderId(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
            });

            _communicationJobQueires.Setup(x => x.FindSupplierByCRNumber(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult<Supplier>(new SupplierDefault().GetSupplier());
            });


            await _sut.UpdateAllNegotiationWaitingSupplierResponse();
            _communicationJobQueires.Verify(x => x.SaveChanges(), Times.AtLeastOnce);
            _communicationJobQueires.Verify(x => x.SaveChanges(), Times.AtMostOnce);
        }

        [Fact]
        public async Task Should_UpdateAllNegotiationWaitingSupplierResponse_UpdateAndSaveNotInvited()
        {

            _communicationJobQueires.Setup(x => x.FindAllNegotiationWaitingSupplierResponse()).Returns(() =>
            {
                return Task.FromResult<List<NegotiationFirstStage>>(new List<NegotiationFirstStage>() { new NegotiationDefaults().GetNegotiationFirstStageWaitingNotInvitedSupplier() });
            });

            _communicationJobQueires.Setup(x => x.GetOfferById(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaults());
            });

            _communicationJobQueires.Setup(x => x.FindTenderByTenderId(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
            });

            _communicationJobQueires.Setup(x => x.FindSupplierByCRNumber(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult<Supplier>(new SupplierDefault().GetSupplier());
            });


            await _sut.UpdateAllNegotiationWaitingSupplierResponse();
            _communicationJobQueires.Verify(x => x.SaveChanges(), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Should_UpdateAllSecondNegotiationWaitingSupplierResponse_UpdateAndSave()
        {

            _communicationJobQueires.Setup(x => x.FindAllSecondNegotiationWaitingSupplierResponse()).Returns(() =>
            {
                return Task.FromResult<List<NegotiationSecondStage>>(new List<NegotiationSecondStage>() { new NegotiationDefaults().GetNegotiationSecondStage() });
            });

            _communicationJobQueires.Setup(x => x.FindAgenciesByAgencyCodes(It.IsAny<List<string>>())).Returns(() =>
            {
                return Task.FromResult<List<GovAgency>>(new AgencyDefaults().GetGovAgencies());
            });

            await _sut.UpdateAllSecondNegotiationWaitingSupplierResponse();
            _communicationJobQueires.Verify(x => x.UpdateNegotiationAsync(It.IsAny<NegotiationSecondStage>()), Times.AtLeastOnce);
            _communicationJobQueires.Verify(x => x.UpdateNegotiationAsync(It.IsAny<NegotiationSecondStage>()), Times.AtMostOnce);
            _communicationJobQueires.Verify(x => x.UpdateNegotiationSecondStageAsync(It.IsAny<NegotiationSecondStage>()), Times.AtLeastOnce);
            _communicationJobQueires.Verify(x => x.UpdateNegotiationSecondStageAsync(It.IsAny<NegotiationSecondStage>()), Times.AtMostOnce);
            _communicationJobQueires.Verify(x => x.SaveChanges(), Times.AtLeastOnce);
            _communicationJobQueires.Verify(x => x.SaveChanges(), Times.AtMostOnce);
        }
    }
}
