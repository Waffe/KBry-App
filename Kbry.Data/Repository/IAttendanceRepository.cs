﻿using System;
using System.Collections.Generic;
using Kbry.Data.Model;

namespace Kbry.Data.Repository
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetAttendancesBetweenDates(DateTime fromDate, DateTime toDate);
        IEnumerable<Attendance> GetAttendancesByDate(DateTime date);
    }
}