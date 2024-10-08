﻿using DK.AuthService.Domain;
using System.Collections.Concurrent;

namespace DK.AuthService.DataAccess;

public class InsuranceAgentsInMemoryDb : IInsuranceAgents
{
    private readonly IDictionary<string, InsuranceAgent> db = new ConcurrentDictionary<string, InsuranceAgent>();

    public InsuranceAgentsInMemoryDb()
    {
        Add(new InsuranceAgent("jimmy.solid", "secret", "static/avatars/jimmy_solid.png",
            new List<string> { "TRI", "HSI", "FAI", "CAR" }));
        Add(new InsuranceAgent("danny.solid", "secret", "static/avatars/danny.solid.png",
            new List<string> { "TRI", "HSI", "FAI", "CAR" }));
        Add(new InsuranceAgent("admin", "admin", "static/avatars/admin.png",
            new List<string> { "TRI", "HSI", "FAI", "CAR" }));
    }

    public void Add(InsuranceAgent agent)
    {
        db[agent.Login] = agent;
    }

    public InsuranceAgent FindByLogin(string login)
    {
        return db[login];
    }
}
