using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders.CommitteeDefaults;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services
{

    public class CommitteeDomainTest
    {
        private readonly Mock<ICommitteeQueries> MoqCommitteeQueries;
        private readonly Mock<ITenderQueries> MoqTenderQueries;
        private readonly Mock<IEnquiryQueries> MoqEnquiryQueries;
        private readonly CommitteeDomainService sut;
        private readonly CommitteeDefaults committeeDefaults = new CommitteeDefaults();

        public CommitteeDomainTest()
        {
            MoqCommitteeQueries = new Mock<ICommitteeQueries>();
            MoqTenderQueries = new Mock<ITenderQueries>();
            MoqEnquiryQueries = new Mock<IEnquiryQueries>();
            sut = new CommitteeDomainService(MoqCommitteeQueries.Object, MoqTenderQueries.Object, MoqEnquiryQueries.Object);
        }

        [Fact]
        public async Task ShoukdCommitteeNameIsExistSucess()
        {
            Committee committee = committeeDefaults.ReturnCommitteeDefaults();
            MoqCommitteeQueries.Setup(x => x.GetByName(It.IsAny<string>(), It.IsAny<string>(), 0))
                        .Returns(() =>
                        {
                            return Task.FromResult<bool>(true);
                        });

            Task act = sut.CheckNameExist(committee.CommitteeName, committee.AgencyCode);
            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => act);
            Assert.Equal(Resources.CommitteeResources.ErrorMessages.CommitteeNameExist, result.Message);
        }

        [Fact]
        public async Task ShoukdCommitteeNameIsNotExistSucess()
        {
            Committee committee = committeeDefaults.ReturnCommitteeDefaults();
            MoqCommitteeQueries.Setup(x => x.GetByName(It.IsAny<string>(), It.IsAny<string>(), 0))
                        .Returns(() =>
                        {
                            return Task.FromResult<bool>(false);
                        });
            var result = await sut.CheckNameExist(committee.CommitteeName, committee.AgencyCode);
            Assert.False(result);
        }


        [Fact]
        public async Task ShouldCommitteeCheckNameExistbyTypeSucess()
        {
            Committee committee = committeeDefaults.ReturnCommitteeDefaults();
            MoqCommitteeQueries.Setup(x => x.GetByNameandTypeId(It.IsAny<string>(), It.IsAny<string>(), 0))
                        .Returns(() =>
                        {
                            return Task.FromResult<bool>(true);
                        });
    
            Task act = sut.CheckNameExistbyType(committee.CommitteeName, committee.AgencyCode, 0);
            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => act);
            Assert.Equal(Resources.CommitteeResources.ErrorMessages.CommitteeNameExist, result.Message);
        }

        [Fact]
        public async Task ShouldCommitteeNameIsNotExistbyTypeSucess()
        {
            Committee committee = committeeDefaults.ReturnCommitteeDefaults();
            MoqCommitteeQueries.Setup(x => x.GetByNameandTypeId(It.IsAny<string>(), It.IsAny<string>(), 0))
                        .Returns(() =>
                        {
                            return Task.FromResult<bool>(false);
                        });
            var result = await sut.CheckNameExistbyType(committee.CommitteeName, committee.AgencyCode, 0);
            Assert.False(result);
        }

        [Fact]
        public async Task ShouldCheckUserExistSucess()
        {
            MoqCommitteeQueries.Setup(x => x.GetAssignedUserByIdAndCommittee(It.IsAny<int>(), It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<CommitteeUser>(null);
                        });
            var result = await sut.CheckUserExist(1,1);
            Assert.True(result);
        }

        [Fact]
        public async Task ShouldCheckUserExistThrowException()
        {
            MoqCommitteeQueries.Setup(x => x.GetAssignedUserByIdAndCommittee(It.IsAny<int>(), It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<CommitteeUser>(new CommitteeUser ());
                        });            
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await sut.CheckUserExist(1, 1));
        }

        [Fact]
        public async Task ShouldCheckIfHasTendersSucess()
        {
            MoqTenderQueries.Setup(x => x.GetHasTendersByCommittee(It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<bool>(false);
                        });
            var result = await sut.CheckIfHasTenders(1);
            Assert.False(result);
        }

        [Fact]
        public async Task ShouldCheckIfHasTendersThrowException()
        {
            MoqTenderQueries.Setup(x => x.GetHasTendersByCommittee(It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<bool>(true);
                        });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await sut.CheckIfHasTenders(1));
        }

        [Fact]
        public async Task ShouldCheckIfHasUsersSucess()
        {
            MoqCommitteeQueries.Setup(x => x.HasAssignedUserByCommittee(It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<bool>(false);
                        });
            var result = await sut.CheckIfHasUsers(1);
            Assert.False(result);
        }

        [Fact]
        public async Task ShouldCheckIfHasUsersThrowException()
        {
            MoqCommitteeQueries.Setup(x => x.HasAssignedUserByCommittee(It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<bool>(true);
                        });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await sut.CheckIfHasUsers(1));
        }


        [Fact]
        public async Task ShouldCheckIfHasEnqiryRepliesSucess()
        {
            MoqEnquiryQueries.Setup(x => x.GetHasEnquiryRepliesByCommittee(It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<bool>(false);
                        });
            var result = await sut.CheckIfHasEnqiryReplies(1);
            Assert.False(result);
        }

        [Fact]
        public async Task ShouldCheckIfHasEnqiryRepliesThrowException()
        {
            MoqEnquiryQueries.Setup(x => x.GetHasEnquiryRepliesByCommittee(It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<bool>(true);
                        });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await sut.CheckIfHasEnqiryReplies(1));
        }
    }
}
