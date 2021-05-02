using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults
{
    public class TenderBiddingRoundDefaults
    {
        public BiddingRound GetBiddingRound()
        {
            BiddingRound biddingRound = new BiddingRound(DateTime.Now.AddDays(1), DateTime.Now.AddDays(1), (int)Enums.BiddingRoundStatus.Started, 1);

            biddingRound.UpdateStatus((int)Enums.BiddingRoundStatus.Started);
            Tender tender = new Tender();
            biddingRound.SetTender(tender);
            return biddingRound;
        }
        public List<BiddingRound> GetBiddingRoundsList()
        {
            List<BiddingRound> biddingRounds = new List<BiddingRound> { GetBiddingRound() };

            return biddingRounds;
        }
    }
}
