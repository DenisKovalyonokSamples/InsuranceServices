﻿using DK.PolicyService.Domain.Types;
using System.Collections.ObjectModel;

namespace DK.PolicyService.Domain;

public class Offer
{
    protected IList<Cover> covers = new List<Cover>();

    protected Offer(
        string productCode,
        DateTime policyFrom,
        DateTime policyTo,
        PolicyHolder policyHolder,
        Price price,
        string agentLogin)
    {
        Id = null;
        Number = Guid.NewGuid().ToString();
        ProductCode = productCode;
        PolicyValidityPeriod = ValidityPeriod.Between(policyFrom, policyTo);
        PolicyHolder = policyHolder;
        covers = price.CoverPrices.Select(c => new Cover(c.Key, c.Value)).ToList();
        Status = OfferStatus.New;
        CreationDate = SysTime.CurrentTime;
        TotalPrice = price.CoverPrices.Sum(c => c.Value);
        AgentLogin = agentLogin;
    }

    protected Offer()
    {
    } //NH required

    public virtual Guid? Id { get; protected set; }

    public virtual string Number { get; protected set; }

    public virtual string ProductCode { get; protected set; }

    public virtual ValidityPeriod PolicyValidityPeriod { get; protected set; }

    public virtual PolicyHolder PolicyHolder { get; protected set; }

    public virtual decimal TotalPrice { get; protected set; }

    public virtual OfferStatus Status { get; protected set; }

    public virtual DateTime CreationDate { get; protected set; }


    public virtual IReadOnlyCollection<Cover> Covers => new ReadOnlyCollection<Cover>(covers);

    public virtual string AgentLogin { get; protected set; }

    public static Offer ForPrice(
        string productCode,
        DateTime policyFrom,
        DateTime policyTo,
        PolicyHolder policyHolder,
        Price price)
    {
        return new Offer
        (
            productCode,
            policyFrom,
            policyTo,
            policyHolder,
            price,
            null
        );
    }

    public static Offer ForPriceAndAgent(
        string productCode,
        DateTime policyFrom,
        DateTime policyTo,
        PolicyHolder policyHolder,
        Price price,
        string agent)
    {
        return new Offer
        (
            productCode,
            policyFrom,
            policyTo,
            policyHolder,
            price,
            agent
        );
    }

    public virtual Policy Buy(PolicyHolder customer)
    {
        if (IsExpired(SysTime.CurrentTime))
            throw new ApplicationException($"Offer {Number} has expired");

        if (Status != OfferStatus.New)
            throw new ApplicationException($"Offer {Number} is not in new status and cannot be bought");

        Status = OfferStatus.Converted;

        return Policy.FromOffer(customer, this);
    }

    public virtual bool IsExpired(DateTime theDate)
    {
        return CreationDate.AddDays(30) < theDate;
    }
}