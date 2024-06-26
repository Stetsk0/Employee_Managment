﻿using Employee_Managment.Data;
using Employee_Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Managment.Repository
{
    public class StatisticsRepository
    {
        private readonly ApplicationDbContext _context;

        public StatisticsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Statistics> GetStatistics()
        {
            return _context.Statistics.ToList();
        }

        public Statistics? GetStatisticsById(int id) => _context.Statistics.FirstOrDefault(s => s.Id == id);

        public void UpdateStatistics(Statistics statistics)
        {
            _context.Entry(statistics).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
