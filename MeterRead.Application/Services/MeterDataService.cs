﻿using MeterRead.Application.Interfaces;
using MeterRead.Application.Models;
using MeterRead.Data.Entities;
using MeterRead.Data.Interfaces;

namespace MeterRead.Application.Services;

public class MeterDataService(IUnitOfWork unitOfWork) : IMeterDataService
{
    public async Task ImportReadingsAsync(MeterReadings readings)
    {
        var readingEntities = readings.Select(r => new Reading
        {
            Account = unitOfWork.AccountRepository.Get(a => a.AccountId == r.AccountId).FirstOrDefault()!,
            AccountId = r.AccountId,
            DateTime = r.MeterReadingDate.ToString("yyyy-MM-dd HH:mm:ss"),
            Value = r.MeterReadingValue
        });

        //Insert readings
        foreach (var reading in readingEntities) await unitOfWork.ReadingRepository.InsertAsync(reading);

        //Save changes
        await unitOfWork.SaveChangesAsync();
    }
}