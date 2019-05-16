namespace SportsBetting.Data.Cache
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;

    using SportsBetting.Data.Common.Contracts;
    using SportsBetting.Data.Models;

    public class TeamsCache : ICache<int, Team>
    {
        private readonly Timer timer;
        private readonly IDictionary<int, Team> cache;
        private readonly IRepository<Team> teamsRepository;

        public TeamsCache(IRepository<Team> teamsRepository)
        {
            this.teamsRepository = teamsRepository;
            this.cache = new ConcurrentDictionary<int, Team>();
            this.timer = new Timer((_) => Load(), null, 1000, 1000 * 60);
        }

        public IEnumerable<Team> All(Expression<Func<Team, bool>> filterExpression)
        {
            return cache.Select(x => x.Value)
                .AsQueryable()
                .Where(filterExpression);
        }

        public void Add(int key, Team entity)
        {
            cache[key] = entity;
        }

        public void Delete(int key, Team entity)
        {
            Team team = cache[key];

            if (team != null)
            {
                team.IsDeleted = entity.IsDeleted;
                team.DeletedOn = entity.DeletedOn;

                cache[key] = team;
            }
        }

        public void HardDelete(int key)
        {
            cache.Remove(key);
        }

        private void Load()
        {
            IEnumerable<Team> teams = teamsRepository.All(x => !x.IsDeleted);

            foreach (var team in teams)
            {
                cache[team.Key] = team;
            }
        }
    }
}